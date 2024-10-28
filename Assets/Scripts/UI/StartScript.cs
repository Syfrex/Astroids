using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour, IObserver
{
    InputKey playKey;
    void Start()
    {
        PauseStart();
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.eRestart);
    }
    void PauseStart()
    {
        playKey = new InputKey(KeyCode.Return);
        Time.timeScale = 0.0f;
    }
    void Update()
    {
        if (playKey.IsKeyBeingPressed())
        {
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
        }
    }
    public bool ReciveMessage(PostMasterMessage.Message aMessage)
    {
        switch (aMessage.type)
        {
            case PostMasterMessage.MessageType.eRestart:
                PauseStart();
                gameObject.SetActive(true);
                return true;
            default:
                break;
        }
        return false;
    }
}
