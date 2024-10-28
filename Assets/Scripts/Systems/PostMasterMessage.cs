using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PostMasterMessage
{
    public enum MessageType
    {
        ePlayerCollision,
        eBulletCollision,
        ePlayerDied,
        eWaveCleared,
        eRestart
    }
    public struct Message
    {
        public MessageType type;
        public GameObject subscriber;
    }
}
