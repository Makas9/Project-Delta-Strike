<?php

namespace App\Tests\Controller;

use Symfony\Bundle\FrameworkBundle\Test\WebTestCase;

class DefaultControllerTest extends WebTestCase
{
    public function testRender()
    {
        $client = static::createClient();

        $client->request('GET', '/');

        $this->assertEquals(302, $client->getResponse()->getStatusCode()); // redirect
    }

    public function testRenderNotLoggedIn()
    {
        $client = static::createClient();

        $client->request('GET', '/hub');

        $this->assertEquals(302, $client->getResponse()->getStatusCode()); // redirect
    }

    function testRenderAllLoggedOffRedirects(){
        $paths = ["/hub", "/trading", "/trade/1", "/trade/accept", "/trade/accept/1", "/trade/decline", "/trade/decline/1", "/trade/cancel", "/trade/cancel/1", "/sendtrade", "/sendtrade/1", "/send-trade", "/achievements", "/settings", "/items", "/logout"];

        $client = static::createClient();

        foreach ($paths as $key) {
            $client->request('GET', $key);
            $this->assertEquals(302, $client->getResponse()->getStatusCode()); // redirect
        }
    }

    function testRenderAllWrongUrl(){
        $paths = ["/test/a", "/logging", "/index", "/test", "/index/1"];

        $client = static::createClient();

        foreach ($paths as $key) {
            $client->request('GET', $key);
            $this->assertEquals(404, $client->getResponse()->getStatusCode()); // redirect
        }
    }
}