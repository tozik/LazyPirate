﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace GeekGame.Input{
	public class JoystickMove : MonoBehaviour ,IDragHandler,IEndDragHandler
	{
		// remember turning is with joystick rotate
		// calculate the h and the v, then give it to PlayerMovement

		public static JoystickMove instance=null;

		public float _speed=6f;
		public Rigidbody2D rb;
		public PlayerController playerIns;
		[Tooltip("the joystick radius ")]
		public float R=90f;

		private float _r;
		private float a, b;
		private Vector2 centerPos;

		private float _h;
		private float _v;

		public float H{
			get{return _h;}
		}
		public float V{
			get{return _v;}
		}

		void Awake(){

			if(instance!=null){
				Destroy(this.gameObject);
			}else{
				instance=this;
			}



		}
		void Start(){

			_r=1f*Screen.width/960f*R; //this to calculate the scale of screen

			centerPos=GetComponent<RectTransform>().position;

		}






		void SetHAndF(Vector2 pos){ //Horizontall and Vertical axes
			
			Vector2 diff=pos-centerPos;
			float distance=diff.magnitude;


			if(distance>_r){
				pos=centerPos+diff/distance*_r;

			}
			GetComponent<RectTransform>().position=pos;


			Vector2 move=pos-centerPos;

			_h=move.x;
			_v=move.y;

			Time.timeScale = 0.2f;
		}

		public void OnDrag(PointerEventData data)
		{	

			Vector2 newPos =new Vector2(data.position.x-20f,data.position.y-20f);

			//clamp the sprite

			SetHAndF(newPos);

			a = _h;
			b = _v;

		}

		public void OnEndDrag(PointerEventData data){

			Debug.Log("End Drag"+centerPos);
			GetComponent<RectTransform>().position=centerPos;
			SetHAndF(centerPos);
			if (a > 0) {
				rb.AddForce (new Vector2 (a * -7, b * -7));
				playerIns.right = false;
			} else {
				rb.AddForce (new Vector2 (a * -7, b * -7));
				playerIns.right = true;
			}

			GameManager.instance.levelSwipeTime -= 1;
			GameManager.instance.SetSwipeTime (GameManager.instance.levelSwipeTime);
			if (GameManager.instance.levelSwipeTime == 0) {
				GameManager.instance.winGame = false;
				GameManager.instance.EndGame (GameManager.instance.winGame);
			}

			Time.timeScale = 1;

			SoundManager.instance.source.PlayOneShot (SoundManager.instance.whoosh [Random.Range (0, 5)]);
		}

	}
}