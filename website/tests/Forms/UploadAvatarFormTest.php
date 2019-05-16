<?php

namespace App\Tests\Form;

use App\Form\Model\ChangeAvatar;
use App\Form\UploadAvatar;
use App\Entity\User;
use Symfony\Component\Form\Test\TypeTestCase;

class RegistrationFormTest extends TypeTestCase
{
    public function testSubmitValidData()
    {
        $formData = [
            'avatar' => 'avatar'
        ];

        $objectToCompare = new ChangeAvatar();

        $form = $this->factory->create(UploadAvatar::class, $objectToCompare);

        $form->submit($formData);

        $this->assertTrue($form->isSynchronized());

        $view = $form->createView();
        $children = $view->children;

        foreach (array_keys($formData) as $key) {
            $this->assertArrayHasKey($key, $children);
        }
    }
}