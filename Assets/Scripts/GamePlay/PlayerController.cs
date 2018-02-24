using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    left,
    right
}
public class PlayerController : MonoBehaviour {

    public Rigidbody2D rb;
    public SpriteRenderer spr;
    public Sprite idle, fly,attack;
	public Vector3[] startPos;
	public float horiFore,verFoce;

    private Vector2 a, b, c, d;
    private bool canMove,attacking, finishGame;
    private Direction playerDir;
	private Vector3 temp1, temp2 , temp3, temp4;
	private bool limAchive;
	private bool startCheck;
	private float canhBen, canhDay, gocDinh;
	public bool right,startMove;
	private Plane[] planes;
	public Collider2D col;

//------------------------------------------------------------
	private	void Awake ()
	{
		transform.position = startPos [Menu.selectedLevel];	

		right = true;

		Invoke ("StartCheckOutScreen", 1.5f);
	}
//------------------------------------------------------------
    void Update() {
		
		ChangDir1();

        ChangeSprite();

		FinishGame ();

		if (startCheck ) {
			if (!CheckOutScreen ()) {
				finishGame = true;
				gameObject.SetActive (false);
				LoseGame ();
			}
		}
    }

//------------------------------------------------------------
	///<Summary>
	///Player move control
	///</Summary>
    void MoveControl()
    {
        
		if (Input.GetMouseButtonDown (0)) {
			//Slow motion frame
			Time.timeScale = 0.2f;

			// 
			temp1 = new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);

			//Setactive the fake joystick
			GameManager.instance.circle.SetActive(true);
			GameManager.instance.circle.transform.position = new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y, 0);
        }
		if (Input.GetMouseButtonUp (0)) {
			//
			if (!GameManager.instance.isPressBtn) {
				if (GameManager.instance.levelSwipeTime > 0) {
				SoundManager.instance.source.PlayOneShot (SoundManager.instance.whoosh [Random.Range (0, 5)]);

				temp2 = new Vector3 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);

				if (temp1.x > temp2.x) {
						float xForce = Vector3.Distance (new Vector3 (temp1.x, 0, 0), new Vector3 (temp2.x, 0, 0)) * horiFore;
						float yForce = Vector3.Distance (new Vector3 (0, temp1.y, 0), new Vector3 (0, temp2.y, 0)) * verFoce;

					playerDir = Direction.right;

					if (temp1.y > temp2.y) {
						rb.AddForce (new Vector2 (xForce, yForce));
					} else {
						rb.AddForce (new Vector2 (xForce, -yForce));
					}

				} else {
						float xForce = Vector3.Distance (new Vector3 (temp1.x, 0, 0), new Vector3 (temp2.x, 0, 0)) * horiFore;
						float yForce = Vector3.Distance (new Vector3 (0, temp1.y, 0), new Vector3 (0, temp2.y, 0)) * verFoce;

					playerDir = Direction.left;

					if (temp1.y > temp2.y) {
						rb.AddForce (new Vector2 (-xForce, yForce));
					} else {
						rb.AddForce (new Vector2 (-xForce, -yForce));
					}
				}

				//set swipe time
			
					GameManager.instance.levelSwipeTime -= 1;
					GameManager.instance.SetSwipeTime (GameManager.instance.levelSwipeTime);


					//normal frame
					Time.timeScale = 1;
				}
			}
		}

    }
//------------------------------------------------------------
	///<Summary>
	///Change player direction when swipe
	///</Summary>
    void ChangDir()
    {
		switch (playerDir) {
		case Direction.left:
			
			transform.localScale = new Vector3 (1, 1, 1);

			break;

		case Direction.right:
			
			transform.localScale = new Vector3 (-1, 1, 1);

			break;
		}
    }

//------------------------------------------------------------
	///<Summary>
	///Change direction by when use joystick
	///</Summary>
	/// 
	void ChangDir1()
	{
		switch (right) {
		case true:

			transform.localScale = new Vector3 (1, 1, 1);

			break;

		case false:

			transform.localScale = new Vector3 (-1, 1, 1);

			break;
		}
	}
//------------------------------------------------------------
	///<Summary>
	///Change player sprite
	///</Summary>
    void ChangeSprite()
    {
		if (!attacking) {
			if (rb.velocity == Vector2.zero) {

				spr.sprite = idle;

				transform.eulerAngles = Vector3.zero;

			} else {
			
				spr.sprite = fly;

			}
		} else {
			spr.sprite = attack;

			if (rb.velocity == Vector2.zero) {
				attacking = false;
			}
		}
    }
//------------------------------------------------------------
	void StartCheckOutScreen()
	{
		startCheck = true;
	}
//------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag) {

		case "enemy":
			attacking = true;

			SoundManager.instance.source.PlayOneShot (SoundManager.instance.shout [Random.Range (0, 3)]);
			SoundManager.instance.source.PlayOneShot (SoundManager.instance.sword [Random.Range (0, 2)]);

			switch (collision.gameObject.GetComponent<Objectt> ().enemyType) {
			case 1:
				Instantiate (GameManager.instance.enemy1.gameObject, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
				Instantiate (GameManager.instance.blood1.gameObject, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
				break;
			case 2:
				Instantiate (GameManager.instance.enemy2.gameObject, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
				Instantiate (GameManager.instance.blood2.gameObject, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
				break;
			}
			collision.gameObject.SetActive (false);

			GameManager.instance.killedEnemy += 1;

		///<Summary>
		///Kill Sound
		///</Summary>
			if (GameManager.instance.killedEnemy == 1) {
				SoundManager.instance.source.PlayOneShot (SoundManager.instance.monsterKill);
			} else if (GameManager.instance.killedEnemy == 2) {
				SoundManager.instance.source.PlayOneShot (SoundManager.instance.doubleKill);
			} else if (GameManager.instance.killedEnemy == 3) {
				SoundManager.instance.source.PlayOneShot (SoundManager.instance.tripleKill);
			} else {
				switch (Random.Range (0, 2)) {
				case 0:
					SoundManager.instance.source.PlayOneShot (SoundManager.instance.goodLike);
					break;
				case 1:
					SoundManager.instance.source.PlayOneShot (SoundManager.instance.ultraKill);
					break;
				}
			}

		///<Summary>
		///kill all enemy =>> open level finish box
		///</Summary>
			if (GameManager.instance.killedEnemy == GameManager.instance.totalEnemy) {
			
				for (int i = 0; i < GameManager.instance.wallBox.Count; i++) {
					GameManager.instance.wallBox [i].SetActive (false);
				}
			}
			break;

		case "levelcomplete":

			collision.gameObject.GetComponent<SpriteRenderer> ().sprite = GameManager.instance.unlockedBox;

			gameObject.SetActive (false);

			//unlock next level
			if (Menu.selectedLevel == PlayerPrefs.GetInt ("level")) {
				PlayerPrefs.SetInt ("level", PlayerPrefs.GetInt ("level") + 1);
			}

			Invoke ("WinGame", 0.2f);
			break;

		case "gem":
			collision.gameObject.SetActive (false);

			SoundManager.instance.source.PlayOneShot (SoundManager.instance.gemClip);

			GameManager.instance.collectedGem += 1;
			if (GameManager.instance.collectedGem == 0) {
				return;
			} else if (GameManager.instance.collectedGem > 0 && GameManager.instance.collectedGem < (int)GameManager.instance.totalGem * 0.3f) {
				PlayerPrefs.SetInt ("gem" + Menu.selectedLevel.ToString (), 1);
			} else if (GameManager.instance.collectedGem > (int)GameManager.instance.totalGem * 0.3f && GameManager.instance.collectedGem < (int)GameManager.instance.totalGem * 0.6f) {
				PlayerPrefs.SetInt ("gem" + Menu.selectedLevel.ToString (), 2);
			} else {
				PlayerPrefs.SetInt ("gem" + Menu.selectedLevel.ToString (), 3);
			}

			break;
		}
	}
//------------------------------------------------------------
	void WinGame()
	{
		Instantiate(GameManager.instance.winParticle.gameObject,new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2f,0,0)).x,Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height/2f,0)).y,0),Quaternion.identity);
		SoundManager.instance.source.PlayOneShot (SoundManager.instance.winClip);
		GameManager.instance.winGame = true;
		GameManager.instance.EndGame (GameManager.instance.winGame);
	}
//------------------------------------------------------------
	void LoseGame()
	{
//		AdsControl.Instance.showAds ();
		SoundManager.instance.source.PlayOneShot (SoundManager.instance.gameOver);
		GameManager.instance.winGame = false;
		GameManager.instance.EndGame (GameManager.instance.winGame);
		gameObject.SetActive (false);
	}
//------------------------------------------------------------
	void FinishGame()
	{
		if (GameManager.instance.levelSwipeTime == 0) {
			if (!finishGame) {
				finishGame = true;
				Invoke ("LoseGame", 1);
			}
		}
	}
//------------------------------------------------------------
	///<Summary>
	///Player out screen
	///</Summary>
	private bool CheckOutScreen()
	{
		planes = GeometryUtility.CalculateFrustumPlanes (Camera.main);
		if (GeometryUtility.TestPlanesAABB (planes, col.bounds)) {
			return true;
		} else {
			return false;
		}
	}
}
