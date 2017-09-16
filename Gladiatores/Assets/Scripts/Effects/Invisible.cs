using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invisible : MonoBehaviour
{
    [SerializeField]
    private Image image;

    private static float alpha;

	void Start () {
        alpha = 0f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }

    public static bool SpriteOn()
    {
        bool res = false;
        alpha += 0.3f * Time.deltaTime;
        if (alpha > 1.0f)
        {
            res = true;
        }
        return res;
    }
}
