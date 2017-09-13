using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    //HPゲージを表示するキャラクター
    //[SerializeField]
    //Character character;
    //体力を表示している
    [SerializeField]
    private Image healthGauge;

    //マックスの体力(スタート時のキャラクターの体力)
    private int healthMax;

    //幅を変更する(HPの減少に伸縮を使用しないため)
    RectTransform rt;

    //動きをつけた表示用
    private int displayHealthPoint;

    // Use this for initialization
    void Start()
    {
        //--------------------------------------------
        //※ここを変更してプレイヤーのステータスをもらう
        //体力
        //healthMax = character.Life;
        //--------------------------------------------
        displayHealthPoint = healthMax;

        rt = healthGauge.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの上に表示する
        //this.transform.position = Camera.main.WorldToScreenPoint(character.gameObject.transform.position + new Vector3(0f, 1.6f, 0f));

        //体力の減少に動きをつける
        //if (displayHealthPoint != character.Life)
        //{
        //    displayHealthPoint = (int)Mathf.Lerp(displayHealthPoint, character.Life, 0.05f);
        //}

        //体力の表示
        float wid = Mathf.Clamp(((float)displayHealthPoint / (float)healthMax) * 95.0f, 0f, 95f);
        rt.sizeDelta = new Vector2(wid, 6.0f);
    }
}