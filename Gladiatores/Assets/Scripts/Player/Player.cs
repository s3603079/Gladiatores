﻿using UnityEngine;
using GamepadInput;

public class Player : Character
{
    bool canPick_ = false;                    //  !<  アイテムを拾えるかどうかのフラグ
    PlayerInputPad pad_;

    public GamePad.Index SetPadNumber
    {
        set { pad_.PadNumber = value; }
    }

    private void Awake()
    {
        pad_ = GetComponent<PlayerInputPad>();
    }

    void Start()
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
        
        Actions();
        base.Update();
    }

    protected override void ChoiceWeapon(WeaponType argWeaponType = WeaponType.Max, GameObject argGameObject = null)
    {
        if (!pad_.GetLeftTriggerPushed())
            return;

        if (equipmentWeapon_.ThisWeaponType != WeaponType.Punch && !argGameObject)
        {
            //  武器のRelease
            ChangeWeapon((int)WeaponType.Punch);
        }
        else if (argGameObject)
        {
            //  武器のPick
            base.ChoiceWeapon(argWeaponType, argGameObject);
        }
    }

    void Actions()
    {
        Walk(pad_.GetAxis(GamePad.Axis.LeftStick).x);
        Jump(pad_.GetButtonDown(GamePad.Button.A));
        RotaShoulder(pad_.GetAxis(GamePad.Axis.RightStick));
        Attack(pad_.GetTrigger(GamePad.Trigger.RightTrigger));
        if (!canPick_)
            ChoiceWeapon();
        canPick_ = false;

    }

    void Walk(float argInputValue)
    {
        if (isAttacking_)
            return;

        rigid2d_.velocity = new Vector2(spd_.x * argInputValue, rigid2d_.velocity.y);
    }

    void Jump(bool argInputTrigger)
    {
        if (argInputTrigger && !isJumping_)
        {
            base.Jump();
        }
    }

    void Attack(float argInputValue)
    {
        if (argInputValue <= 0.0f)
            return;

        base.Attack();
        equipmentWeapon_.Attack(argInputValue);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Weapon weapon = collision.gameObject.GetComponent<Weapon>();
        if (weapon)
            canPick_ = true;

        TriggerStay2D(weapon, collision, (int)CharacterManager.Instance.Enemy.Power);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping_ = false;
        }
    }
}
