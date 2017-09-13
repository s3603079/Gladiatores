using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    TestPlayer activePlayer_;   //  !<  生きているプレイヤー
    float targetDir_ = 0;       //  !<  ターゲットの方向
    Transform targetPos_;       //  !<  ターゲットの位置

    void Start()
    {
        activePlayer_ = CharacterManager.Instance.PlayerList[0];
        targetPos_ = activePlayer_.gameObject.transform;
        targetDir_ = Mathf.Atan2(targetPos_.position.y - transform.position.y, targetPos_.position.x - transform.position.x);
    }
	
    public void Execute(BaseEnemy argBaseEnemy)
    {
        //  攻撃中は行動しない
        if (argBaseEnemy.IsAttacking)
            return;

        Move(argBaseEnemy);
        Attack(argBaseEnemy);
    }

    void Attack(BaseEnemy argBaseEnemy)
    {
        var targetDistance = Vector2.Distance(transform.position, targetPos_.position);
        if(targetDistance <= argBaseEnemy.EquipmentWeapon.AttackedReach)
        {
            //  TODO    :   animation

            argBaseEnemy.Attack();
        }
    }

#region Move
    void Move(BaseEnemy argBaseEnemy)
    {
        //  攻撃中は移動しない
        if (argBaseEnemy.IsAttacking)
            return;

        if (WeaponManager.Instance.ActiveWeapons[0] && argBaseEnemy.IsMoveToPick())
        {
            MoveToPick(argBaseEnemy);
        }
        else
        {
            MoveToAttack(argBaseEnemy);
        }

        Jump(argBaseEnemy);

    }

    void MoveToAttack(BaseEnemy argBaseEnemy)
    {
        //  移動方向の取得
        targetDir_ = Mathf.Atan2(
            targetPos_.position.y - transform.position.y,
            targetPos_.position.x - transform.position.x);

        //  移動座標の取得
        Vector2 target = transform.position;
        target.x += argBaseEnemy.Spd.x * Mathf.Cos(targetDir_);

        //  移動
        transform.position = target;

        //  画像の向きの変更
        transform.localScale = (targetPos_.position.x < transform.position.x) ?
            new Vector3(argBaseEnemy.Direction.x, transform.localScale.y, transform.localScale.z) :
            new Vector3(-argBaseEnemy.Direction.x, transform.localScale.y, transform.localScale.z);
    }

    void MoveToPick(BaseEnemy argBaseEnemy)
    {
        //  武器の座標取得
        Transform weaponPos = WeaponManager.Instance.ActiveWeapons[0].transform;

        //  移動方向の取得
        targetDir_ = Mathf.Atan2(
            weaponPos.position.y - transform.position.y,
            weaponPos.position.x - transform.position.x);

        //  移動座標の取得
        Vector2 target = transform.position;
        target.x += argBaseEnemy.Spd.x * Mathf.Cos(targetDir_);

        //  移動
        transform.position = target;

        //  画像の向きの変更
        transform.localScale = (weaponPos.position.x < transform.position.x) ?
            new Vector3(argBaseEnemy.Direction.x, transform.localScale.y, transform.localScale.z) :
            new Vector3(-argBaseEnemy.Direction.x, transform.localScale.y, transform.localScale.z);

    }

    void Jump(BaseEnemy argBaseEnemy)
    {
        if (!activePlayer_.IsJumping ||
            argBaseEnemy.IsJumping)
            return;

        argBaseEnemy.Jump();

#if false
        argBaseEnemy.IsJumping = true;
        argBaseEnemy.RigitBody2D.velocity = new Vector2(argBaseEnemy.RigitBody2D.velocity.x, argBaseEnemy.Spd.y);
#endif
    }
    #endregion Move
}
