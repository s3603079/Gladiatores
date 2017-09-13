using UnityEngine;

public class BaseEnemy : Character
{
    int level_ = 0;             //  !<  現在のレベル
    EnemyAI ai_;                //  !<  AIの挙動

    float animationTime_ = 0.0f;

#region UnityDefault
    void Start()
    {
        base.Start();
        power_ = 1f;
        spd_ = new Vector2(spd_ .x * 0.01f, spd_.y);    //  !<  速度の補正
        ai_ = GetComponent<EnemyAI>();
        logRegistKey_[(int)LogNum.Attack] = "Enemy Attaking : ";
        logRegistKey_[(int)LogNum.TakeDamage] = "Player Attack for Enemy!! ";
    }

    void Update()
    {
        if(!isLiving_)
        {
            gameObject.SetActive(false);
        }

        ai_.Execute(this);
        base.Update();
    }

    public void Animation()
    {
        animationTime_ += Time.deltaTime * 6.2f;
        equipmentWeapon_.Attack(Mathf.Sin(animationTime_));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping_ = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Weapon weapon = collision.gameObject.GetComponent<Weapon>();
        TriggerStay2D(weapon, collision, (int)CharacterManager.Instance.PlayerList[0].Power);
    }
#endregion UnityDefault

#region Move
    public bool IsMoveToPick()
    {
        bool res = true;
        if (equipmentWeapon_.ThisWeaponType != WeaponType.Punch)
            res = false;
        Player player = CharacterManager.Instance.PlayerList[0];
        WeaponType playerWeaponType = player.EquipmentWeapon.ThisWeaponType;

        //  TODO    :   未実装
        //if(playerWeaponType == equipmentWeapon_.WeakWeaponType)

        //  プレイヤーの弱点以外か、自分と同じ武器タイプなら拾いに行かない
        Weapon weapon = WeaponManager.Instance.SearchNearestWeapon(transform.position);
        if (weapon)
        {
            if (weapon.StrengthWeaponType != playerWeaponType || 
                weapon.ThisWeaponType == equipmentWeapon_.ThisWeaponType)
            {
                res = false;
            }
        }
        return res;
    }
#endregion Move
#region Attack

    protected override void ChoiceWeapon(WeaponType argWeaponType = WeaponType.Max, GameObject argGameObject = null)
    {
        Player player = CharacterManager.Instance.PlayerList[0];
        WeaponType playerWeaponType = player.EquipmentWeapon.ThisWeaponType;

        //  TODO    :   まだ未実装
        //if(playerWeaponType == equipmentWeapon_.WeakWeaponType)

        Weapon weapon = WeaponManager.Instance.SearchNearestWeapon(transform.position);
        if (weapon)
        {
            if (weapon.StrengthWeaponType == playerWeaponType &&
                weapon.ThisWeaponType != equipmentWeapon_.ThisWeaponType)
            {
                base.ChoiceWeapon(argWeaponType, argGameObject);
            }
        }
    }
#endregion Attack
}