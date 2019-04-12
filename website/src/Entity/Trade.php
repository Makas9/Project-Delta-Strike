<?php

namespace App\Entity;

use Doctrine\ORM\Mapping as ORM;
use Doctrine\ORM\EntityRepository;

/**
 * @ORM\Entity
 */
class Trade
{
    // Active => 0; Completed => 1; Denied => 2;
    /**
     * @ORM\Id
     * @ORM\GeneratedValue
     * @ORM\Column(type="integer")
     */
    private $id;

    /**
     * @ORM\Column(type="integer")
     */
    private $userFrom;

    /**
     * @ORM\Column(type="integer")
     */
    private $userTo;

    /**
     * @ORM\Column(type="array")
     */
    private $offeredItems;

    /**
     * @ORM\Column(type="array")
     */
    private $receivingItems;

    /**
     * @ORM\Column(type="integer")
     */
    private $status;

    /**
     * @ORM\Column(type="date")
     */
    private $timestamp;
    
    public function getId()
    {
        return $this->id;
    }

    public function getFrom()
    {
        return $this->userFrom;
    }

    public function getStatus()
    {
        return $this->status;
    }

    public function statusToString(){
        switch($this->status){
            case 0:
                return "Active";
                break;
            case 1:
                return "Completed";
                break;
            case 2:
                return "Denied";
                break;
        }
    }

    // ... getter and setter methods
}