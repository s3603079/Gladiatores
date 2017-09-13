using UnityEngine;

public class Punch : Weapon {

    [SerializeField]
    private float spring = 10F;

    private Vector3 offset;

    protected override void Start() {
        weakToType_ = WeaponType.Sword;
        strengthToType_ = WeaponType.Shield;
        thisType_ = WeaponType.Punch;
        base.Start();

        offset = transform.parent.localPosition;
    }

    public override void Attack(float InputValue) {
        var foward = transform.parent.parent.parent.parent.up;
        transform.parent.localPosition = offset + (Vector3.MoveTowards(Vector3.zero, foward, InputValue) * spring);
    }
}
