using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    void Start()
    {
        var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (string.Equals(sceneName, "arenaSingle") || string.Equals(sceneName, "arenaMulti"))
        {
            playerKillCount = CharacterManager.Instance.PlayerList[0].gameObject.GetComponent<KillCount>();
            displayScore = score;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene argScene, LoadSceneMode argSceneMode)
    {
        if (string.Equals(argScene.name, "Result"))
        {
            if (!scoreText)
            {
                scoreText = GameObject.FindGameObjectWithTag("ScoreNumber").GetComponent<Text>();
            }
        }
        else if (string.Equals(argScene.name, "ResultForMulti"))
        {
            if (!scoreText)
            {
                GameObject[] texts = GameObject.FindGameObjectsWithTag("ScoreNumber");

                playerKillNum = texts[0].GetComponent<Text>();
                secondKillNum = texts[0].GetComponent<Text>();
            }
        }
    }

    private void Update()
    {
        if (string.Equals(SceneManager.GetActiveScene().name, "Result"))
        {
            DrawScore();
        }
        else if (string.Equals(SceneManager.GetActiveScene().name, "ResultForMulti"))
        {
            DrawMultiResult();
        }
    }

    public void SingleScore()
    {
        //キルカウントの加算
        playerKillCount.KillCounts++;

        //スコアの加算
        /*長時間生き残ると、スコアが高い*/
        score = (int)timer.GetTime() * 10 + playerKillCount.KillCounts * 100;
    }

    public void AddOtherPlayerScore(Player argPlayer)
    {
        if(argPlayer == CharacterManager.Instance.PlayerList[0])
        {
            playerKillCount.KillCounts++;
            //スコアの加算
            /*長時間生き残ると、スコアが高い*/
            score = (int)timer.GetTime() * 10 + playerKillCount.KillCounts * 100;
        }
        else
        {
            scondKillCount.KillCounts++;
            //スコアの加算
            /*長時間生き残ると、スコアが高い*/
            score = (int)timer.GetTime() * 10 + scondKillCount.KillCounts * 100;
        }
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
        if(playerKillNum.text == "")
        {
            playerKillNum.text = "0";
        }
        //2PのkillCount表示
        secondKillNum.text = scondKillCount.KillCounts.ToString();
        if (secondKillNum.text == "")
        {
            secondKillNum.text = "0";
        }
    }
}