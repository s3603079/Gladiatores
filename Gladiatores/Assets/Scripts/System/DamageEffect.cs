using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{

    static Animator _animator;
    static Vector3 pos;
    

    float timer;

    void Start()
    {
        _animator = GetComponent<Animator>();
        timer = 0f;
    }

    private void Update()
    {
        transform.position = pos;
    }

    public static void Anim(Character character)
    {
        //プレイヤーの上に表示するか画面外に飛ばす
        pos = Camera.main.WorldToScreenPoint(character.gameObject.transform.position + new Vector3(0f, 0f, 0f));
        if (_animator)
        {
            Debug.Log(_animator);
            _animator.SetTrigger("Damage");
            _animator.Play("damageBef");
        }
    }
}
