using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove2 : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		move();
	}
	public void move() {
		transform.position += Vector3.left;
		LevelManager.enemyMoves--;
	}
}
