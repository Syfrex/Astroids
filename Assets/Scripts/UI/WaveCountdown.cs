using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCountdown : MonoBehaviour, IObserver
{
    private TextMeshProUGUI myText;
    private Transform myUIButton;
    private int myCountdown;
    private void Start()
    {
        PostMaster.AddSubscriber(this, MessageType.eWaveCleared);
        myCountdown = 3;
    }
    public void Init(TextMeshProUGUI aText, Transform anUIButton)
    {
        myText = aText;
        myUIButton = anUIButton;
    }
    public bool ReciveMessage(Message aMessage)
    {
        switch (aMessage.type)
        {
            case MessageType.ePlayerCollision:
                break;
            case MessageType.eBulletCollision:
                break;
            case MessageType.ePlayerDied:
                break;
            case MessageType.eWaveCleared:
                myUIButton.parent.gameObject.SetActive(true);
                myText.SetText(myCountdown.ToString());
                StartCoroutine("NextWave");
                return true;
            default:
                break;
        }
        return false;
    }

    IEnumerator NextWave()
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(1);
        myCountdown--;
        myText.SetText(myCountdown.ToString());
        yield return new WaitForSecondsRealtime(1);
        myCountdown--;
        myText.SetText(myCountdown.ToString());
        yield return new WaitForSecondsRealtime(1);
        myUIButton.parent.gameObject.SetActive(false);
        Time.timeScale = 1.0f;

    }
}
