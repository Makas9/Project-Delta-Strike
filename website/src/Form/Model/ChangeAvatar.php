<?php

namespace App\Form\Model;

use Symfony\Component\Validator\Constraints as Assert;

class ChangeAvatar
{
    /**
     * @Assert\NotBlank()
     * @Assert\File(mimeTypes={ "image/jpeg", "image/png" })
     */
    private $avatar;

    public function getAvatar()
    {
        return $this->avatar;
    }

    public function setAvatar($avatar)
    {
        $this->avatar = $avatar;

        return $this;
    }
}