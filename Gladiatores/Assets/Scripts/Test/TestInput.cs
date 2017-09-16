using UnityEngine;
using System.Collections;
using GamepadInput;

public class TestInput : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        var keyState = GamePad.GetState(GamePad.Index.One);
        Debug.Log(keyState.RightTrigger);

    }
}