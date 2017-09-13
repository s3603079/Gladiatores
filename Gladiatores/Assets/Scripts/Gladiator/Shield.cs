using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapon {

    [SerializeField]
    private float spring = 1F;

    private Vector3 offset;

    protected override void Start() {
        attackedReach_ = 1.0f;
        weakToType_ = WeaponType.Punch;
        strengthToType_ = WeaponType.Bow;
        thisType_ = WeaponType.Sword;
        base.Start();

        offset = transform.parent.localPosition;
    }

    public override void Attack(float InputValue) {
        var foward = transform.parent.parent.parent.parent.up;
        transform.parent.localPosition = offset + (Vector3.MoveTowards(Vector3.zero, foward, InputValue) * spring);
    }
}
