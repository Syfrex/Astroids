using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{
    [Test]
    public void PlayerSetAndGetPosition()
    {
        GameObject me = new GameObject();
        Player player = me.AddComponent<Player>();
        Vector3 position = player.gameObject.transform.position;
        Vector3 newPosition = position + player.gameObject.transform.forward;
        player.SetPosition(newPosition);
        Assert.AreEqual(newPosition, player.GetPosition());
    }
    [Test]
    public void PlayerDeath()
    {
        GameObject me = new GameObject();
        Player player = me.AddComponent<Player>();
        PostMaster.AddSubscriber(player, MessageType.ePlayerDied);
        Message msgPlayerDead;
        msgPlayerDead.subscriber = me;
        msgPlayerDead.type = MessageType.ePlayerDied;
        player.ReciveMessage(msgPlayerDead);
        bool isAlive = me.gameObject.activeSelf;
        Assert.IsFalse(isAlive);
    }
}
