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
        spd_ = new Vector2(spd_.x * 0.01f, spd_.y);    //  !<  速度の補正
        ai_ = GetComponent<EnemyAI>();
        logRegistKey_[(int)LogNum.Attack] = "Enemy Attaking : ";
        logRegistKey_[(int)LogNum.TakeDamage] = "Player Attack for Enemy!! ";
    }

    void Update()
    {
        if (!isLiving_)
        {
            gameObject.SetActive(false);
        }

        ai_.Execute(this);
        base.Update();
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

    public void Animation()
    {
        animationTime_ += Time.deltaTime * 1.0f;
        float arg = Mathf.Clamp(Mathf.Sin(animationTime_ * Mathf.PI * 2), 0.0f, 1.0f);
        equipmentWeapon_.Attack(arg);
        RotaAnim();

#if false
        switch (equipmentWeapon_.ThisWeaponType)
        {
            case WeaponType.Punch:
                animationTime_ += Time.deltaTime * 1.0f;
                float arg = Mathf.Clamp(Mathf.Sin(animationTime_ * Mathf.PI * 2), 0.0f, 1.0f);
                equipmentWeapon_.Attack(arg);
                RotaAnim();
                break;
            case WeaponType.Sword:
                animationTime_ += Time.deltaTime * 6.2f;
                equipmentWeapon_.Attack(Mathf.Sin(animationTime_));
                break;
            case WeaponType.Shield:
                AnimationShoulder();
                break;
            case WeaponType.Bow:
                AnimationShoulder();
                break;

            default:
                break;

        }
#endif
    }

    void RotaAnim()
    {
        // 方向角度
        Vector3 dir = CharacterManager.Instance.PlayerList[0].gameObject.transform.position - transform.position;
        // Y軸角度
        float yAngle = Mathf.Atan2(dir.x, dir.z);
        //180度以上差があれば+-360度して逆回し
        if (yAngle - transform.localEulerAngles.y > 180.0f)
        {
            yAngle -= 360.0f;
        }
        else if (transform.localEulerAngles.y - yAngle > 180.0f)
        {
            yAngle += 360.0f;
        }
        // X軸角度
        float zxLen = Mathf.Sqrt(dir.x * dir.x + dir.z * dir.z);
        float xAngle = Mathf.Atan2(dir.y, zxLen);

        //  軸と座標は違うので注意
        RotaShoulder(new Vector2(yAngle, xAngle));

    }


    float Aim(Vector2 p1, Vector2 p2)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }

    void AnimationShoulder()
    {
#if false
        var vec = (CharacterManager.Instance.transform.position - shoulder_.transform.position).normalized;
        var angle = (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) - 170.0f;
        shoulder_.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
#endif
        shoulder_.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Aim(transform.position, CharacterManager.Instance.PlayerList[0].gameObject.transform.position) * 0.5f);

    }

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