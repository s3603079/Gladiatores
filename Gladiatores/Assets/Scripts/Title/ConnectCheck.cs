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
    private bool start1;
    private bool start2;

    //スプライトの切り替え
    [SerializeField]
    private Sprite textureBef;
    [SerializeField]
    private Sprite textureAfter;

    //Sprite spBef;
    //Sprite spAf;

    //案内の点滅
    [SerializeField]
    private Text guide;

    // Use this for initialization
    void Start()
    {
        // 観客席の声援をBGMで再生
        SoundManager.Instance.PlayBGM("audienceCheer");
    }

    // Update is called once per frame
    void Update()
    {
        //ガイドを点滅させる
        guide.color = new Color(guide.color.r, guide.color.g, guide.color.b, Mathf.PingPong(Time.time, 1f));

        ////Input.ResetInputAxes();
        //// 接続されているコントローラの名前を調べる
        //var controllerNames = Input.GetJoystickNames();
        //controllerNames.Initialize();

        ////1P Player Login
        if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start, GamepadInput.GamePad.Index.One))
        {
            // 待機状態の切り替え
            start1 = !start1;
            if(start1)
            {
                OneState.sprite = textureAfter;
                SoundManager.Instance.PlaySE("Ready");
            }
            else
            {
                OneState.sprite = textureBef;
                SoundManager.Instance.PlaySE("NotReady");
            }
            OneState.sprite = (start1) ? textureAfter : textureBef;
        }
        //if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start, GamepadInput.GamePad.Index.One))
        //{
        //    //OneState.color = new Color(OneState.color.r, OneState.color.g, OneState.color.b, 255f);
        //    OneState.sprite = spAf;
        //    singleFlag = true;
        //}

        ////2P Player Login
        if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start, GamepadInput.GamePad.Index.Two))
        {
            // 待機状態の切り替え
            start2 = !start2;
            if (start2)
            {
                TwoState.sprite = textureAfter;
                SoundManager.Instance.PlaySE("Ready");
            }
            else
            {
                TwoState.sprite = textureBef;
                SoundManager.Instance.PlaySE("NotReady");
            }
        }
        //if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start, GamepadInput.GamePad.Index.Two))
        //{
        //    if (controllerNames.Length >= 2)
        //    {
        //        multiFlag = true;
        //    }
        //    //TwoState.color = new Color(TwoState.color.r, TwoState.color.g, TwoState.color.b, 255f);
        //    TwoState.sprite = spAf;
        //}


        //if (singleFlag == true)//1Pがエントリーしていたら
        //{
        //    //Display PlayButton(プレイボタンをアクティブにする)
        //    playButton.color = new Color(playButton.color.r, playButton.color.g, playButton.color.b, 255f);
        //}

        ////Both player push "A". ->StartMultiPlay
        //if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.A, GamepadInput.GamePad.Index.One)) start1 = true;
        //if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.A, GamepadInput.GamePad.Index.Two)) start2 = true;

        // プレイボタンのアクティブを開始フラグで決める
        playButton.gameObject.SetActive((start1 || start2) ? true : false);

        // ボタンが押されることでゲームを開始（フラグに応じて分岐）
        if ((GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.A, GamepadInput.GamePad.Index.One)
         || (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.A, GamepadInput.GamePad.Index.Two))))
            {
            if (start1 && start2)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("arenaMulti");
                GameManager.Instance.oneIndex = GamepadInput.GamePad.Index.One;
                GameManager.Instance.twoIndex = GamepadInput.GamePad.Index.Two;
            }
            else if (start1 || start2)
            {
                SoundManager.Instance.PlaySE("metalClash");
                UnityEngine.SceneManagement.SceneManager.LoadScene("arenaSingle");
                if (start1)
                {
                    GameManager.Instance.oneIndex = GamepadInput.GamePad.Index.One;
                }
                if(start2)
                {
                GameManager.Instance.oneIndex = GamepadInput.GamePad.Index.Two;
                }
            }
        }

        ////シングルプレイ
        //if (singleFlag == false || multiFlag == false)
        //{
        //    if (start1 == true)
        //    {
        //        Debug.Log("SINGLE");
        //        UnityEngine.SceneManagement.SceneManager.LoadScene("arenaSingle");
        //    }
        //}
        //else//マルチプレイ
        //{
        //    if (start1 == true && start2 == true)
        //    {
        //        Debug.Log("MULTI");
        //        UnityEngine.SceneManagement.SceneManager.LoadScene("arenaMulti");
        //    }
        //}
    }
}
