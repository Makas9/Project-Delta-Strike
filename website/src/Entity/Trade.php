<?php

namespace App\Entity;

use Doctrine\ORM\Mapping as ORM;
use Doctrine\ORM\EntityRepository;

/**
 * @ORM\Entity
 */
class Trade
{
    // Active => 0; Completed => 1; Denied => 2; Cancelled => 3;
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

    public function getTo()
    {
        return $this->userTo;
    }

    public function getReceivingItems()
    {
        return $this->receivingItems;
    }

    public function getOfferedItems()
    {
        return $this->offeredItems;
    }

    public function getStatus()
    {
        return $this->status;
    }

    public function setStatus($status)
    {
        $this->status = $status;
    }

    public function statusToString(){
        switch($this->status){
            case 0:
                return "Active";
                break;
            case 1:
                return "Accepted";
                break;
            case 2:
                return "Denied";
                break;
            case 3:
                return "Cancelled";
                break;
        }
    }

    public function setUserFrom($userFrom){
        $this->userFrom = $userFrom;
    }

    public function setUserTo($userTo){
        $this->userTo = $userTo;
    }

    public function setOfferedItems($offeredItems){
        $this->offeredItems = $offeredItems;
    }

    public function setReceivingItems($receivingItems){
        $this->receivingItems = $receivingItems;
    }

    public function setTimestamp($timestamp){
        $this->timestamp = $timestamp;
    }
    // ... getter and setter methods
}