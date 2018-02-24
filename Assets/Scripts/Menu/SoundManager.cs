using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    public AudioSource source;

    public AudioClip[] backgroundClip;
    public AudioClip[] whoosh;
	public AudioClip[] shout;
	public AudioClip[] sword;
	public AudioClip winClip;
	public AudioClip gemClip;
	public AudioClip breakBox;
	public AudioClip gameOver;
	public AudioClip doubleKill, tripleKill, monsterKill, ultraKill, goodLike;
//------------------------------------------------------------
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
//------------------------------------------------------------
    public void PlayBG()
    {
        source.clip = backgroundClip[0];
        source.Play();
        source.loop = true;
    }
}
