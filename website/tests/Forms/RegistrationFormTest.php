<?php

namespace App\Tests\Form;

use App\Form\Registration;
use App\Entity\User;
use Symfony\Component\Form\Test\TypeTestCase;

class UploadAvatarFormTest extends TypeTestCase
{
    public function testSubmitValidData()
    {
        $formData = [
            'email' => 'email@gmail.com',
            'username' => 'someUsername',
            'plainPassword' => 'somePassword'
        ];

        $objectToCompare = new User();

        $form = $this->factory->create(Registration::class, $objectToCompare);

        $form->submit($formData);

        $this->assertTrue($form->isSynchronized());

        $view = $form->createView();
        $children = $view->children;

        foreach (array_keys($formData) as $key) {
            $this->assertArrayHasKey($key, $children);
        }
    }
}