using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class GameManager : SingletonMonoBehaviour<GameManager> {

    public GamePad.Index oneIndex;
    public GamePad.Index twoIndex;

    protected override void Awake() {
        base.Awake();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }
}
