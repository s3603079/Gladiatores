using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GamepadInput.GamePad;
//For Debug
using UnityEngine.UI;

public class ConnectCheck : MonoBehaviour {

    private bool singleFlag;//Flag of play single

    private bool multiFlag;//Flag of play multi

    [SerializeField]
    private Text OneState;//state of 1P

    [SerializeField]
    private Text TwoState;//state of 2P

    [SerializeField]
    private Text playButton;//Play Button

    //1P,2PのAボタンを押したフラグ
    bool start1;
    bool start2;

    // Use this for initialization
    void Start()
    {
        start1 = false;
        start2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Input.ResetInputAxes();
        // 接続されているコントローラの名前を調べる
        var controllerNames = Input.GetJoystickNames();
        controllerNames.Initialize();

        //1P Player Login
        if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start, GamepadInput.GamePad.Index.One))
        {
            OneState.color = new Color(OneState.color.r, OneState.color.g, OneState.color.b, 255f);
            singleFlag = true;
        }

        //2P Player Login
        if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start, GamepadInput.GamePad.Index.Two))
        {
            if (controllerNames.Length >= 2)
            {
                multiFlag = true;
            }
            TwoState.color = new Color(TwoState.color.r, TwoState.color.g, TwoState.color.b, 255f);
        }


        if (singleFlag == true)//1Pがエントリーしていたら
        {
            //Display PlayButton(プレイボタンをアクティブにする)
            playButton.color = new Color(playButton.color.r, playButton.color.g, playButton.color.b, 255f);
        }

        //Both player push "A". ->StartMultiPlay
        if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.A, GamepadInput.GamePad.Index.One)) start1 = true;
        if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.A, GamepadInput.GamePad.Index.Two)) start2 = true;

        //シングルプレイ
        if (singleFlag == false || multiFlag == false)
        {
            if (start1 == true)
            {
                Debug.Log("SINGLE");
            }
        }
        else//マルチプレイ
        {
            if (start1 == true && start2 == true)
            {
                Debug.Log("MULTI");
            }
        }
    }
}
