<?php

namespace App\Controller;

use App\Service\UserServices;
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

        if(!$user) return new Response(
            '<html><body>Error!</body></html>'
        );

        $sent = $this->getDoctrine()
            ->getRepository(Trade::class)
            ->findBy(array('userFrom' => $user->getId(), 'status' => 0));

        $active = $this->getDoctrine()
            ->getRepository(Trade::class)
            ->findBy(array('userTo' => $user->getId(), 'status' => 0));

        $completed = $this->getDoctrine()
            ->getRepository(Trade::class)
            ->findBy(array('userTo' => $user->getId(), 'status' => array(1, 2)));

        return $this->render('pages/trading.html.twig', [
            'sent' => $sent,
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

        if(!$user) return new Response(
            '<html><body>Error!</body></html>'
        );

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

        if(!$user) return new Response(
            '<html><body>Error!</body></html>'
        );

        $trade = $this->getDoctrine()
            ->getRepository(Trade::class)
            ->findOneBy(array('userTo' => $user->getId(), 'id' => $id, 'status' => 0));

        $inventory = $this->getDoctrine()
            ->getRepository(Inventory::class)
            ->findOneBy(array('user' => $user->getId()));

        $entityManager = $this->getDoctrine()->getManager();

        /* ----- */

        if(!$trade) return new Response(
            '<html><body>This trade offer is not active!</body></html>'
        );

        if ($trade->getFrom() == $user->getId()) {
            $receiving = $trade->getReceivingItems();
            $offering = $trade->getOfferedItems();
        } else {
            $receiving = $trade->getOfferedItems();
            $offering = $trade->getReceivingItems();
        }

        /* ----- */

        foreach ($receiving as $item) {
           if(!$item) {
               $receiving = false;
           }
        }

        foreach ($offering as $item) {
            if(!$item) {
                $offering = false;
            }
        }

        /* ----- */

        if($receiving) {
            $values = "";
            foreach ($receiving as $item) {
                $values .= '("' . $item . '", "' . $user->getId() . '"),';
                $sql = "DELETE FROM inventory WHERE item=" . $item . " AND user=" . $trade->getFrom() . " LIMIT 1";
                $em = $this->getDoctrine()->getManager();
                $stmt = $em->getConnection()->prepare($sql);
                $stmt->execute();
            }
            $sql = "INSERT INTO inventory (item, user) VALUES " . rtrim($values, ',');
            $em = $this->getDoctrine()->getManager();
            $stmt = $em->getConnection()->prepare($sql);
            $stmt->execute();
        }

        /* ----- */

        if($offering) {
            $values = "";
            foreach ($offering as $item) {
                $values .= '("' . $item . '", "' . $trade->getFrom() . '"),';
                $sql = "DELETE FROM inventory WHERE item=" . $item . " AND user=" . $user->getId() . " LIMIT 1";
                $em = $this->getDoctrine()->getManager();
                $stmt = $em->getConnection()->prepare($sql);
                $stmt->execute();
            }
            $sql = "INSERT INTO inventory (item, user) VALUES " . rtrim($values, ',');
            $em = $this->getDoctrine()->getManager();
            $stmt = $em->getConnection()->prepare($sql);
            $stmt->execute();
        }

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

        if(!$user) return new Response(
            '<html><body>Error!</body></html>'
        );

        $trade = $this->getDoctrine()
            ->getRepository(Trade::class)
            ->findOneBy(array('userTo' => $user->getId(), 'id' => $id, 'status' => 0));

        if($trade) {
            $entityManager = $this->getDoctrine()->getManager();
            $trade->setStatus(2); // Decline
            $entityManager->flush();
        }

        return $this->render('pages/trade_action.html.twig', [
            'title' => 'Trade Offer #'.$id,
            'trade' => $trade,
            'action' => 'decline'
        ]);
    }

    /**
     * @Route("/trade/cancel/{id}", name="trade_cancel")
     */
    public function trade_cancel($id)
    {
        $user = $this->getUser();

        if(!$user) return new Response(
            '<html><body>Error!</body></html>'
        );

        $trade = $this->getDoctrine()
            ->getRepository(Trade::class)
            ->findOneBy(array('userFrom' => $user->getId(), 'id' => $id, 'status' => 0));

        if($trade) {
            $entityManager = $this->getDoctrine()->getManager();
            $trade->setStatus(3); // Cancel
            $entityManager->flush();
        }

        return $this->redirectToRoute('hub');
    }

    /**
     * @Route("/sendtrade/{usernameTo}", name="send_trade")
     */
    public function send_trade($usernameTo = "")
    {
        $user = $this->getUser();

        if(!$user) return new Response(
            '<html><body>Error!</body></html>'
        );

        $myInventory = $this->getDoctrine()
            ->getRepository(Inventory::class)
            ->findBy(array('user' => $user->getId()));

        $myInventoryArray = array();
        foreach($myInventory as $data){
            array_push($myInventoryArray, $data->getItem());
        }

        $hisID = $this->getDoctrine()
            ->getRepository(User::class)
            ->findOneBy(array('username' =>  $usernameTo));

        if(!$hisID) return new Response(
            '<html><body>Username not found!</body></html>'
        );

        $hisInventory = $this->getDoctrine()
            ->getRepository(Inventory::class)
            ->findBy(array('user' =>  $hisID->getId()));

        $hisInventoryArray = array();
        foreach($hisInventory as $data){
            array_push($hisInventoryArray, $data->getItem());
        }

        $items = $this->getDoctrine()
            ->getRepository(Item::class)
            ->findAll();

        return $this->render('pages/send_trade.html.twig', [
            'usernameTo' => $usernameTo,
            'idTo' => $hisID->getId(),
            'title' => 'Send trade to '.$usernameTo,
            'myInventory' => $myInventoryArray,
            'hisInventory' => $hisInventoryArray,
            'items_list' => $items
        ]);
    }

    /**
     * @Route("/send-trade", name="send_trade_send")
     */
    public function send_trade_send(Request $request)
    {
        $user = $this->getUser();

        if(!$user) return new Response(
            '<html><body>Error!</body></html>'
        );

        $sentTradesCheck = $this->getDoctrine()
            ->getRepository(Trade::class)
            ->findBy(array('userFrom' =>  $user->getId(), 'status' => 0));

        if($sentTradesCheck) return new Response(
            '<html><body>You cant send more than one trade offer!</body></html>'
        );

        $userToID = $request->query->get('to');
        $myItems = $request->query->get('my');
        $theirItems = $request->query->get('their');

        $hisInvCheck = explode(",", $theirItems);
        $myInvCheck = explode(",", $myItems);

        if(!$userToID) return new Response(
            '<html><body>Missing some parameters!</body></html>'
        );

        if($theirItems) {
            $hisInventory = $this->getDoctrine()
                ->getRepository(Inventory::class)
                ->findBy(array('user' => $userToID));

            $hisInvArray = [];
            foreach ($hisInventory as $data) {
                array_push($hisInvArray, $data->getItem());
            }

            foreach (explode(",", $theirItems) as $data) {
                $key = array_search($data, $hisInvArray);
                if ($key !== false) {
                    unset($hisInvArray[$key]);
                } else return new Response(
                    '<html><body>Receiver is missing items!</body></html>'
                );
            }
        }
        if($myItems) {
            $myInventory = $this->getDoctrine()
                ->getRepository(Inventory::class)
                ->findBy(array('user' => $user->getId()));

            $myInvArray = [];
            foreach ($myInventory as $data) {
                array_push($myInvArray, $data->getItem());
            }

            foreach (explode(",", $myItems) as $data) {
                $key = array_search($data, $myInvArray);
                if ($key !== false) {
                    unset($myInvArray[$key]);
                } else return new Response(
                    '<html><body>Sender is missing items!</body></html>'
                );
            }
        }
        $trade = new Trade();
        $trade->setUserFrom($user->getId());
        $trade->setUserTo($userToID);
        $trade->setOfferedItems($myInvCheck);
        $trade->setReceivingItems($hisInvCheck);
        $trade->setStatus(0);
        $trade->setTimestamp(new \DateTime());
        $em = $this->getDoctrine()->getManager();
        $em->persist($trade);
        $em->flush();

        return $this->redirectToRoute('hub');
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