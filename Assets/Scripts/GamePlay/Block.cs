using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
	wood,
	brick
}

public class Block : MonoBehaviour {
	public BlockType blockType;
	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Player") {
			SoundManager.instance.source.PlayOneShot (SoundManager.instance.breakBox);

			switch (blockType) {

			case BlockType.wood:
				Instantiate (GameManager.instance.wood.gameObject, transform.position, transform.rotation);
				gameObject.SetActive (false);
				break;

			case BlockType.brick:
				Instantiate (GameManager.instance.brick.gameObject, transform.position, transform.rotation);
				gameObject.SetActive (false);
				break;
			}
		}
	}

}
