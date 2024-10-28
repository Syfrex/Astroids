using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public enum MessageType
{
    ePlayerCollision,
    eBulletCollision,
    ePlayerDied,
    eWaveCleared
}
public struct Message
{
    public MessageType type;
    public GameObject subscriber;
}
public static class PostMaster
{
    private static Dictionary<MessageType, List<IObserver>> myObservers = new Dictionary<MessageType, List<IObserver>>();
    public static void SendMessage(Message aMessage)
    {
        //Debug.Log(message.type);
        foreach (var msgType in myObservers)
        {
            if (msgType.Key ==  aMessage.type)
            {
                foreach (var obs in msgType.Value)
                {
                    obs.ReciveMessage(aMessage);
                }
                return;
            }
        }
    }
    public static void AddSubscriber(IObserver anObserver, MessageType aMessageType)
    {
        foreach (var msgType in myObservers)
        {
            if (msgType.Key == aMessageType)
            {
                msgType.Value.Add(anObserver);
                return;
            }
        }
        myObservers.Add(aMessageType, new List<IObserver>() { anObserver });
    }
    //public static void UnSubscribe(IObserver observer)
    //{
    //    foreach (var observerList in myObservers.Values)
    //    {
    //        if (observerList.Contains(observer))
    //        {
    //            observerList.Remove(observer);
    //        }
    //    }
    //}
}
