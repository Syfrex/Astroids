using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    InputKey playKey;
    void Start()
    {
        playKey = new InputKey(KeyCode.Return);
        Time.timeScale = 0.0f;
    }
    void Update()
    {
        if (playKey.IsKeyBeingPressed())
        {
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
        }
    }
}
