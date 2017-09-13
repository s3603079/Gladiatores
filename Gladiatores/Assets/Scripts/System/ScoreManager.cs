using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    //時間管理クラス　スコア算出のために使用
    class Timer : MonoBehaviour
    {
        float startTime_;

        void Start()
        {
            startTime_ = Time.time;
        }

        public float GetTime()
        {
            return Time.time - startTime_;
        }

        public void ResetStartTime()
        {
            startTime_ = Time.time;
        }
    }

    //経過時間
    private Timer timer = new Timer();

    //キルカウント
    [SerializeField]
    KillCount PlayerKillCount;

    //スコア(８桁)
    int score = 0;

    //動きをつけた表示用
    private int displayScore;

    //スコアのテキスト
    [SerializeField]
    [Tooltip("Use Only Result")]
    private Text _scoreText;

    //スコアの取得
    public int Score
    {
        get { return score; }
    }
    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start()
    {
        displayScore = score;
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterManager.Instance.Enemy.Life <= 0)//敵が死んだら
        {
            AddScore();//スコアを入手
        }
    }

    public void AddScore()
    {
        //キルカウントの加算
        PlayerKillCount.AddKillCount();

        //スコアの加算
        /*長時間生き残ると、スコアが高い*/
        score = (int)timer.GetTime() * 10 + PlayerKillCount.KillCounts * 100;
    }

    public void DrawScore()
    {
        //スコアの表示
        if (displayScore != score)
        {
            displayScore = (int)Mathf.Lerp(displayScore, score, 0.1f);
        }

        _scoreText.text = string.Format("{0:00000000}", displayScore);
    }
}