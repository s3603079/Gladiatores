using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectIcon : MonoBehaviour {

    [SerializeField]
    private VirtualChatactor chara;//所持している武器を取得するため

    [SerializeField]
    private Image[] squares;

    [SerializeField]
    private string[] iconName;

    Texture2D textureNS;
    Texture2D textureS;

    //スプライト設定
    Sprite spNS;//ノットセレクト
    Sprite spS;//セレクト

    // Use this for initialization
    void Start () {
        textureNS= Resources.Load("Textures/UI/SkillButton_Unselected") as Texture2D;
        textureS = Resources.Load("Textures/UI/SkillButton_Selected") as Texture2D;

        for (var i = 0; i < squares.Length; i++)
        {
            squares[i]= GameObject.Find("Canvas/SelectIcon/"+iconName[i]).GetComponent<Image>();
        }
        //変更するスプライトの作成
        spNS = Sprite.Create(textureNS, new Rect(0, 0, textureNS.width, textureNS.height), Vector2.zero);
        spS = Sprite.Create(textureS, new Rect(0, 0, textureS.width, textureS.height), Vector2.zero);
    }

    // Update is called once per frame
    void Update () {
        //デバッグ用※要書き換え
        switch (chara.WeaponType())
        {
            case 0://パンチを選択時
                squares[0].sprite = spS;
                squares[1].sprite = spNS;
                squares[2].sprite = spNS;
                squares[3].sprite = spNS;
                break;

            case 1:
                squares[0].sprite = spNS;
                squares[1].sprite = spS;
                squares[2].sprite = spNS;
                squares[3].sprite = spNS;
                break;

            case 2:
                squares[0].sprite = spNS;
                squares[1].sprite = spNS;
                squares[2].sprite = spS;
                squares[3].sprite = spNS;
                break;

            case 3:
                squares[0].sprite = spNS;
                squares[1].sprite = spNS;
                squares[2].sprite = spNS;
                squares[3].sprite = spS;
                break;
        }
	}
}
