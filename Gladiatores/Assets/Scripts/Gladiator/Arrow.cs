using UnityEngine;

public class Arrow : Weapon
{
    [SerializeField]
    float destroyTime_ = 1.0f;

    void Update()
    {
        if (!transform.parent)
        {// 発射されたら一定時間後に消滅
            Destroy(gameObject, destroyTime_);
        }
    }
}
