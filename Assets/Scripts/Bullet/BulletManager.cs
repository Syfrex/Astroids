using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Transform = UnityEngine.Transform;

public class BulletManager 
{
    private const float myBulletOffset = 0.3f;
    private PoolManager myBulletPool;
    private Transform myBulletSpawnTransform;
    private float myBulletSpeed;
    public BulletManager(PoolManager aBulletPool, float aPulletSpeed, Transform aTransform)
    {
        myBulletPool = aBulletPool;
        myBulletSpeed = aPulletSpeed;
        myBulletSpawnTransform = aTransform;
    }
    public void ShootBullet()
    {
        Bullet bullet = (Bullet)myBulletPool.GetAFreeObject();
        bullet.SetDirection(myBulletSpawnTransform.up);
        bullet.SetBulletSpeed(myBulletSpeed);
        Vector3 bulletPosition = myBulletSpawnTransform.position + (myBulletSpawnTransform.up * myBulletOffset);
        bullet.SetPositionAndRotation(bulletPosition, myBulletSpawnTransform.rotation);

    }
}
