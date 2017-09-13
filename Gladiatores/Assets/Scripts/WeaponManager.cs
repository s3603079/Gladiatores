using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : SingletonMonoBehaviour<WeaponManager>
{
    Weapon []activeWeapons_ = new Weapon[1];   //  !<  フィールドに落ちている武器 
    const string PrefabsPath = "Prefabs/Weapons/";    //  !<  武器のプレハブのパス

    Weapon[] weaponTypeGruop_ = new Weapon[(int)WeaponType.Max];

    public Weapon []ActiveWeapons
    {
        get { return activeWeapons_; }
        set { activeWeapons_ = value; }
    }

    public void RemoveActiveWeapon(GameObject argDestroyWeapon, int argIndex)
    {
        Destroy(argDestroyWeapon);
        activeWeapons_[argIndex] = null;
    }

    override protected void Awake()
    {
        base.Awake();
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
    }

    public void DebugPopupWeapon(WeaponType argWeaponType)
    {
        GameObject weapon = weaponTypeGruop_[(int)argWeaponType].gameObject;

        float range = (Camera.main.orthographicSize - 1) * 2;
        Vector3 pos = new Vector3(Random.Range(-range, range), range, 0);
        Instantiate(weapon, pos, Quaternion.identity);
    }
}
