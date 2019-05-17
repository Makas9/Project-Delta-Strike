<?php

namespace App\Tests\Controller;

use Symfony\Bundle\FrameworkBundle\Test\WebTestCase;

class SecurityControllerTest extends WebTestCase
{
    public function testRender()
    {
        $client = static::createClient();

        $client->request('GET', '/login');

        $this->assertEquals(200, $client->getResponse()->getStatusCode());
    }

    public function testWrongUrl()
    {
        $client = static::createClient();

        $client->request('GET', '/login/something');

        $this->assertEquals(404, $client->getResponse()->getStatusCode());
    }
}