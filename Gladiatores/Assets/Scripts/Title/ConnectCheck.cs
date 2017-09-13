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
    private Image OneState;//state of 1P

    [SerializeField]
    private Image TwoState;//state of 2P

    [SerializeField]
    private Image playButton;//Play Button

    //1P,2PのAボタンを押したフラグ
    bool start1;
    bool start2;

    //スプライトの切り替え
    Texture2D textureBef;
    Texture2D textureAfter;

    Sprite spBef;
    Sprite spAf;

    //案内の点滅
    [SerializeField]
    private Text guide;

    // Use this for initialization
    void Start()
    {
        start1 = false;
        start2 = false;

        textureBef = Resources.Load("Textures/UI/Player_NotReady") as Texture2D;
        textureAfter = Resources.Load("Textures/UI/Player_Ready") as Texture2D;

        OneState = GameObject.Find("Canvas/SelectIcon/controller(1P)").GetComponent<Image>();
        TwoState = GameObject.Find("Canvas/SelectIcon/controller(2P)").GetComponent<Image>();

        spBef = Sprite.Create(textureBef, new Rect(0, 0, textureBef.width, textureBef.height), Vector2.zero);
        spBef = Sprite.Create(textureAfter, new Rect(0, 0, textureAfter.width, textureAfter.height), Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        //ガイドを点滅させる
        guide.color = new Color(guide.color.r, guide.color.g, guide.color.b, Mathf.PingPong(Time.time, 1f));

        //Input.ResetInputAxes();
        // 接続されているコントローラの名前を調べる
        var controllerNames = Input.GetJoystickNames();
        controllerNames.Initialize();

        //1P Player Login
        if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start, GamepadInput.GamePad.Index.One))
        {
            //OneState.color = new Color(OneState.color.r, OneState.color.g, OneState.color.b, 255f);
            OneState.sprite = spAf;
            singleFlag = true;
        }

        //2P Player Login
        if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start, GamepadInput.GamePad.Index.Two))
        {
            if (controllerNames.Length >= 2)
            {
                multiFlag = true;
            }
            //TwoState.color = new Color(TwoState.color.r, TwoState.color.g, TwoState.color.b, 255f);
            TwoState.sprite = spAf;
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
