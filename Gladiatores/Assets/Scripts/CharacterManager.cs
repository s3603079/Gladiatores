using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GamepadInput;

public class CharacterManager : SingletonMonoBehaviour<CharacterManager>
{
    const int EntryPlayerMax = 2;
    Player []playerList_ = new Player[EntryPlayerMax];

    BaseEnemy enemy_;
    Vector2 entryPos = new Vector2(10, 0);
    const float ReEntryTime_ = 1.0f;
    float currentEntryTime_ = 0.0f;

    public BaseEnemy Enemy
    {
        get { return enemy_; }
        set { enemy_ = value; }
    }
    public Player []PlayerList
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

        for (int lPlayerIndex = 0; lPlayerIndex < EntryPlayerMax; lPlayerIndex++)
        {
            if(players.Length == lPlayerIndex)
            {
                break;
            }
            playerList_[lPlayerIndex] = players[lPlayerIndex].GetComponent<Player>();
        }
        enemy_ = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy>();
    }
	
	void Update ()
    {
        
        playerList_[0].Actions(GamePad.Index.One);

        if (playerList_[1])
        {
            playerList_[1].Actions(GamePad.Index.Two);
        }

        if (!enemy_.gameObject.activeSelf)
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
