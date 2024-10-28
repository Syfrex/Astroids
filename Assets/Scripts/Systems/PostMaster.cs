using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;


public static class PostMaster
{
    private static Dictionary<PostMasterMessage.MessageType, List<IObserver>> myObservers = new Dictionary<PostMasterMessage.MessageType, List<IObserver>>();
    public static void SendMessage(PostMasterMessage.Message aMessage)
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
    public static void AddSubscriber(IObserver anObserver, PostMasterMessage.MessageType aMessageType)
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
    //public static void UnSubscribe(IObserver observer) //Never ended up using this but keeping the logic here
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
