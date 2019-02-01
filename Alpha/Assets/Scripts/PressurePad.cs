using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour {
	public bool activated = false;
	public Gate gate;
	Vector3 thisPosistion;
	public int check = 0;

	// Use this for initialization
	void Start () {
		thisPosistion = transform.position;
		thisPosistion.x += 0.5f;
		thisPosistion.y += 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 player = GameObject.Find("Player").transform.position;
		if(check == 0){
			foreach(GameObject enemy in TurnManager.enemies){
				if(enemy.transform.position == thisPosistion){
					activated = true;
					check = 1;
				}
			}
			if(player == thisPosistion){
				activated = true;
				check = 1;
			}
		}
		if(check == 1){
			gate.open = true;
		} else {
			gate.open = false;
		}
		//check = 0;
	}
}
