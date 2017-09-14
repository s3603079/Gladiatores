using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHealth : MonoBehaviour
{
    [SerializeField]
    Character character_;
    [SerializeField]
    Image healthGauge_; //  !<  体力を表示している
    
    //マックスの体力(スタート時のキャラクターの体力)
    int healthMax_;

    RectTransform rt_;

    //動きをつけた表示用
    int displayHealthPoint_;

    void Start()
    {
        Debug.Assert(character_, "Found Character Failed...");
        Debug.Assert(healthGauge_, "Found HealthGauge Image Failed...");
        healthMax_ = character_.Life;
        displayHealthPoint_ = healthMax_;// 体力を最大値にする

        rt_ = healthGauge_.GetComponent<RectTransform>();

    }

    void Update()
    {
        //プレイヤーの上に表示する
        transform.position = Camera.main.WorldToScreenPoint(character_.gameObject.transform.position + new Vector3(0f, 1.6f, 0f));
        
        if (displayHealthPoint_ != character_.Life)
        {// 体力の減少に動きをつける
            displayHealthPoint_ = (int)Mathf.Lerp(displayHealthPoint_, character_.Life, 0.05f);
        }

        //体力の表示
        float wid = Mathf.Clamp((displayHealthPoint_ / (float)healthMax_) * 95.0f, 0f, 95f);
        rt_.sizeDelta = new Vector2(wid, 6.0f);

    }
}