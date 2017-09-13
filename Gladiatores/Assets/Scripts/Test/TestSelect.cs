using UnityEngine;

public class TestSelect : MonoBehaviour
{
    [SerializeField]
    Character chara_;

    GameObject[] squares_;
    GameObject[] actions_;

    void Start()
    {
        GameObject actions = transform.GetChild(0).gameObject;
        actions_ = new GameObject[actions.transform.childCount];

        for (int lAction = 0; lAction < actions.transform.childCount; lAction++)
        {
            actions_[lAction] = actions.transform.GetChild(lAction).gameObject;
        }

        squares_ = new GameObject[actions.transform.childCount];
        GameObject[] playerIcons = GameObject.FindGameObjectsWithTag("IconP");
        GameObject[] enemyIcons = GameObject.FindGameObjectsWithTag("IconE");
        
        for (int lSquare = 0; lSquare < actions.transform.childCount; lSquare++)
        {
            squares_[lSquare] = (chara_.GetComponent<BaseEnemy>()) ? enemyIcons[lSquare] : playerIcons[lSquare];
        }
    }

    void Update ()
    {
        switch (chara_.EquipmentWeapon.ThisWeaponType)
        {
            case WeaponType.Punch:
                actions_[0].transform.position = squares_[0].transform.position;
                actions_[1].transform.position = squares_[1].transform.position;
                actions_[2].transform.position = squares_[2].transform.position;
                actions_[3].transform.position = squares_[3].transform.position;
                break;

            case WeaponType.Sword:
                actions_[1].transform.position = squares_[0].transform.position;
                actions_[0].transform.position = squares_[1].transform.position;
                actions_[2].transform.position = squares_[2].transform.position;
                actions_[3].transform.position = squares_[3].transform.position;
                break;

            case WeaponType.Shield:
                actions_[2].transform.position = squares_[0].transform.position;
                actions_[0].transform.position = squares_[1].transform.position;
                actions_[1].transform.position = squares_[2].transform.position;
                actions_[3].transform.position = squares_[3].transform.position;
                break;

            case WeaponType.Bow:
                actions_[3].transform.position = squares_[0].transform.position;
                actions_[0].transform.position = squares_[1].transform.position;
                actions_[1].transform.position = squares_[2].transform.position;
                actions_[2].transform.position = squares_[3].transform.position;
                break;
        }
    }
}
