using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	[Header("Camera limit pos")]
	public CamPosData[] camPos;

	[Header ("Object")]
	[Space (10)]
	public List<GameObject> wallBox;
	public List<GameObject> enemy;
	public List<GameObject> gem;
	public Sprite unlockedBox;

	[Header("Swipe time")]
	[Space (10)]
	[HideInInspector]
	public int levelSwipeTime;
	public int[] swipeTime;

	[Header("UI")]
	[Space (10)]
	public Text swpieTime;
	public GameObject helpPanel;
	public GameObject pausePanel;
	public GameObject endPanel;
	public GameObject winPanel;
	public GameObject losePanel;
	public Text lastMove;
	public Text score;
	public Image soundImg;
	public Sprite soundOn, soundOff;
	public Image joyCircle, joyBall;
	public bool isFade;

	[Header("Game condition")]
	[Space (10)]
	public int totalEnemy;
	public int killedEnemy;
	public int totalGem;
	public int collectedGem;


	[Header("Partical effect")]
	public ParticleSystem wood;
	public ParticleSystem brick;
	public ParticleSystem enemy1;
	public ParticleSystem enemy2;
	public ParticleSystem blood1;
	public ParticleSystem blood2;
	public ParticleSystem winParticle;


	public GameObject circle;
	public GameObject ball;
	public GameObject joystick;
	public GameObject joystickBall;

	public bool isPressBtn;
	public GameObject handTut;
	public bool winGame;

	public Sprite bg1,bg2;
	public Image bgImg;
	void Awake ()
	{
		instance = this;

		levelSwipeTime = swipeTime [Menu.selectedLevel];
		SetSwipeTime (levelSwipeTime);

		SoundSys ();

		isFade = true;

		if (Menu.selectedLevel <= 9) {
			bgImg.sprite = bg1;
		} else {
			bgImg.sprite = bg2;
		}
	}

//------------------------------------------------------------
	public void SetSwipeTime(int value)
	{
		swpieTime.text = value.ToString ();
	}
//------------------------------------------------------------
	public void Help(int index)
	{
		switch (index) {
		case 0:
			Time.timeScale = 0;
			helpPanel.SetActive (true);
			break;
		case 1:
			Time.timeScale = 1;
			helpPanel.SetActive (false);
			break;
		}
	}
//------------------------------------------------------------
	public void Pause(int index)
	{
		switch (index) {
		case 0:
			Time.timeScale = 0;
			pausePanel.SetActive (true);
			AdsControl.Instance.ShowRewardVideo ();
			break;
		case 1:
			Time.timeScale = 1;
			Application.LoadLevel ("Menu");
			break;
		case 2:
			SetSound ();
			break;
		case 3:
			Time.timeScale = 1;
			Application.LoadLevel (Application.loadedLevel);
			break;
		case 4:
			Time.timeScale = 1;
			pausePanel.SetActive (false);
			break;
		}
	}

//------------------------------------------------------------
	public void PressButton()
	{
		isPressBtn = true;
	}
	public void ReleaseButton()
	{
		Invoke ("SetFalse", 0.2f);
	}

	void SetFalse()
	{
		isPressBtn = false;
	}
//------------------------------------------------------------
	void Tutorial()
	{
		GameObject obj = Instantiate (handTut, Vector3.zero, Quaternion.identity);
		obj.transform.SetParent (transform);
		obj.transform.localPosition = new Vector3 (-0.25f, -0.056f, 0);
		obj.transform.eulerAngles = new Vector3 (0, 0, -35);
	}
//------------------------------------------------------------
	public void EndGame(bool check)
	{
//		Time.timeScale = 0;

		switch (check) {
		case true://win game
			endPanel.SetActive(true);
			winPanel.SetActive (true);
			lastMove.text = GameManager.instance.levelSwipeTime.ToString ();
			score.text = (GameManager.instance.levelSwipeTime * 2).ToString ();
			AdsControl.Instance.showAds ();
			break;
		case false:
			endPanel.SetActive(true);
			losePanel.SetActive (true);
			AdsControl.Instance.showAds ();
			break;
		}
			
	}
//------------------------------------------------------------
	public void WinGame(int index)
	{
		switch (index) {
		case 0:
			Time.timeScale = 1;
			Application.LoadLevel ("Menu");
			break;
		case 1:
			Time.timeScale = 1;
			Application.LoadLevel (Application.loadedLevel);
			break;
		case 2:
			Time.timeScale = 1;
			Menu.selectedLevel += 1;
			Application.LoadLevel (Application.loadedLevel);
			break;
		}
	}
//------------------------------------------------------------
	public void LoseGame(int index)
	{
		switch (index) {
		case 0:
			Time.timeScale = 1;
			Application.LoadLevel ("Menu");
			break;
		case 1:
			Time.timeScale = 1;
			Application.LoadLevel (Application.loadedLevel);
			break;
		}
	}
//------------------------------------------------------------
	public void SoundSys()
	{
		switch(PlayerPrefs.GetInt("sound"))
		{
		case 0:
			SoundManager.instance.source.volume = 1;
			soundImg.sprite = soundOn;
			break;
		case 1:
			SoundManager.instance.source.volume = 0;
			soundImg.sprite = soundOff;
			break;
		}
	}
//------------------------------------------------------------
	public void SetSound()
	{
		switch (PlayerPrefs.GetInt ("sound")) {
		case 0:
			PlayerPrefs.SetInt ("sound", 1);
			SoundSys ();
			break;
		case 1:
			PlayerPrefs.SetInt ("sound", 0);
			SoundSys ();
			break;
		}
	}
//------------------------------------------------------------
///<Summary>
	///Fade joystick
	///</Summary>
	/// 
	public void FadeJoyStick()
	{
		if (isFade) {
			joyBall.color = new Color (1, 1, 1,75f/255);
			joyCircle.color = new Color (1, 1, 1,75f/255);
		} else {
			joyBall.color = new Color (1, 1, 1, 1);
			joyCircle.color = new Color (1, 1, 1, 1);
		}
	}
}

[System.Serializable]
public class CamPosData
{
	public float xMin,xMax, yMin,yMax;
}
