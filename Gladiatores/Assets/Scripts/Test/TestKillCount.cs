using UnityEngine;
using UnityEngine.UI;

public class TestKillCount : MonoBehaviour
{
    int now_ = 1;

    [SerializeField]
    int killCount_ = 0;
    [SerializeField]
    int maxKillCount_ = 100;

    [SerializeField]
    Slider slider;
    [SerializeField]
    Text killText;

    [SerializeField]
    bool isInverted;//ゲージの反転処理


    public int KillCount
    {
        get { return killCount_; }
        set { killCount_ = value; }
    }

    void Start()
    {
        slider.maxValue = maxKillCount_;
        slider.value = (isInverted) ? 100 : slider.value = 0;
    }

    void Update()
    {
        killCount_ = Mathf.Clamp(killCount_, 0, maxKillCount_);
        //指定した番号ごとにゲージをリセット
        slider.value = (isInverted) ? 100 - killCount_ : killCount_;
     
        //アイコン上に討伐数を表示
        killText.text = killCount_.ToString();
    }

}
