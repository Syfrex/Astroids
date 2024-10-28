using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour, IObserver
{
    private Image[] myLife;
    private int myLifeCount = 0;
    private void Start()
    {
        PostMaster.AddSubscriber(this, MessageType.ePlayerCollision);
    }
    public void Init(Image[] images)
    {
        myLife = images;
        myLifeCount = myLife.Length;
    }
    public bool ReciveMessage(Message aMessage)
    {
        switch (aMessage.type)
        {
            case MessageType.ePlayerCollision:
                HandleMessage();
                return true;
            case MessageType.eBulletCollision:
                break;
            case MessageType.ePlayerDied:
                break;
            case MessageType.eWaveCleared:
                break;
            default:
                break;
        }
        return false;
    }
    private void HandleMessage() 
    {
        myLifeCount--;
        myLife[myLifeCount].enabled = false;
        if (myLifeCount <= 0)
        {
            Message msg;
            msg.subscriber = null;
            msg.type = MessageType.ePlayerDied;
           
            PostMaster.SendMessage(msg);

        }
    }

}
