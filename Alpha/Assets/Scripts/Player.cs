using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour {

	public Grid grid;
	public Tilemap tilemap;
	public GameObject abilityButtons;

	public bool moved = false;
	bool stunActive = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!moved){
			move();
		}
		if(stunActive){
			if(Input.GetMouseButtonDown(0)) {
			//get the coordinates from the mouse click.
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        	Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);

			// bool values to check if the distance the player has clicked to move to
			// is only 1 tile either up or down, or left or right.
			bool leftAndRight = Math.Abs((this.transform.position.x-0.5f) - coordinate.x) == 1;
			bool upAndDown = Math.Abs((this.transform.position.y-0.5f) - coordinate.y) == 1;
			bool leftAndRight2 = Math.Abs((this.transform.position.x-0.5f) - coordinate.x) == 0;
			bool upAndDown2 = Math.Abs((this.transform.position.y-0.5f) - coordinate.y) == 0;

			bool enemyThere = false;

			foreach(GameObject enemies in TurnManager.enemies){
				Vector3 checkPos = (Vector3)coordinate;
				checkPos.x += 0.5f;
				checkPos.y += 0.5f;
				if(enemies.transform.position == checkPos){
					enemyThere = true;
				}
			}

			// Check if the tile isn't a path.
			if(tilemap.GetTile(coordinate).name != "NonPath" && enemyThere){

				//Check to make sure the player doesnt move diagonal.
				if((upAndDown && leftAndRight2) || (leftAndRight && upAndDown2)){
					Vector3 newPosition = (Vector3)coordinate;

					// Shift the players character to the center of the tile.
					newPosition.x += 0.5f;
					newPosition.y += 0.5f;
					this.transform.position = newPosition;
					stunActive = false;
					TurnManager.nextState();
				}
			}
		}
		}
	}

	void move(){
		if(Input.GetMouseButtonDown(0)) {
			//get the coordinates from the mouse click.
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        	Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);

			// bool values to check if the distance the player has clicked to move to
			// is only 1 tile either up or down, or left or right.
			bool leftAndRight = Math.Abs((this.transform.position.x-0.5f) - coordinate.x) == 1;
			bool upAndDown = Math.Abs((this.transform.position.y-0.5f) - coordinate.y) == 1;
			bool leftAndRight2 = Math.Abs((this.transform.position.x-0.5f) - coordinate.x) == 0;
			bool upAndDown2 = Math.Abs((this.transform.position.y-0.5f) - coordinate.y) == 0;

			// Check if the tile isn't a path.
			if(tilemap.GetTile(coordinate).name != "NonPath" ){

				//Check to make sure the player doesnt move diagonal.
				if((upAndDown && leftAndRight2) || (leftAndRight && upAndDown2)){
					Vector3 newPosition = (Vector3)coordinate;

					// Shift the players character to the center of the tile.
					newPosition.x += 0.5f;
					newPosition.y += 0.5f;
					this.transform.position = newPosition;
					abilityButtons.SetActive(true);
					moved = true;
				}
			}
		}
	}

	public void distract() {

		abilityButtons.SetActive(false);
		TurnManager.nextState();
	}

	public void stun() {
		stunActive = true;
		abilityButtons.SetActive(false);
		//TurnManager.nextState();
	}

	public void skip() {

		abilityButtons.SetActive(false);
		TurnManager.nextState();
	}
}
