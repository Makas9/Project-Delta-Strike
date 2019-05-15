<?php

namespace App\Tests\Controller;

use Symfony\Bundle\FrameworkBundle\Test\WebTestCase;

class RegistrationControllerTest extends WebTestCase
{
    public function testRender()
    {
        $client = static::createClient();

        $client->request('GET', '/register');

        $this->assertEquals(200, $client->getResponse()->getStatusCode());
    }

    public function testWrongUrl()
    {
        $client = static::createClient();

        $client->request('GET', '/registration');

        $this->assertEquals(404, $client->getResponse()->getStatusCode());
    }
}