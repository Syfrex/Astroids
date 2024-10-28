using UnityEngine;

public class PlayerController
{
    private InputKey myForward;
    private InputKey myBackward;
    private InputKey myLeft;
    private InputKey myRight;
    private InputKey myShoot;
    public PlayerController(
        KeyCode aForward = KeyCode.W, 
        KeyCode aBackward = KeyCode.S, 
        KeyCode aLeft = KeyCode.A, 
        KeyCode aRight = KeyCode.D, 
        KeyCode aShoot = KeyCode.Space )
    {
        myForward   = new InputKey(aForward);
        myBackward  = new InputKey(aBackward);
        myLeft      = new InputKey(aLeft);
        myRight     = new InputKey(aRight);
        myShoot     = new InputKey(aShoot);
    }
    public bool GetForward()
    {
        return myForward.IsKeyPressed();
    }
    public bool GetBackward()
    {
        return myBackward.IsKeyPressed();
    }
    public bool GetLeft()
    {
        return myLeft.IsKeyPressed();
    }
    public bool GetRight()
    {
        return myRight.IsKeyPressed();
    }
    public bool GetShoot()
    {
        return myShoot.IsKeyBeingPressed();
    }
}
