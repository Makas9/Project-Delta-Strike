<?php

namespace App\Controller;

use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use App\Form\UploadAvatar;
use App\Form\Model\ChangeAvatar;
use Symfony\Component\HttpFoundation\File\Exception\FileException;
use App\Entity\User;
use App\Entity\Inventory;
use App\Entity\Item;
use App\Entity\Trade;
use App\Entity\Achievement;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;
use Doctrine\DBAL\Driver\Connection;
use Symfony\Component\HttpFoundation\File\File;

class DefaultController extends AbstractController
{



    /**
     * @Route("/", name="index")
     */
    public function index()
    {
        if(!$this->denyAccessUnlessGranted('IS_AUTHENTICATED_FULLY')){
            return $this->redirectToRoute('hub');
        }

        return $this->redirectToRoute('app_login');
    }

    /**
     * @Route("/hub", name="hub")
     */
    public function hub(Request $request)
    {
        $user = $this->getUser();

        /* ----- */
        $avatar = new ChangeAvatar();
        $form = $this->createForm(UploadAvatar::class, $avatar);
        $form->handleRequest($request);
        if ($form->isSubmitted() && $form->isValid()) {
            $file = $avatar->getAvatar();
            $fileName = $this->generateUniqueFileName().'.'.$file->guessExtension();
            try {
                $file->move(
                    $this->getParameter('avatars_directory'),
                    $fileName
                );
            } catch (FileException $e) {

            }
            $user->setAvatar($fileName);
            $entityManager = $this->getDoctrine()->getManager();
            $entityManager->merge($user);
            $entityManager->flush();
            return $this->redirectToRoute('hub');
        }
        /* ----- */

        $top = $this->getDoctrine()
            ->getRepository(User::class)
            ->findBy(array(), array('level' => 'DESC'));

        $inventory = $this->getDoctrine()
            ->getRepository(Inventory::class)
            ->findBy(array('user' => $user->getId()));

        $trades = $this->getDoctrine()
            ->getRepository(Trade::class)
            ->findBy(array('userTo' => $user->getId(), 'status' => 0));

        return $this->render('pages/hub.html.twig', [
            'title' => 'Hub',
            'avatar' => $user->getAvatar(),
            'username' => '@'.$user->getUsername(),
            'level' => $user->getLevel().' level',
            'top' => $top,
            'trades' => count($trades),
            'items' => count($inventory),
        ]);
    }

    /**
     * @Route("/trading", name="trading")
     */
    public function trading()
    {
        $user = $this->getUser();

        $active = $this->getDoctrine()
            ->getRepository(Trade::class)
            ->findBy(array('userTo' => $user->getId(), 'status' => 0));

        $completed = $this->getDoctrine()
            ->getRepository(Trade::class)
            ->findBy(array('userTo' => $user->getId(), 'status' => array(1, 2)));

        return $this->render('pages/trading.html.twig', [
            'active' => $active,
            'completed' => $completed,
        ]);
    }

    /**
     * @Route("/trade/{id}", name="trade")
     */
    public function trade($id)
    {
        $user = $this->getUser();

        $trade = $this->getDoctrine()
            ->getRepository(Trade::class)
            ->findOneBy(array('userTo' => $user->getId(), 'id' => $id, 'status' => 0));

        return $this->render('pages/trade.html.twig', [
            'id' => $id,
            'title' => 'Trade Offer #'.$id,
            'trade' => $trade
        ]);
    }

    /**
     * @Route("/trade/accept/{id}", name="trade_accept")
     */
    public function trade_accept($id)
    {
        $user = $this->getUser();

        $trade = $this->getDoctrine()
            ->getRepository(Trade::class)
            ->findOneBy(array('userTo' => $user->getId(), 'id' => $id, 'status' => 0));

        $inventory = $this->getDoctrine()
            ->getRepository(Inventory::class)
            ->findOneBy(array('user' => $user->getId()));

        $entityManager = $this->getDoctrine()->getManager();

        /* ----- */

        if ($trade->getFrom() == $user->getId()) {
            $receiving = $trade->getReceivingItems();
            $offering = $trade->getOfferedItems();
        } else {
            $receiving = $trade->getOfferedItems();
            $offering = $trade->getReceivingItems();
        }

        /* ----- */

        $values = "";
        foreach($receiving as $item){
            $values .= '("'.$item.'", "'.$user->getId().'"),';
            $sql = "DELETE FROM inventory WHERE item=".$item." AND user=".$trade->getFrom()." LIMIT 1";
            $em = $this->getDoctrine()->getManager();
            $stmt = $em->getConnection()->prepare($sql);
            $stmt->execute();
        }
        $sql = "INSERT INTO inventory (item, user) VALUES ".rtrim($values, ',');
        $em = $this->getDoctrine()->getManager();
        $stmt = $em->getConnection()->prepare($sql);
        $stmt->execute();

        /* ----- */

        $values = "";
        foreach($offering as $item){
            $values .= '("'.$item.'", "'.$trade->getFrom().'"),';
            $sql = "DELETE FROM inventory WHERE item=".$item." AND user=".$user->getId()." LIMIT 1";
            $em = $this->getDoctrine()->getManager();
            $stmt = $em->getConnection()->prepare($sql);
            $stmt->execute();
        }
        $sql = "INSERT INTO inventory (item, user) VALUES ".rtrim($values, ',');
        $em = $this->getDoctrine()->getManager();
        $stmt = $em->getConnection()->prepare($sql);
        $stmt->execute();

        /* ----- */

        $trade->setStatus(1); // Accept
        $entityManager->flush();

        return $this->render('pages/trade_action.html.twig', [
            'title' => 'Trade Offer #'.$id,
            'trade' => $trade,
            'action' => 'accept'
        ]);
    }

    /**
     * @Route("/trade/decline/{id}", name="trade_decline")
     */
    public function trade_decline($id)
    {
        $user = $this->getUser();

        $trade = $this->getDoctrine()
            ->getRepository(Trade::class)
            ->findOneBy(array('userTo' => $user->getId(), 'id' => $id, 'status' => 0));

        $entityManager = $this->getDoctrine()->getManager();
        $trade->setStatus(2); // Decline
        $entityManager->flush();

        return $this->render('pages/trade_action.html.twig', [
            'title' => 'Trade Offer #'.$id,
            'trade' => $trade,
            'action' => 'decline'
        ]);
    }

    /**
     * @Route("/achievements", name="achievements")
     */
    public function achievements()
    {
        $user = $this->getUser();

        $achievements = $this->getDoctrine()
            ->getRepository(Achievement::class)
            ->findAll();

        return $this->render('pages/achievements.html.twig', [
            'achievements' => $achievements,
            'completed' => $user->getAchievements(),
        ]);
    }

    /**
     * @Route("/settings", name="settings")
     */
    public function settings(Request $request)
    {
        $user = $this->getUser();

        $avatar = new ChangeAvatar();
        $form = $this->createForm(UploadAvatar::class, $avatar);

        $form->handleRequest($request);

        if ($form->isSubmitted() && $form->isValid()) {
            $file = $avatar->getAvatar();
            $fileName = $this->generateUniqueFileName().'.'.$file->guessExtension();
            try {
                $file->move(
                    $this->getParameter('avatars_directory'),
                    $fileName
                );
            } catch (FileException $e) {

            }
            $user->setAvatar($fileName);
            $entityManager = $this->getDoctrine()->getManager();
            $entityManager->merge($user);
            $entityManager->flush();
            return $this->redirectToRoute('settings');
        }

        return $this->render('pages/settings.html.twig', [
            'email' => $user->getEmail(),
            'form' => $form->createView(),
        ]);
    }

    /**
     * @return string
     */
    private function generateUniqueFileName()
    {
        // md5() reduces the similarity of the file names generated by
        // uniqid(), which is based on timestamps
        return md5(uniqid());
    }

    /**
     * @Route("/items", name="items")
     */
    public function items()
    {
        $user = $this->getUser();
        $inventory = $this->getDoctrine()
            ->getRepository(Inventory::class)
            ->findBy(array('user' => $user->getId()));

        $inventoryArray = array();
        foreach($inventory as $datas){
            array_push($inventoryArray, $datas->getItem());
        }

        $items = $this->getDoctrine()
            ->getRepository(Item::class)
            ->findBy(array('id' => $inventoryArray));

        return $this->render('pages/items.html.twig', [
            'count' => count($inventoryArray),
            'items' => $inventoryArray,
            'items_list' => $items,
        ]);
    }

    /**
     * @Route("/logout", name="app_logout")
     */
    public function logout()
    {
        return $this->redirectToRoute('index');
    }
}