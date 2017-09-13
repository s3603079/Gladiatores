using UnityEngine;
using UnityEngine.UI;

public class TestScoreManager : SingletonMonoBehaviour<TestScoreManager>
{
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
    [SerializeField]
    Text scoreText_;        //  !<  スコアのテキスト

    TestKillCount playerKillCount_;
    Timer timer_ = new Timer();
    
    int score_ = 0;         //  !<  スコア(８桁)
    int displayScore_ = 0;  //  !<  動きをつけた表示用

    public int Score
    {
        get { return score_; }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        playerKillCount_ = CharacterManager.Instance.PlayerList[0].gameObject.GetComponent<TestKillCount>();
    }
    void Update()
    {
#if false   //  Debug
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("life" + CharacterManager.Instance.Enemy.Life.ToString());
            int test = CharacterManager.Instance.Enemy.Life - (int)CharacterManager.Instance.PlayerList[0].Power;
            Debug.Log("test" + test.ToString());
            CharacterManager.Instance.Enemy.Life = test;
            if(CharacterManager.Instance.Enemy.Life <= 0)
            {
                CharacterManager.Instance.Enemy.IsLiving = false;
            }
        }
#endif      //  Debug
    }

    public void AddScore()
    {
        /*長時間生き残ると、スコアが高い*/
        playerKillCount_.KillCount++;
        score_ = (int)timer_.GetTime() * 10 + playerKillCount_.KillCount * 100;
    }

    public void DrawScore()
    {
        if (displayScore_ != score_)
        {// スコアの表示
            displayScore_ = (int)Mathf.Lerp(displayScore_, score_, 0.1f);
        }

        scoreText_.text = string.Format("{0:00000000}", displayScore_);
    }
}