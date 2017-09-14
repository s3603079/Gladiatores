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

    [SerializeField]
    [Tooltip("1P用のKillCount")]
    private KillCount playerKillCount;

    [SerializeField]
    [Tooltip("2P用のKillCount")]
    private KillCount scondKillCount;

    //スコア(８桁)
    int score = 0;

    //動きをつけた表示用
    private int displayScore;

    //(シングル専用)スコアのテキスト
    [SerializeField]
    [Tooltip("Use Only Single Result")]
    private Text scoreText;

    //(マルチ専用)1P用killCount
    [SerializeField]
    [Tooltip("Use Only Multi Result")]
    private Text playerKillNum;

    //(マルチ専用)2P用killCount
    [SerializeField]
    [Tooltip("Use Only Multi Result")]
    private Text secondKillNum;

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
        playerKillCount = CharacterManager.Instance.PlayerList[0].gameObject.GetComponent<KillCount>();
        displayScore = score;

    }

    // Update is called once per frame
    void Update()
    {
        //if (CharacterManager.Instance.Enemy.Life <= 0)//敵が死んだら
        //{
        //    AddScore();//スコアを入手
        //}
    }

    public void SingleScore()
    {
        //キルカウントの加算
        playerKillCount.KillCounts++;

        //スコアの加算
        /*長時間生き残ると、スコアが高い*/
        score = (int)timer.GetTime() * 10 + playerKillCount.KillCounts * 100;
    }

    public void DrawScore()
    {
        //スコアの表示
        if (displayScore != score)
        {
            displayScore = (int)Mathf.Lerp(displayScore, score, 0.01f);
        }

        scoreText.text = string.Format("{0:00000000}", displayScore);
    }

    public void DrawMultiResult()
    {
        //1PのkillCount表示
        playerKillNum.text = playerKillCount.KillCounts.ToString();

        //2PのkillCount表示
        secondKillNum.text = scondKillCount.KillCounts.ToString();
    }
}