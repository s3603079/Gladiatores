using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class InputInfo : MonoBehaviour {

    [SerializeField]
    private GamePad.Index index;
    [SerializeField]
    private Gladiator player;

	void Update () {
        player.Walk(GamePad.GetAxis(GamePad.Axis.LeftStick, index).x);
        player.Jump(GamePad.GetButtonDown(GamePad.Button.A, index));
        player.RotaShoulder(GamePad.GetAxis(GamePad.Axis.RightStick, index));
        player.Attack(GamePad.GetTrigger(GamePad.Trigger.RightTrigger, index));
    }
}
