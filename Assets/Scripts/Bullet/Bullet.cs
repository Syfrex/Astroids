using UnityEngine;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour, IPoolObject, IObserver
{
    private bool myIsBeingUsed = false;
    private Vector3 myDirection;
    private float mySpeed;
    private float myLifeTime;
    public GameObject GameObject => gameObject;
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
        myLifeTime -= Time.deltaTime;
        if (myLifeTime < 0)
        {
            DeactivateObject();
        }
    }
    public void SetPositionAndRotation(Vector3 aPosition, Quaternion aRotation)
    {
        GameObject.transform.SetPositionAndRotation(aPosition, aRotation);
    }
    public void SetDirection(Vector3 aDirection)
    {
        myDirection = aDirection;
    }
    public void SetBulletSpeed(float aSpeed)
    {
        mySpeed = aSpeed;
        //I know the single responisibility principle but I want to base the
        //lifetime of the bullet of the speed, so if the bullets are super slow,
        //it should have time to leave the screen before deactivate it
        float lifeTime = 50.0f / mySpeed;
        SetLifeTime(lifeTime);
    }
    public void SetLifeTime(float aLifeTime)
    {
        myLifeTime = aLifeTime;

    }
    public void DeactivateObject()
    {
        //PostMaster.UnSubscribe(this);  - known bug
        myIsBeingUsed = false;
        GameObject.SetActive(false);
    }
    public bool ReciveMessage(PostMasterMessage.Message aMessage)
    {
        switch (aMessage.type)
        {
            case PostMasterMessage.MessageType.eBulletCollision:
                DeactivateObject();
                return true;
        }
        return false;
    }
}
