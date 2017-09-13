using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobbing : MonoBehaviour {

    Animator animator_;
    float time = 0;
    SpriteRenderer spriteRenderer_;
    Sprite sprite_;

    // Use this for initialization
    void Start()
    {
        animator_ = GetComponent<Animator>();
        animator_.enabled = false;
        time = Random.Range(0.0f, 3.0f);
        spriteRenderer_ = GetComponent<SpriteRenderer>();
        sprite_ = spriteRenderer_.sprite;
    }

    // Update is called once per frame
    void Update () {

        time -= Time.deltaTime;

        if (!animator_.enabled && time <= 0.0f)
        {
            animator_.enabled = true;
        }
#if false
        else if (animator_.enabled)
        {
            time = 100000;
            animator_.enabled = false;
            spriteRenderer_.sprite = sprite_;
            Debug.Assert(spriteRenderer_.sprite);
            Debug.Assert(sprite_);
        }
#endif
    }
}
