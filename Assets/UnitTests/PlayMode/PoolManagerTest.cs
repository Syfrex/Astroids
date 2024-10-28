using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class PoolManagerTest
{

    [UnityTest]
    public IEnumerator PoolOneObject()
    {
        GameObject me = new GameObject();
        PoolManager poolManager = me.AddComponent<PoolManager>();
        poolManager.SetAsyncHandleThroughString("Assets/Prefabs/Player/Bullet.prefab");
        Time.timeScale = 1.0f; //Since I pause the game by timescale I need to activate it here 
        yield return new WaitForSeconds(1.0f);
        Bullet bulletOne = (Bullet)poolManager.GetAFreeObject();
        bulletOne.DeactivateObject();
        Bullet bulletTwo = (Bullet)poolManager.GetAFreeObject();//Since I deactivate it, it should return the same bullet twice doing this
        Assert.AreSame(bulletOne, bulletTwo);
    }
    [UnityTest]
    public IEnumerator PoolMultibleObject()
    {
        int amount = 2; //3, since poolmanager gives me one 
        GameObject me = new GameObject();
        PoolManager poolManager = me.AddComponent<PoolManager>();
        poolManager.SetAsyncHandleThroughString("Assets/Prefabs/Player/Bullet.prefab");
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(1);
        poolManager.AddPoolObjectBuffer(amount);
        Bullet bulletOne = (Bullet)poolManager.GetAFreeObject();
        Bullet bulletTwo = (Bullet)poolManager.GetAFreeObject();
        Bullet bulletThree = (Bullet)poolManager.GetAFreeObject();
        bulletOne.DeactivateObject();
        bulletTwo.DeactivateObject();
        bulletThree.DeactivateObject();
        Bullet bulletFour = (Bullet)poolManager.GetAFreeObject();
        Bullet bulletFive = (Bullet)poolManager.GetAFreeObject();
        Bullet bulletSix = (Bullet)poolManager.GetAFreeObject();

        Assert.AreSame(bulletOne, bulletFour);
        Assert.AreSame(bulletTwo, bulletFive);
        Assert.AreSame(bulletThree, bulletSix);
    }
}
