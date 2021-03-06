﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GamepadInput;

public class CharacterManager : MonoBehaviour
{
    static CharacterManager instance_;
    
    const int EntryPlayerMax = 2;
    Player []playerList_ = new Player[EntryPlayerMax];

    BaseEnemy enemy_;
    Vector2 entryPos = new Vector2(8, 0);
    const float ReEntryTime_ = 2.0f;
    float currentEntryTime_ = 0.0f;
    
    const string SingleScene = "arenaSingle";

    bool isEnemyFirstKill_ = true;

    public static CharacterManager Instance
    {
        get { return instance_; }
    }

    public bool IsEntryEnemy
    {
        get { return string.Equals(SceneManager.GetActiveScene().name, SingleScene); }
    }

    public Player OtherPlayer(Player argPlayer)
    {
        Player otherPlayerPower = null;
        foreach (var player in playerList_)
        {
            if (argPlayer != player)
            {
                otherPlayerPower = player;
                break;
            }
        }
        return otherPlayerPower;
    }


    public BaseEnemy Enemy
    {
        get { return enemy_; }
        set { enemy_ = value; }
    }
    public Player []PlayerList
    {
        get { return playerList_; }
    }

    void Awake()
    {
        instance_ = this;
    }

    void Start()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int lPlayerIndex = 0; lPlayerIndex < EntryPlayerMax; lPlayerIndex++)
        {
            if(players.Length == lPlayerIndex)
            {
                break;
            }
            playerList_[lPlayerIndex] = players[lPlayerIndex].GetComponent<Player>();
        }
        if (playerList_[0])
        {
            playerList_[0].SetPadNumber = GameManager.Instance.oneIndex;
        }
        
        if (IsEntryEnemy)
        {
            enemy_ = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BaseEnemy>();
            Debug.Assert(enemy_);
        }
        else
        {
            playerList_[1].SetPadNumber = GameManager.Instance.twoIndex;
        }

    }

    void Update ()
    {
        if (IsEntryEnemy)
        {
            if (!enemy_.gameObject.activeSelf)
            {
                if(isEnemyFirstKill_)
                {
                    WeaponManager.Instance.ResetWeaponPopTimeAndWeaponPop();
                    isEnemyFirstKill_ = false;
                }

                if (currentEntryTime_ <= 0.0f)
                {// 敵が死んだ瞬間のみスコア計算
                    ScoreManager.Instance.SingleScore();
                }

                currentEntryTime_ += Time.deltaTime;
                if (currentEntryTime_ > ReEntryTime_)
                {
                    currentEntryTime_ = 0;
                    EntryEnemy();
                }
            }
        }
    }

    void EntryEnemy()
    {
        Vector2 pos = (playerList_[0].gameObject.transform.position.x < 0) ? entryPos : -entryPos;
        enemy_.Initialize((int)playerList_[0].EquipmentWeapon.WeakWeaponType, 20, pos);
    }
}
