using UnityEngine;

public class bobbing : MonoBehaviour {

    Animator animator_;
    float time = 0;

    void Start()
    {
        animator_ = GetComponent<Animator>();
        animator_.enabled = false;
        time = Random.Range(0.0f, 3.0f);
    }

    void Update () {

        time -= Time.deltaTime;

        if (!animator_.enabled && time <= 0.0f)
        {
            animator_.enabled = true;
        }
    }
}
