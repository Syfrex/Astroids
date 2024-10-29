using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : IObserver
{
    private bool myIsPaused = true;
    public PauseGame() 
    {
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.ePause);
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.eRestart);
    }
    private void Pause()
    {
        myIsPaused = !myIsPaused;
        if (myIsPaused == true)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    } 
    public bool ReciveMessage(PostMasterMessage.Message aMessage)
    {
        switch (aMessage.type)
        {
            case PostMasterMessage.MessageType.eRestart:
                Pause();
                return true;
            case PostMasterMessage.MessageType.ePause:
                Pause();
                return true;
            default:
                break;
        }
        return false;
    }

}
