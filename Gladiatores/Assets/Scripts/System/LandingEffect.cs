using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingEffect : MonoBehaviour {

    Animator _animator;

    float timer;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        timer = 0f;
    }

    private void Update()
    {
        //エフェクトをキャラクターの上に表示する関数
        //this.transform.position = Camera.main.WorldToScreenPoint(character.gameObject.transform.position + new Vector3(0f, 1.6f, 0f));

        timer += Time.deltaTime;
        if ((int)timer % 3 == 0)
        {
            if (_animator)
            {
                _animator.SetTrigger("Landing");
                _animator.Play("nullAni");
            }
        }
    }
}
