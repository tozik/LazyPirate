using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObjectType
{
	enemy,
	wallBox,
	gem
}

public class Objectt : MonoBehaviour {
	public ObjectType type;
	public int enemyType;
	void Start ()
	{
		switch (type) {

		case ObjectType.wallBox:
			GameManager.instance.wallBox.Add (gameObject);	
			break;
		case ObjectType.enemy:
			GameManager.instance.totalEnemy += 1;
			GameManager.instance.enemy.Add (gameObject);	
			break;
		case ObjectType.gem:
			GameManager.instance.totalGem += 1;
			GameManager.instance.gem.Add (gameObject);	
			break;
		}

	}
}
