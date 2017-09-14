// ==============================
// file:SoundManager(.cs)
// brief:全ての音声機構を所有する
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager> {

    [Range(0F, 1F)]
    public float volume = 0.5F;

    [SerializeField]
    private AudioClip[] bgmClips;
    [SerializeField]
    private AudioClip[] seClips;

    private Dictionary<string, int> bgmIndex;
    private Dictionary<string, int> seIndex;

    private AudioSource bgmPlayer;
    private AudioSource[] sePlayer;

    /// <summary>
    /// Start前に呼び出される
    /// </summary>
    protected override void Awake() {
        base.Awake();

        // BGM用のオーディオソースを追加
        bgmPlayer = gameObject.AddComponent<AudioSource>();
        bgmPlayer.loop = true;
        bgmIndex = new Dictionary<string, int>();
        for(var i = 0; i < bgmClips.Length; i++)
        {
            bgmIndex.Add(bgmClips[i].name, i);
        }

        // SE用のオーディオソースを追加
        sePlayer = new AudioSource[seClips.Length];
        seIndex = new Dictionary<string, int>();
        for (var i = 0; i < sePlayer.Length; i++)
        {
            sePlayer[i] = gameObject.AddComponent<AudioSource>();
            sePlayer[i].clip = seClips[i];
            seIndex.Add(seClips[i].name, i);
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update() {
        // 全ての再生機構の音量を変更
        bgmPlayer.volume = volume;
        foreach(var se in sePlayer)
        {
            se.volume = volume;
        }
    }

    /// <summary>
    /// 背景音を名前から再生
    /// </summary>
    /// <param name="soundName"></param>
    public void PlayBGM(string soundName) {
        var index = bgmIndex[soundName];
        bgmPlayer.clip = bgmClips[index];
        bgmPlayer.Play();
    }

    /// <summary>
    /// 効果音を名前から再生
    /// </summary>
    /// <param name="soundName"></param>
    public void PlaySE(string soundName) {
        var index = seIndex[soundName];
        sePlayer[index].Play();
    }
}
