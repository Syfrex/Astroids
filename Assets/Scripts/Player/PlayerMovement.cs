using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement
{
    private Transform myPlayerTransform;
    private Vector3 myForceDirection = new Vector3();
    private float myMaxMovementSpeed;
    private float myRotationSpeed;
    private float myAcceleration;
    private float mySpeed;
    public PlayerMovement(Transform aPlayerTransform, float aRotationSpeed, float aMaxMovementSpeed, float aSpeed)
    {
        myForceDirection = Vector3.zero;
        myPlayerTransform = aPlayerTransform;
        myRotationSpeed = aRotationSpeed;
        myMaxMovementSpeed = aMaxMovementSpeed;
        mySpeed = aSpeed;
        myAcceleration = 0;
    }
    public void UpdateMovement()
    {
        MovePlayer();
    }
    public void MovePlayer()
    {
        myAcceleration = myAcceleration < 0 ? Mathf.Clamp(myAcceleration + Time.deltaTime, -myMaxMovementSpeed, myMaxMovementSpeed) : Mathf.Clamp(myAcceleration - Time.deltaTime, -myMaxMovementSpeed, myMaxMovementSpeed);
        myPlayerTransform.position += myForceDirection * myAcceleration * Time.deltaTime;
    }
    public void MoveForward() //made for testing
    {
        myAcceleration = Mathf.Clamp(myAcceleration + mySpeed * Time.deltaTime, -myMaxMovementSpeed, myMaxMovementSpeed);
        myForceDirection += (myPlayerTransform.up) * Time.deltaTime;
        myForceDirection = Vector3.ClampMagnitude(myForceDirection, myAcceleration);
    }
    public void MoveBackward() //made for testing
    {

        myAcceleration = Mathf.Clamp(myAcceleration + mySpeed * Time.deltaTime, -myMaxMovementSpeed, myMaxMovementSpeed);
        myForceDirection -= (myPlayerTransform.up)  * Time.deltaTime;
        myForceDirection = Vector3.ClampMagnitude(myForceDirection, myAcceleration);
    }
    public void RotateLeft()
    {
        myPlayerTransform.Rotate(new Vector3(0, 0, 1 * myRotationSpeed));
    }
    public void RotateRight()
    {
        myPlayerTransform.Rotate(new Vector3(0, 0, -1 * myRotationSpeed));
    }
    public void Reset()
    {
        myAcceleration = 0;
    }
}
