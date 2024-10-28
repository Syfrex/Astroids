using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolObject, IWorldBoundObject
{
    private bool myIsBeingUsed = false;
    private Vector3 myDirection;
    private float mySpeed;
    public GameObject GameObject => gameObject;
    public Enemy()
    {
        AddMyselfToWorldObjects();
    }
    public bool IsBeingUsed()
    {
        return myIsBeingUsed;
    }
    public void ActivateObject()
    {
        myIsBeingUsed = true;
        GameObject.SetActive(true);
    }
    public void Update()
    {
        transform.position += myDirection * Time.deltaTime * mySpeed;
    }
    public void SetDirection(Vector3 aDirection)
    {
        myDirection = aDirection;
    }
    public Vector3 GetDirection()
    {
        return myDirection;
    }
    public void SetEnemySpeed(float aSpeed)
    {
        mySpeed = aSpeed;
    }
    public void DeactivateObject()
    {
        myIsBeingUsed = false;
        GameObject.SetActive(false);
    }
    public void SetPosition(Vector3 aPosition)
    {
        GameObject.transform.position = aPosition;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
    public void AddMyselfToWorldObjects()
    {
        Global.AddWorldBoundObject(this);
    }
    private void OnTriggerEnter2D(Collider2D aCollider) //this is not what I wanted to do (created my own collsion-checker that bloated the entire project)
    {
        PostMasterMessage.Message msg;
        msg.subscriber = gameObject;
        if (aCollider.gameObject.GetComponent<Bullet>())
        {
            msg.type = PostMasterMessage.MessageType.eBulletCollision;
            if (aCollider.gameObject.GetComponent<Bullet>().IsBeingUsed())
            {
                PostMaster.SendMessage(msg);
            }
        }
        if (aCollider.gameObject.GetComponent<Player>())
        {
            msg.type = PostMasterMessage.MessageType.ePlayerCollision;
            PostMaster.SendMessage(msg);
        }
    }
}
