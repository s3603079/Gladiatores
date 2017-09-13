using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon {

    [SerializeField]
    private GameObject arrowPrefab;
    [SerializeField]
    private float shotPower = 10F;

    private GameObject arrow;
    private float coolTime;
    private bool isDraw;
    private bool isShot;

    public override void Attack(float InputValue) {
        if(InputValue >= 0.1F)
        {
            if(!isDraw && coolTime++ >= 60F)
            {
                isDraw = true;
                isShot = true;

                arrow = Instantiate(arrowPrefab, transform);
                arrow.transform.localPosition = Vector3.left;
            }
        }
        else
        {
            if(isShot)
            {
                isDraw = false;
                isShot = false;
                coolTime = 0F;

                arrow.transform.parent = null;
                arrow.AddComponent<Rigidbody2D>().AddForce(-transform.parent.up * shotPower * 100F);
            }
        }
    }
}
