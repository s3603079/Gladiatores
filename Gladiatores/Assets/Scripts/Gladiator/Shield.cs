using UnityEngine;

public class Shield : Weapon {

    [SerializeField]
    private float spring = 1f;

    protected override void Start()
    {
        attackedReach_ = 1.0f;
        weakToType_ = WeaponType.Punch;
        strengthToType_ = WeaponType.Bow;
        thisType_ = WeaponType.Shield;
        base.Start();
    }

    public override void Attack(float InputValue)
    {
        var foward = transform.parent.parent.parent.parent.up;
        transform.parent.localPosition = (Vector3.MoveTowards(Vector3.zero, foward, InputValue) * spring);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            Rigidbody2D rigit2d = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rigit2d)
            {
                rigit2d.AddForce(-rigit2d.velocity * 100f);
            }
        }
    }
}