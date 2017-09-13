using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectIcon : MonoBehaviour {

    [SerializeField]
    private VirtualChatactor chara;//所持している武器を取得するため

    [SerializeField]
    private GameObject[] actions;

    [SerializeField]
    private GameObject[] squares;

    private GameObject parent;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
		switch(chara.WeaponType())
        {
            case 0://パンチを選択時
                actions[0].transform.position = squares[0].transform.position;
                actions[1].transform.position = squares[1].transform.position;
                actions[2].transform.position = squares[2].transform.position;
                actions[3].transform.position = squares[3].transform.position;
                break;

            case 1:
                actions[1].transform.position = squares[0].transform.position;
                actions[0].transform.position = squares[1].transform.position;
                actions[2].transform.position = squares[2].transform.position;
                actions[3].transform.position = squares[3].transform.position;
                break;

            case 2:
                actions[2].transform.position = squares[0].transform.position;
                actions[0].transform.position = squares[1].transform.position;
                actions[1].transform.position = squares[2].transform.position;
                actions[3].transform.position = squares[3].transform.position;
                break;

            case 3:
                actions[3].transform.position = squares[0].transform.position;
                actions[0].transform.position = squares[1].transform.position;
                actions[1].transform.position = squares[2].transform.position;
                actions[2].transform.position = squares[3].transform.position;
                break;
        }
	}
}
