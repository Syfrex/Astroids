using UnityEngine;
public class InputKey
{
    private KeyCode myKey;
    public InputKey(KeyCode aKeyCode)
    {
        myKey = aKeyCode;
    }
    public bool IsKeyBeingPressed()
    {
        return Input.GetKeyDown(myKey);
    }
    public bool IsKeyPressed()
    {
        return Input.GetKey(myKey);
    }
}