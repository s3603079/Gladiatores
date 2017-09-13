using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;

public class GameManager : SingletonMonoBehaviour<GameManager> {

    [SerializeField]
    private Text time;
    [SerializeField]
    private Gladiator player1;
    [SerializeField]
    private Gladiator player2;

    protected override void Awake() {
        base.Awake();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        SettingGladiator(player1, GamePad.Index.One);
        //SettingGladiator(player2, GamePad.Index.Two);
    }

    void SettingGladiator(Gladiator player, GamePad.Index index) {
        if (player == null) return;
        player.Walk(GamePad.GetAxis(GamePad.Axis.LeftStick, index).x);
        player.Jump(GamePad.GetButtonDown(GamePad.Button.A, index));
        player.RotaShoulder(GamePad.GetAxis(GamePad.Axis.RightStick, index));
        player.Attack(GamePad.GetTrigger(GamePad.Trigger.RightTrigger, index));
    }
}
