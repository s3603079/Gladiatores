using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : Character
{
    bool canPick_ = false;                    //  !<  アイテムを拾えるかどうかのフラグ

    void Start ()
    {
        power_ = 10;
        base.Start();
        logRegistKey_[(int)LogNum.Attack] = "Player Attaking : ";
        logRegistKey_[(int)LogNum.TakeDamage] = "Enemy Attack for Player!! ";
    }

    void Update()
    {
        if (!isLiving_)
        {// TODO    :   死亡処理
        }
        DebugActions();
        base.Update();
    }

    protected override void ChoiceWeapon(WeaponType argWeaponType = WeaponType.Max, GameObject argGameObject = null)
    {
        if (!Input.GetKeyDown(KeyCode.E))
            return;

        if (equipmentWeapon_.ThisWeaponType != WeaponType.Punch && !argGameObject)
        {
            //  TODO    :   武器のRelease
            ChangeWeapon((int)WeaponType.Punch);
        }
        else if (argGameObject)
        {
            //  TODO    :   武器のPick
            base.ChoiceWeapon(argWeaponType, argGameObject);
        }
    }


    void DebugActions()
    {
        if (isAttacking_)
            return;

        DebugMove();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }

        if (!canPick_)
            ChoiceWeapon();
        canPick_ = false;
    }

    void DebugNoticeLineRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            equipmentWeapon_.transform.localEulerAngles = new Vector3(equipmentWeapon_.transform.localEulerAngles.x, equipmentWeapon_.transform.localEulerAngles.y, equipmentWeapon_.transform.localEulerAngles.z - 1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            equipmentWeapon_.transform.localEulerAngles = new Vector3(equipmentWeapon_.transform.localEulerAngles.x, equipmentWeapon_.transform.localEulerAngles.y, equipmentWeapon_.transform.localEulerAngles.z + 1);
        }
    }

    void DebugMove()
    {
        if (isAttacking_)
            return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigid2d_.velocity = new Vector2(spd_.x, rigid2d_.velocity.y);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigid2d_.velocity = new Vector2(-spd_.x, rigid2d_.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping_)
        {
            base.Jump();

#if false
            isJumping_ = true;
            rigid2d_.velocity = new Vector2(rigid2d_.velocity.x, spd_.y);
#endif
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Weapon weapon = collision.gameObject.GetComponent<Weapon>();
        if(weapon)
            canPick_ = true;

        TriggerStay2D(weapon, collision, (int)CharacterManager.Instance.Enemy.Power);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isJumping_ = false;
        }
    }
}
