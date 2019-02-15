using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour {
	public bool activated = false;
	public Gate gate;
	Vector3 thisPosistion;
	public int check = 0;
	AudioSource onpad;

	// Use this for initialization
	void Start () {
		thisPosistion = transform.position;
		thisPosistion.x += 0.5f;
		thisPosistion.y += 0.5f;
		onpad = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		checkPad();
	}
	public void checkPad(){		
		Vector3 player = GameObject.Find("Player").transform.position;
			foreach(GameObject enemy in TurnManager.enemies){
				if(enemy.transform.position == thisPosistion){
					check = 1;
				}
			}
			if(player == thisPosistion){
				check = 1;
			}
		if(check == 1){
			gate.open = true;
			gate.gameObject.SetActive(false);
		} else {
			gate.open = false;
			gate.gameObject.SetActive(true);
			onpad.Play();
		}
		check = 0;
	}
}
