<?php

namespace App\Entity;

use Doctrine\ORM\Mapping as ORM;

/**
 * @ORM\Entity
 */
class Trade
{
    /**
     * @ORM\Id
     * @ORM\GeneratedValue
     * @ORM\Column(type="integer")
     */
    private $id;

    /**
     * @ORM\Column(type="string", length=255)
     */
    private $usernameFrom;

    /**
     * @ORM\Column(type="string", length=255)
     */
    private $usernameTo;

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

    // ... getter and setter methods
}