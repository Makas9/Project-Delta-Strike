<?php

namespace App\Controller;

use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;
use Doctrine\DBAL\Driver\Connection;

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
    public function hub()
    {
        $user = $this->getUser();

        return $this->render('pages/hub.html.twig', [
            'title' => 'Hub',
            'avatar' => $user->getAvatar(),
            'username' => '@'.$user->getUsername(),
            'level' => $user->getLevel().' level',
        ]);
    }

    /**
     * @Route("/trading", name="trading")
     */
    public function trading()
    {
        return $this->render('pages/trading.html.twig');
    }

    /**
     * @Route("/achievements", name="achievements")
     */
    public function achievements()
    {
        return $this->render('pages/achievements.html.twig');
    }

    /**
     * @Route("/settings", name="settings")
     */
    public function settings()
    {
        return $this->render('pages/settings.html.twig');
    }

    /**
     * @Route("/items", name="items")
     */
    public function items()
    {
        return $this->render('pages/items.html.twig');
    }

    /**
     * @Route("/logout", name="app_logout")
     */
    public function logout()
    {
        return $this->redirectToRoute('index');
    }
}