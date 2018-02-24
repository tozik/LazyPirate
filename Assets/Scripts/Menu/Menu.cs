using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

	public static int selectedLevel = 1;

	public GameObject startBtn;
	public GameObject logo;

	[Header("Level Selector")]
	public GameObject[] pageSelector;
	public GameObject nextPageBtn, prevPageBtn;
	public GameObject[] levelButton;
	public Sprite lockedLevel;
	public Sprite[] unlockedLevel;
	public Image background;
	public Sprite[] bg;
	public Sprite[] gemStar;

//------------------------------------------------------------
	void Awake ()
	{
		//PlayerPrefs.SetInt ("level", 19);
		//PlayerPrefs.DeleteAll();
		CheckUnlockedLevel ();	
		CheckGemStar ();

		print (Vector3.right);
	}
//------------------------------------------------------------
	///<Summary>
	///Press start button
	///</Summary>
	public void MenuGame(int index)
	{
		switch (index) {
		case 0:
			startBtn.SetActive (false);
			logo.SetActive (false);
			pageSelector [0].SetActive (true);
			nextPageBtn.SetActive (true);
			prevPageBtn.SetActive (true);
			break;
		}
	}
//------------------------------------------------------------
	public void SelectLevel(int index)
	{
		selectedLevel = index;
		Application.LoadLevel("Game");
	}
//------------------------------------------------------------
	///<Summary>
	///Show unlocked level
	///</Summary>
	private void CheckUnlockedLevel()
	{
		for (int i = 0; i <= PlayerPrefs.GetInt ("level"); i++) {
			levelButton [i].GetComponent<Image> ().sprite = unlockedLevel [0];
			levelButton [i].GetComponent<Image> ().raycastTarget = true;
		}
	}
//------------------------------------------------------------
	///<Summary>
	///Press next and prev button
	///</Summary>
	public void NextAndPrev(int index)
	{
		switch (index) {
		case 0:
			for (int i = pageSelector.Length-1; i > 0; i--) {
				print (i);
				if (i > 0) {
					if (pageSelector [i].activeInHierarchy) {
						background.sprite = bg [i-1];
						pageSelector [i].SetActive (false);
						pageSelector [i - 1].SetActive (true);
						break;
					}
				} 
				if(pageSelector [0].activeInHierarchy) { 
					startBtn.SetActive (true);
					logo.SetActive (true);
					pageSelector [0].SetActive (false);
					nextPageBtn.SetActive (false);
					prevPageBtn.SetActive (false);
				}
			}
			break;
		case 1:
			for (int i = 0; i < pageSelector.Length; i++) {
				if (i < pageSelector.Length - 1) {
					if (pageSelector [i].activeInHierarchy) {
						background.sprite = bg [i+1];
						pageSelector [i].SetActive (false);
						pageSelector [i + 1].SetActive (true);
						break;
					}
				} else  {
					return;
				}
			}
			break;
		}
	}
//------------------------------------------------------------
	///<Summary>
	///Show star level when collect enough gems
	///</Summary>
	private void CheckGemStar()
	{
		for (int i = 0; i < PlayerPrefs.GetInt("level"); i++) {
			switch (PlayerPrefs.GetInt ("gem" + i.ToString ())) {
			case 0:
				levelButton [i].GetComponent<Image> ().sprite = gemStar [0];
			break;
			case 1:
				levelButton [i].GetComponent<Image> ().sprite = gemStar [1];
				break;
			case 2:
				levelButton [i].GetComponent<Image> ().sprite = gemStar [2];
				break;
			case 3:
				levelButton [i].GetComponent<Image> ().sprite = gemStar [3];
				break;
			}
		}
	}
}

