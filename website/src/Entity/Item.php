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
     * @ORM\Column(type="text", nullable=true)
     */
    private $image;

    /**
     * @ORM\Column(type="json")
     */
    private $stats; // [IMPORTANT] Change type to object when migrating; (Database: { "damage": 10, "fire-rate": 0.1, "something": 1 }, { n }, { n+1 } }; n - items);

    public function getId()
    {
        return $this->id;
    }

    public function getItem()
    {
        return $this;
    }

    public function getName()
    {
        return $this->name;
    }

    public function getType()
    {
        return $this->type;
    }

    public function getDescription()
    {
        return $this->descripton;
    }

    public function getImage(): ?string
    {
        if($this->image === null) {
            return "/build/images/unknown-avatar.svg";
        }

        return '/uploads/avatars/'.$this->image;
    }

    public function setImage(?string $image): self
    {
        $this->image = $image;

        return $this;
    }

    public function getStats()
    {
        return $this->stats;
    }
}