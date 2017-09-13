using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    protected override void Start() {
        attackedReach_ = 1.0f;
        weakToType_ = WeaponType.Bow;
        strengthToType_ = WeaponType.Punch;
        thisType_ = WeaponType.Sword;
        base.Start();
    }

    public override void Attack(float InputValue) {
        transform.parent.localEulerAngles = (Vector3.MoveTowards(transform.forward * 0F, transform.forward * 90F, InputValue) * 120F);
    }
}
