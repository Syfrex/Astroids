using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour, IObserver
{
    private TextMeshProUGUI myText;
    private int myScore;
    private void Start()
    {
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.eBulletCollision);
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.eWaveCleared);
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.eRestart);
        myScore = 0;
    }
    public void Init(TextMeshProUGUI aText)
    {
        myText = aText;
    }
    public bool ReciveMessage(PostMasterMessage.Message aMessage)
    {
        switch (aMessage.type)
        {
            case PostMasterMessage.MessageType.eRestart:
                myScore = 0;
                myText.SetText(myScore.ToString());
                return true;
            case PostMasterMessage.MessageType.eBulletCollision:
                myScore += 10;
                myText.SetText(myScore.ToString());
                return true;
            case PostMasterMessage.MessageType.eWaveCleared:
                myScore += 100;
                myText.SetText(myScore.ToString());
                return true;
            default:
                break;
        }
        return false;
    }

}
