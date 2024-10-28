using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IObserver
{
    private PoolManager myEnemyPool;
    private int myAmountOfEnemies;
    private int myEnemySpeed;
    private const float myAxisOffset = 1.0f;
    private int myEnemiesKilled;
    private int myWave;
    private void Start()
    {
        Invoke("AddEnemyPool", 0.5f);//I need to give the ascyn time to load
        Invoke("EnemiesSetup", 1.0f);//I should probably check the ascyn handle message for when done
    }
    public void Init(PoolManager aEnemyManager, int anAmountOfEnemies, int anEnemySpeed)
    {
        myEnemyPool = aEnemyManager;
        myAmountOfEnemies = anAmountOfEnemies;
        myEnemySpeed = anEnemySpeed;
        myEnemiesKilled = 0;
        myWave = 1;
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.eBulletCollision);
        PostMaster.AddSubscriber(this, PostMasterMessage.MessageType.eRestart);
    }
    private void AddEnemyPool()
    {
        myEnemyPool.AddPoolObjectBuffer(myAmountOfEnemies - 1); //-1 because my pooling always assure 1 object
    }
    private void Restart()
    {
        myAmountOfEnemies /= myWave;
        myEnemyPool.ReturnAllObjects();
        NewWave();
    }
    private void EnemiesSetup()
    {
        for (int i = 0; i < myAmountOfEnemies; i++)
        {
            Enemy enemy = (Enemy)myEnemyPool.GetAFreeObject();
            enemy.SetPosition(GenerateRandomSpawnPosition());
            enemy.SetDirection(GenerateRandomDirection());
            enemy.SetEnemySpeed(myEnemySpeed);
            enemy.transform.localScale = Vector3.one * 3;
        }
    }
    private Vector3 GenerateRandomSpawnPosition()
    {
        float minAxisX = Global.WorldBounds.min.x  + myAxisOffset;
        float maxAxisX = Global.WorldBounds.max.x  - myAxisOffset;
        float minAxisY = Global.WorldBounds.min.y  + myAxisOffset;
        float maxAxisY = Global.WorldBounds.max.y  - myAxisOffset;

        float randomPositionOnX = Random.Range(minAxisX, maxAxisX);
        float randomPositionOnY = Random.Range(minAxisY, maxAxisY);
        float[] randomXAxis = { minAxisX, maxAxisX };
        float[] randomYAxis = { minAxisY, maxAxisY };

        int randomAxis = Random.Range(0, 2);
        Vector3[] randomSpawnPositionOnAxis = { new Vector3(randomPositionOnX, randomYAxis[randomAxis], 0), new Vector3(randomXAxis[randomAxis], randomPositionOnY, 0) };

        int randomPosition = Random.Range(0, 2);

        return randomSpawnPositionOnAxis[randomPosition];
    }
    private Vector3 GenerateRandomDirection()
    {
        float minAxisX = Global.WorldBounds.min.x  + myAxisOffset;
        float maxAxisX = Global.WorldBounds.max.x  - myAxisOffset;
        float minAxisY = Global.WorldBounds.min.y  + myAxisOffset;
        float maxAxisY = Global.WorldBounds.max.y  - myAxisOffset;
        float randomDirectionX = Random.Range(minAxisX, maxAxisX);
        float randomDirectionY = Random.Range(minAxisY, maxAxisY);
        return new Vector3(randomDirectionX, randomDirectionY, 0).normalized;
    }
    private void NewWave()
    {
        myWave++;
        EnemiesSetup();
    }

    private void EnemyCollision(PostMasterMessage.Message aMessage)
    {
        myEnemiesKilled++;
        if (myEnemiesKilled >= myAmountOfEnemies * 7 * myWave) //hardcoded: 1 becomes 2 becomes 4 so once 7 is dead, 1 original enemy is killed
        {
            PostMasterMessage.Message msg;
            msg.subscriber = gameObject;
            msg.type = PostMasterMessage.MessageType.eWaveCleared;
            PostMaster.SendMessage(msg);
            myAmountOfEnemies *= 2;
            NewWave();
        }
        Enemy enemy = aMessage.subscriber.GetComponent<Enemy>();
        enemy.DeactivateObject();
        if (enemy.GameObject.transform.localScale.x > 1)
        {
            Vector3 direction = enemy.GetDirection();
            Vector3 position = enemy.GetPosition();
            Vector3 scale = enemy.GameObject.transform.localScale;
            SplitEnemy(position, direction, scale);
        }
        else
        {
            enemy.DeactivateObject();
        }
    }
    private void SplitEnemy(Vector3 aPosition, Vector3 aDirection, Vector3 aScale)
    {
        myEnemyPool.AddPoolObject();
        int[] leftOrRight = { 1, -1 };
        for (int i = 0; i < 2; i++)
        {
            Enemy enemy = (Enemy)myEnemyPool.GetAFreeObject();
            enemy.GameObject.transform.localScale = aScale;
            enemy.GameObject.transform.localScale -= Vector3.one;
            enemy.SetPosition(aPosition);
            Vector3 newDirection = aDirection + (enemy.gameObject.transform.right * leftOrRight[i]);
            enemy.SetDirection(newDirection.normalized);
            enemy.SetEnemySpeed(myEnemySpeed * 1.2f);
        }
    }
    public bool ReciveMessage(PostMasterMessage.Message aMessage)
    {
        switch (aMessage.type)
        {
            case PostMasterMessage.MessageType.eRestart:
                Restart();
                return true;
            case PostMasterMessage.MessageType.eBulletCollision:
                EnemyCollision(aMessage);
                return true;
            default:
                break;
        }
        return false;
    }
}
