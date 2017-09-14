using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class Cursor : MonoBehaviour {

    [SerializeField]
    private RectTransform[] positions;
    [SerializeField]
    private string[] scenes;

    private Vector3 offset;
    private float coolTime;
    private int selectNumber;

	// Use this for initialization
	void Start () {
        offset = transform.localPosition;
	}

    // Update is called once per frame
    void Update()
    {
        // 操作不可の時間はここにて終了
        if (coolTime++ <= 30F) return;

        // 左スティックの入力値を取得
        var axis = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Any);

        // 入力値と絶対値の差分から番号の変更
        if (Vector2.Distance(axis, Vector2.up) <= 0F)
        {
            coolTime = 0F;
            selectNumber--;
            SoundManager.Instance.PlaySE("cursorMovement");
        }
        if (Vector2.Distance(axis, Vector2.down) <= 0F)
        {
            coolTime = 0;
            selectNumber++;
            SoundManager.Instance.PlaySE("cursorMovement");
        }
        // 配列をオーバーしないための処理
        if (selectNumber < 0) selectNumber = positions.Length - 1;
        if (selectNumber > positions.Length - 1) selectNumber = 0;

        // カーソルを選択肢の位置に合わせる
        transform.localPosition = offset + positions[selectNumber].localPosition;

        // シーン遷移
        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Any))
        {
            SoundManager.Instance.PlaySE("cursorDecide");
            if (string.Equals(scenes[selectNumber], ""))
            {
                Debug.Log("Quit");
                Application.Quit();
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(scenes[selectNumber]);
            }
        }
    }
}
