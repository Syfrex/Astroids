using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
[System.Serializable]
public struct PlayerStats
{
    public float maxShootCooldown;
    public float maxMovementSpeed;
    public float rotationSpeed;
    public float speed;
    public float bulletSpeed;
}

public class Player : MonoBehaviour, IWorldBoundObject, IObserver
{
    private PlayerStats myStats;

    private PlayerController myController;
    private PlayerMovement myPlayerMovement;
    private BulletManager myBulletManager;
    private float myShootCooldown;
    InputKey myPlayKey;

    public void Init(PoolManager aBulletPool, PlayerStats aPlayerStats)
    {
        myStats = aPlayerStats;
        myBulletManager = new BulletManager(aBulletPool, myStats.bulletSpeed, transform);
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.ePlayerDied);
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.eRestart);
    }
    void Start()
    {
        myPlayKey = new InputKey(KeyCode.Escape);
        AddMyselfToWorldObjects();
        myController = new PlayerController(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.Space);
        myPlayerMovement = new PlayerMovement(this.transform, myStats.rotationSpeed, myStats.maxMovementSpeed, myStats.speed);

        myShootCooldown = 0;
    }
    void Update()
    {
        if (myController.GetForward())
        {
            myPlayerMovement.MoveForward();
        }
        if (myController.GetBackward())
        {
            myPlayerMovement.MoveBackward();
        }
        if (myController.GetLeft())
        {
            myPlayerMovement.RotateLeft();
        }
        if (myController.GetRight())
        {
            myPlayerMovement.RotateRight();
        }
        if (myController.GetShoot() && myShootCooldown <= 0.0)
        {
            myShootCooldown = myStats.maxShootCooldown;
            myBulletManager.ShootBullet();
        }
        if(myPlayKey.IsKeyBeingPressed())
        {
            PostMasterMessage.Message msg;
            msg.subscriber = gameObject;
            msg.type = PostMasterMessage.MessageType.ePause;
            PostMaster.SendMessage(msg);
        }
        myPlayerMovement.UpdateMovement();
        _ = myShootCooldown > 0.0f ? myShootCooldown -= Time.deltaTime : myShootCooldown = 0;
    }


    public void SetPosition(Vector3 aPosition)
    {
        transform.position = aPosition;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void AddMyselfToWorldObjects()
    {
        Global.AddWorldBoundObject(this);
    }
    public bool ReciveMessage(PostMasterMessage.Message aMessage)
    {
        switch (aMessage.type)
        {
            case PostMasterMessage.MessageType.eRestart:
                gameObject.SetActive(true);
                SetPosition(new Vector3(0,0,0)); //Idealy save start position and spawn player there
                myPlayerMovement.Reset();
                return true;
            case PostMasterMessage.MessageType.ePlayerDied:
                gameObject.SetActive(false);
                return true;

            default:
                break;
        }
        return false;

    }
}
