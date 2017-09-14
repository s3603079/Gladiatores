using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackgroundMusic : MonoBehaviour {

    [SerializeField]
    private string bgmName;

	void Start () {
        SoundManager.Instance.PlayBGM(bgmName);
	}
}
