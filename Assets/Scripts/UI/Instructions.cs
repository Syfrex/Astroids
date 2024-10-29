using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour, IObserver
{
    private bool myIsShowing = true;
    private void Start()
    {
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.ePause);
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.eRestart);
    }
    private void ShowInstruction()
    {
        myIsShowing = !myIsShowing; 
        if(myIsShowing == true) 
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public bool ReciveMessage(PostMasterMessage.Message aMessage)
    {
        switch (aMessage.type)
        {
            case PostMasterMessage.MessageType.ePause:
                ShowInstruction();
                return true;
            case PostMasterMessage.MessageType.eRestart:
                ShowInstruction();
                return true;
            default:
                break;
        }
        return false;
    }
}
