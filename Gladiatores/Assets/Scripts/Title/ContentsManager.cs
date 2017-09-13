using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class ContentsManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] _contents;

    [SerializeField]
    private GameObject _chooseIcon;

    private static GameObject selection;

	// Use this for initialization
	void Start () {
        selection = _contents[0];
        _chooseIcon.transform.localPosition = new Vector2(_chooseIcon.transform.localPosition.x, -20);

    }
	
	// Update is called once per frame
	void Update () {

        var input = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One);

        if (Vector2.Distance(input, Vector2.up) == 0) { //up
            selection = _contents[0];
            _chooseIcon.transform.localPosition = new Vector2(_chooseIcon.transform.localPosition.x, -20);
        }

        else if (Vector2.Distance(input, Vector2.down) == 0){ //down
            selection = _contents[1];
            _chooseIcon.transform.localPosition = new Vector2(_chooseIcon.transform.localPosition.x, -70);
        }
    }

    public static GameObject getSelection() {
        return selection;
    }
}
