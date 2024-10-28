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
        PostMaster.AddSubscriber(bullet, PostMasterMessage.MessageType.eBulletCollision);
        PostMasterMessage.Message msgBullet;
        msgBullet.subscriber = me;
        msgBullet.type = PostMasterMessage.MessageType.eBulletCollision;
        bool recievedMessage = bullet.ReciveMessage(msgBullet);
        Assert.IsTrue(recievedMessage);
    }

    [Test]
    public void SendWrongMessage()
    {
        GameObject me = new GameObject();
        Bullet bullet = me.AddComponent<Bullet>();
        PostMaster.AddSubscriber(bullet, PostMasterMessage.MessageType.eBulletCollision);
        PostMaster.AddSubscriber(bullet, PostMasterMessage.MessageType.eWaveCleared);
        PostMaster.AddSubscriber(bullet, PostMasterMessage.MessageType.ePlayerDied);
        PostMasterMessage.Message msgBullet;
        msgBullet.subscriber = me;
        msgBullet.type = PostMasterMessage.MessageType.ePlayerCollision;
        bool recievedMessage = bullet.ReciveMessage(msgBullet); //if it return false, set true
        Assert.IsFalse(recievedMessage);
    }
}
