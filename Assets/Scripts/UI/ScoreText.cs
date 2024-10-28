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
        PostMaster.AddSubscriber(this, MessageType.eBulletCollision);
        PostMaster.AddSubscriber(this, MessageType.eWaveCleared);
        myScore = 0;
    }
    public void Init(TextMeshProUGUI aText)
    {
        myText = aText;
    }
    public bool ReciveMessage(Message aMessage)
    {
        switch (aMessage.type)
        {
            case MessageType.ePlayerCollision:
                break;
            case MessageType.eBulletCollision:
                myScore += 10;
                myText.SetText(myScore.ToString());
                return true;
            case MessageType.ePlayerDied:
                break;
            case MessageType.eWaveCleared:
                myScore += 100;
                return true;
            default:
                break;
        }
        return false;
    }

}
