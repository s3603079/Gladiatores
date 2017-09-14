using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManager : MonoBehaviour
{
    static WeaponManager instance_;
    List<Weapon> activeWeapons_ = new List<Weapon>();   //  !<  フィールドに落ちている武器 
    const string PrefabsPath = "Prefabs/Weapons/";    //  !<  武器のプレハブのパス

    Weapon[] weaponTypeGruop_ = new Weapon[(int)WeaponType.Max];

    float weaponPopTime_ = Mathf.Infinity;
    float currentPopTime_ = 0.0f;

    public static WeaponManager Instance
    {
        get { return instance_; }
    }

    public void ResetWeaponPopTimeAndWeaponPop()
    {
        currentPopTime_ = 0;
        weaponPopTime_ = Random.Range(5, 10);
        PopupWeapon();
    }

    public void RemoveActiveWeapon(GameObject argDestroyWeapon)
    {
        for (var lDeleteIndex = 0; lDeleteIndex < activeWeapons_.Count; lDeleteIndex++)
        {
            if (activeWeapons_[lDeleteIndex].gameObject == argDestroyWeapon)
            {// 引数と同じオブジェクトをアクティブリストとゲームから削除
                activeWeapons_.RemoveAt(lDeleteIndex);
                Destroy(argDestroyWeapon);
                break;
            }
        }
    }

    public void AddActiveWeapon(Weapon argWeapon)
    {
        int weaponSameTypeCnt = 0;
        bool res = true;
        foreach (var activeWeapon in activeWeapons_)
        {
            if (activeWeapon.ThisWeaponType == argWeapon.ThisWeaponType)
            {// アクティブな武器のカウント
                weaponSameTypeCnt++;
            }

            if (weaponSameTypeCnt >= 2)
            {// 既に同じ種類の武器が2つあれば生成しない
                res = false;
                Destroy(argWeapon.gameObject);
                break;
            }
        }

        if (res)
        {
            activeWeapons_.Add(argWeapon);
        }
    }

    public Weapon SearchNearestWeapon(Vector2 argCharcterPos)
    {
        float nearest = 100000; //  !<  比較用の大きい値
        Weapon weapon = null;   //  !<  一番距離が近い武器
        foreach (var lWeapon in activeWeapons_)
        {
            float distance = Vector2.Distance(lWeapon.transform.position, argCharcterPos);
            if (nearest > distance)
            {// より近い距離の武器を選択
                nearest = distance;
                weapon = lWeapon;
            }
        }

        return weapon;
    }

    void Awake()
    {
        instance_ = this;
    }

    void Start()
    {
        GameObject[] weaponGruop = new GameObject[(int)WeaponType.Max];

        weaponGruop[(int)WeaponType.Sword] = Resources.Load(PrefabsPath + "Sword") as GameObject;
        weaponGruop[(int)WeaponType.Shield] = Resources.Load(PrefabsPath + "Shield") as GameObject;
        weaponGruop[(int)WeaponType.Bow] = Resources.Load(PrefabsPath + "Bow") as GameObject;

        for (int lType = 0; lType < (int)WeaponType.Max; lType++)
        {
            if (!weaponGruop[lType])
                continue;
            weaponTypeGruop_[lType] = weaponGruop[lType].GetComponent<Weapon>();
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "arenaMulti" ||
            SceneManager.GetActiveScene().name == "arenaSingle")
        {
            currentPopTime_ += Time.deltaTime;
            if (CharacterManager.Instance.IsEntryEnemy)
            {
                if (weaponPopTime_ < currentPopTime_)
                {
                    ResetWeaponPopTimeAndWeaponPop();
                }
            }
            else
            {
                const float WeaponPopTime = 10f;
                if (WeaponPopTime < currentPopTime_)
                {
                    ResetWeaponPopTimeAndWeaponPop();
                }
            }
        }
#if false
        if (Input.GetKeyDown(KeyCode.W))
        {
            DebugPopupWeapon(WeaponType.Sword);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            DebugPopupWeapon(WeaponType.Shield);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            DebugPopupWeapon(WeaponType.Bow);
        }
#endif
    }

    void PopupWeapon()
    {
        int weaponType = Random.Range((int)WeaponType.Sword, (int)WeaponType.Max);
        GameObject weapon = weaponTypeGruop_[weaponType].gameObject;

        float range = (Camera.main.orthographicSize - 1.5f) * 2.0f;
        Vector3 pos = new Vector3(Random.Range(-range, range), range, 0);
        Instantiate(weapon, pos, Quaternion.identity);
    }


    void DebugPopupWeapon(WeaponType argWeaponType)
    {
        GameObject weapon = weaponTypeGruop_[(int)argWeaponType].gameObject;

        float range = (Camera.main.orthographicSize - 1.5f) * 2.0f;
        Vector3 pos = new Vector3(Random.Range(-range, range), range, 0);
        Instantiate(weapon, pos, Quaternion.identity);
    }
}
