using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour, IObserver
{
    private Image[] myLife;
    private int myLifeCount = 0;
    private int myStartLife;
    private void Start()
    {
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.ePlayerCollision);
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.eRestart);
    }
    public void Init(Image[] images)
    {
        myLife = images;
        myLifeCount = myLife.Length;
        myStartLife = myLifeCount;
    }
    public bool ReciveMessage(PostMasterMessage.Message aMessage)
    {
        switch (aMessage.type)
        {
            case PostMasterMessage.MessageType.ePlayerCollision:
                PlayerCollidedMessage();
                return true;
            case PostMasterMessage.MessageType.eRestart:
                Restart();
                return true;
            default:
                break;
        }
        return false;
    }
    private void PlayerCollidedMessage() 
    {
        myLifeCount--;
        myLife[myLifeCount].enabled = false;
        if (myLifeCount <= 0)
        {
            PostMasterMessage.Message msg;
            msg.subscriber = null;
            msg.type = PostMasterMessage.MessageType.ePlayerDied;
            PostMaster.SendMessage(msg);
            msg.type = PostMasterMessage.MessageType.eRestart;
            PostMaster.SendMessage(msg);
        }
    }
    private void Restart()
    {
        myLifeCount = myStartLife;
        for (int i = 0; i < myLifeCount; i++) 
        {
            myLife[i].enabled = true;   
        }
    }

}
