<?php

namespace App\Entity;

use Doctrine\ORM\Mapping as ORM;

/**
 * @ORM\Entity
 */
class Item
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
    private $type;

    /**
     * @ORM\Column(type="string", length=255)
     */
    private $name;

    /**
     * @ORM\Column(type="string", length=255)
     */
    private $descripton;

    /**
     * @ORM\Column(type="array")
     */
    private $offensive;

    /**
     * @ORM\Column(type="array")
     */
    private $defensive;

    public function getId()
    {
        return $this->id;
    }

    // ... getter and setter methods
}