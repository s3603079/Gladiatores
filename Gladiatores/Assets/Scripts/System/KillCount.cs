using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCount : MonoBehaviour {
    //殺した数
    private int killNumber = 0;
    //目標となる殺人数
    [SerializeField]
    private int maxKillCount = 100;
    //キルカウントのスライダー(UI)
    [SerializeField]
    private Slider slider;
    //殺した数(UI)
    [SerializeField]
    private Text killText;
    //ゲージの反転処理
    [SerializeField]
    private bool isInverted;

    public int KillCounts
    {
        get { return killNumber; }
        set { killNumber = value; }
    }

    // Use this for initialization
    void Start()
    {
        slider.maxValue = maxKillCount;
        slider.value = (isInverted) ? 100 : slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {

        killNumber = Mathf.Clamp(killNumber, 0, maxKillCount);
        //指定した番号ごとにゲージをリセット
        slider.value = (isInverted) ? maxKillCount - killNumber : killNumber;

        //アイコン上に討伐数を表示
        killText.text = killNumber.ToString();

    }

    public void AddKillCount()
    {
        killNumber++;
    }
}
