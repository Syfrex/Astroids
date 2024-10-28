using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTest
{
 
    [UnityTest]
    public IEnumerator PlayerMovementForward()
    {
        GameObject me = new GameObject();
        Player player = me.AddComponent<Player>();
        PlayerMovement playerMovement = new PlayerMovement(player.transform, 0.3f, 20.0f, 10.0f);
        Vector3 lastPosition = player.transform.position;
        playerMovement.MoveForward();
        yield return null;
        bool moved = player.transform.forward == lastPosition;
        Assert.IsFalse(moved);
    }
    [UnityTest]
    public IEnumerator PlayerMovementBackward()
    {
        GameObject me = new GameObject();
        Player player = me.AddComponent<Player>();
        PlayerMovement playerMovement = new PlayerMovement(player.transform, 0.3f, 20.0f, 10.0f);
        Vector3 lastPosition = player.transform.position;
        playerMovement.MoveBackward();
        yield return null;
        bool moved = player.transform.forward == lastPosition;
        Assert.IsFalse(moved);
    }
    [UnityTest]
    public IEnumerator PlayerRotatedLeft()
    {
        GameObject me = new GameObject();
        Player player = me.AddComponent<Player>();
        PlayerMovement playerMovement = new PlayerMovement(player.transform, 0.3f, 20.0f, 10.0f);
        Vector3 lastDirection = player.transform.up;
        playerMovement.RotateLeft();
        Vector3 rotatedDirection = player.transform.up;
        
        yield return null;
        bool rotated = rotatedDirection == player.transform.up;
        bool oldRotation = lastDirection == player.transform.up;
        Assert.IsTrue(rotated);
        Assert.IsFalse(oldRotation);
    }
    [UnityTest]
    public IEnumerator PlayerRotatedRight()
    {
        GameObject me = new GameObject();
        Player player = me.AddComponent<Player>();
        PlayerMovement playerMovement = new PlayerMovement(player.transform, 0.3f, 20.0f, 10.0f);
        Vector3 lastDirection = player.transform.up;
        playerMovement.RotateRight();
        Vector3 rotatedDirection = player.transform.up;

        yield return null;
        bool rotated = rotatedDirection == player.transform.up;
        bool oldRotation = lastDirection == player.transform.up;
        Assert.IsTrue(rotated);
        Assert.IsFalse(oldRotation);
    }
}
