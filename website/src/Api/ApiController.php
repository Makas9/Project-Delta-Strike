<?php
namespace App\Api;

use FOS\RestBundle\Controller\FOSRestController;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\Routing\Annotation\Route;
use App\Entity\Item;
use App\Entity\Inventory;
use FOS\RestBundle\View\View;
use JMS\Serializer\SerializerBuilder;

final class ApiController extends FOSRestController
{
    /**
     * @Route("/getItemsList", name="getItemsList")
     */
    public function getItemsList()
    {
        $item = $this->getDoctrine()
            ->getRepository(Item::class)->findAll();
        $serializer = SerializerBuilder::create()->build();
        $jsonObject = $serializer->serialize($item, 'json');
        return View::create($jsonObject, Response::HTTP_OK);
    }

    /**
     * @Route("/getItemByUser/{user}", name="getItemByUser")
     */
    public function getItemByUser($user)
    {
        $item = $this->getDoctrine()
            ->getRepository(Inventory::class)
            ->findBy(array('user' => $user));
        $serializer = SerializerBuilder::create()->build();
        $jsonObject = $serializer->serialize($item, 'json');
        return View::create($jsonObject, Response::HTTP_OK);
    }
}