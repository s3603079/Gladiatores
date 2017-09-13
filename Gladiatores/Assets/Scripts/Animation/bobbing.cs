using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobbing : MonoBehaviour {

    float time = 0;
    public Animator myAnimator;
   
	// Use this for initialization
	void Start () {
        myAnimator.enabled = false;
        time = Random.Range(0.0f, 3.0f);

    }
	
	// Update is called once per frame
	void Update () {

        time -= Time.deltaTime;

        if (time <= 0.0f)
        {
            myAnimator.enabled = true;
        }
    }
}
