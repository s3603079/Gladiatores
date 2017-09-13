using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationLion : MonoBehaviour {

    [SerializeField]
    private Image lion;

    [SerializeField]
    private bool Is1P;

    private Texture2D Closed;
    private Texture2D Open;

    private float timer;

    Sprite spC;
    Sprite spO;

    void Start()
    {
        timer = 0;
        if (Is1P)
        {
            lion = GameObject.Find("Canvas/KillCount(Player)/Handle Slide Area/Handle").GetComponent<Image>();
        }
        else
        {
            lion = GameObject.Find("Canvas/KillCount(Enemy)/Handle Slide Area/Handle").GetComponent<Image>();
        }
        Open = Resources.Load("Textures/UI/LionHead_Lineart") as Texture2D;
        Closed= Resources.Load("Textures/UI/LionHead_Lineart_Closed") as Texture2D;

        spC = Sprite.Create(Closed, new Rect(0, 0, Closed.width, Closed.height), Vector2.zero);
        spO = Sprite.Create(Open, new Rect(0, 0, Open.width, Open.height), Vector2.zero);
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if((int)timer%2==0)
        {
            lion.sprite = spO;
        }
        if((int)timer%2f==1)
        {
            lion.sprite = spC;
        }
    }
}
