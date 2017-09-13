using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : SingletonMonoBehaviour<CharacterManager>
{
    List<TestPlayer> playerList_ = new List<TestPlayer>();

    BaseEnemy enemy_;
    Vector2 entryPos = new Vector2(10, 0);
    const float ReEntryTime_ = 1.0f;
    float currentEntryTime_ = 0.0f;

    public BaseEnemy Enemy
    {
        get { return enemy_; }
        set { enemy_ = value; }
    }
    public List<TestPlayer> PlayerList
    {
        get { return playerList_; }
    }

    override protected void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        //if(SceneManager.GetActiveScene().name == "Play")
        //{//   プレイシーンに入ったらオブジェクト格納
        //}

        //  HACK    :   デバックシーンの為、仮の処理
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (var player in players)
        {
            playerList_.Add(player.GetComponent<TestPlayer>());
        }
        enemy_ = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy>();
    }
	
	void Update ()
    {
        if(!enemy_.gameObject.activeSelf)
        {
            if(currentEntryTime_ <= 0.0f)
            {// 敵が死んだ瞬間のみスコア計算
                TestScoreManager.Instance.AddScore();
            }

            currentEntryTime_ += Time.deltaTime;
            if(currentEntryTime_ > ReEntryTime_)
            {
                currentEntryTime_ = 0;
                EntryEnemy();
            }
        }
    }

    void EntryEnemy()
    {
        Vector2 pos = (playerList_[0].gameObject.transform.position.x < 0) ? entryPos : -entryPos;
        //  HACK    :   プレイヤーの弱点タイプ
        //playerList_[0].EquipmentWeapon.WeakWeaponType
        enemy_.Initialize((int)WeaponType.Sword, 1, pos);
    }
}
