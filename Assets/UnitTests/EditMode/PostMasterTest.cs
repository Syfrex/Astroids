using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PostMasterTest
{
    [Test]
    public void AddSubscriberAndRecieveMessage()
    {
        GameObject me = new GameObject();
        Bullet bullet = me.AddComponent<Bullet>();
        PostMaster.AddSubscriber(bullet, MessageType.eBulletCollision);
        Message msgBullet;
        msgBullet.subscriber = me;
        msgBullet.type = MessageType.eBulletCollision;
        bool recievedMessage = bullet.ReciveMessage(msgBullet);
        Assert.IsTrue(recievedMessage);
    }

    [Test]
    public void SendWrongMessage()
    {
        GameObject me = new GameObject();
        Bullet bullet = me.AddComponent<Bullet>();
        PostMaster.AddSubscriber(bullet, MessageType.eBulletCollision);
        PostMaster.AddSubscriber(bullet, MessageType.eWaveCleared);
        PostMaster.AddSubscriber(bullet, MessageType.ePlayerDied);
        Message msgBullet;
        msgBullet.subscriber = me;
        msgBullet.type = MessageType.ePlayerCollision;
        bool recievedMessage = bullet.ReciveMessage(msgBullet); //if it return false, set true
        Assert.IsFalse(recievedMessage);
    }
}
