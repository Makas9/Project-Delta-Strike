<?php

namespace App\Service;

use Doctrine\ORM\EntityManagerInterface;
use App\Entity\User;

class UserServices
{

    private $em;

    public function __construct(EntityManagerInterface $em){
        $this->em = $em;
    }

    public function getUserById($id)
    {
        $user =  $this->em
            ->getRepository(User::class)
            ->findBy(array('id' => $id));
        return $user[0]->getUsername();
    }
}