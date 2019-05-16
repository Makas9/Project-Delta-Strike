<?php

namespace App\Service;

use Doctrine\ORM\EntityManagerInterface;
use App\Entity\Item;

class ItemServices
{

    private $em;

    public function __construct(EntityManagerInterface $em){
        $this->em = $em;
    }

    public function getName($id)
    {
        $item =  $this->em
            ->getRepository(Item::class)
            ->findBy(array('id' => $id));
        return $item[0]->getName();
    }

    public function getImage($id)
    {
        $item =  $this->em
            ->getRepository(Item::class)
            ->findBy(array('id' => $id));
        return $item[0]->getImage();
    }
}