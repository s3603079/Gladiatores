using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapon {

    [SerializeField]
    private float spring = 1F;

    protected override void Start() {
        attackedReach_ = 1.0f;
        weakToType_ = WeaponType.Punch;
        strengthToType_ = WeaponType.Bow;
        thisType_ = WeaponType.Shield;
        base.Start();
    }

    public override void Attack(float InputValue) {
        var foward = transform.parent.parent.parent.parent.up;
        transform.parent.localPosition = (Vector3.MoveTowards(Vector3.zero, foward, InputValue) * spring);
    }
}
