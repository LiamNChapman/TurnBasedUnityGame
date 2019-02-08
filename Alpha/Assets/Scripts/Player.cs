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
	public GameObject cancelButton;
	public bool moved = false;
	public RectTransform abilityButtonsTransform;
	bool stunActive = false;
	bool distractActive = false;
	public GameObject UIManager;
	public int abilityCharges = 1;
	public bool haveKey = false;
	public ParticleSystem FlourPoof;
	float speed = 4.0f;
	Vector3Int coordinate;
	bool clicked = false;
	bool blocked = false;
	bool validTile = false;
	bool kidDead = false;
	public Transform dad;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(!moved){
			move();
		}
		if(stunActive){
			stunMovement();
		} else if(distractActive){
			throwDistraction();
		}
	}

	void move(){
		if(Input.GetMouseButtonDown(0) && !clicked) {
			//get the coordinates from the mouse click.
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        	coordinate = grid.WorldToCell(mouseWorldPos);

			GameObject gate;
			

			if(GameObject.Find("Gate") != null){
				gate = GameObject.Find("Gate");
				blocked = coordinate == gate.transform.position;
				if(blocked && haveKey){
					blocked = false;
					gate.SetActive(false);
					haveKey = false;
				}
			}
				// bool values to check if the distance the player has clicked to move to
			// is only 1 tile either up or down, or left or right.
			bool leftAndRight = Math.Abs((this.transform.position.x-0.5f) - coordinate.x) == 1;
			bool upAndDown = Math.Abs((this.transform.position.y-0.5f) - coordinate.y) == 1;
			bool leftAndRight2 = Math.Abs((this.transform.position.x-0.5f) - coordinate.x) == 0;
			bool upAndDown2 = Math.Abs((this.transform.position.y-0.5f) - coordinate.y) == 0;
			if((upAndDown && leftAndRight2) || (leftAndRight && upAndDown2)){
				

				// Check if the tile isn't a path.
				if(tilemap.GetTile(coordinate).name != "NonPath"){
					clicked = true;
					validTile = true;
				}
			}
			

		}
			
			if(!blocked && clicked){
				//Check to make sure the player doesnt move diagonal.
				if(validTile){
					if(kidDead == false) {
						foreach (Transform child in dad) {
             				Destroy(child.gameObject);
        				}
						kidDead = true;
					}
					Vector3 newPosition = (Vector3)coordinate;

					// Shift the players character to the center of the tile.
					newPosition.x += 0.5f;
					newPosition.y += 0.5f;
					float step = speed * Time.deltaTime;
					this.transform.position = Vector3.MoveTowards(transform.position, newPosition, step);

					if(transform.position == newPosition){
						GameObject flour;
						if(GameObject.Find("FlourItem") != null){
							flour = GameObject.Find("FlourItem");
							if(transform.position == flour.transform.position){
								Destroy(flour);
								abilityCharges++;
							}
						}

						GameObject key;
						if(GameObject.Find("Key") != null){
							key = GameObject.Find("Key");
							if(transform.position == key.transform.position){
								Destroy(key);
								haveKey = true;
							}
						}

						//check if the player is moving onto an enenmy
						foreach(GameObject enemies in TurnManager.enemies){
							Vector3 checkPos = (Vector3)coordinate;
							checkPos.x += 0.5f;
							checkPos.y += 0.5f;
							if(enemies.transform.position == checkPos){
								if(enemies.GetComponent<Scout>() != null) {
									if(!enemies.GetComponent<Scout>().isStunned){
										TurnManager.killed();
									}
								} else if(enemies.GetComponent<Ranger>() != null) {
									if(!enemies.GetComponent<Ranger>().isStunned){
										TurnManager.killed();
									}
								} else if(enemies.GetComponent<Bezerker>() != null) {
									if(!enemies.GetComponent<Bezerker>().isStunned){
										TurnManager.killed();
									}
								} else if(enemies.GetComponent<Warrior>() != null) {
									if(!enemies.GetComponent<Warrior>().isStunned){
										TurnManager.killed();
									}
								}
							}
						}
						if(tilemap.GetTile(coordinate).name == "WinTile"){
							UIManager = GameObject.Find("Canvas");
							UIManager.GetComponent<UIManager>().winLevel();					
						}
					
						if(abilityCharges > 0){
							abilityButtons.SetActive(true);
						}
						moved = true;
						clicked = false;
						validTile = false;
						kidDead = false;
						TurnManager.nextState();
					}
				}
			} 
		
		abilityButtonsTransform.position = this.transform.position + Vector3.up;
	}

	public void distract() {
		distractActive = true;
		moved = true;
		clicked = false;
		abilityButtons.SetActive(false);
		cancelButton.SetActive(true);
	}

	void throwDistraction(){
		if(Input.GetMouseButtonDown(0)) {
			//get the coordinates from the mouse click.
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        	Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);

			bool enemyThere = false;

			// Check if the tile isn't a path.
			if(tilemap.GetTile(coordinate).name != "NonPath"){
			foreach(GameObject enemies in TurnManager.enemies){
				Vector3 checkPos = (Vector3)coordinate;
				checkPos += Vector3.left;
				checkPos.x += 0.5f;
				checkPos.y += 0.5f;
				if(enemies.transform.position == checkPos){
					enemyThere = true;
					if(enemies.GetComponent<Scout>() != null) {
						enemies.GetComponent<Scout>().facing = 3;
					} else if(enemies.GetComponent<Ranger>() != null) {
						enemies.GetComponent<Ranger>().facing = 3;
						enemies.GetComponent<Ranger>().isStunned = true;
						enemies.GetComponent<Ranger>().stunLeft = 1;
						enemies.GetComponent<Ranger>().gotDistracted= true;
					} else if(enemies.GetComponent<Bezerker>() != null) {
						enemies.GetComponent<Bezerker>().facing = 3;
					} else if(enemies.GetComponent<Warrior>() != null) {
						enemies.GetComponent<Warrior>().facing = 3;
					}
				}
				checkPos = (Vector3)coordinate;
				checkPos += Vector3.down;
				checkPos.x += 0.5f;
				checkPos.y += 0.5f;
				if(enemies.transform.position == checkPos){
					enemyThere = true;
					if(enemies.GetComponent<Scout>() != null) {
						enemies.GetComponent<Scout>().facing = 4;
					} else if(enemies.GetComponent<Ranger>() != null) {
						enemies.GetComponent<Ranger>().facing = 4;
						enemies.GetComponent<Ranger>().isStunned = true;
						enemies.GetComponent<Ranger>().stunLeft = 1;
						enemies.GetComponent<Ranger>().gotDistracted= true;
					} else if(enemies.GetComponent<Bezerker>() != null) {
						enemies.GetComponent<Bezerker>().facing = 4;
					} else if(enemies.GetComponent<Warrior>() != null) {
						enemies.GetComponent<Warrior>().facing = 4;
					}
				}
				checkPos = (Vector3)coordinate;
				checkPos += Vector3.right;
				checkPos.x += 0.5f;
				checkPos.y += 0.5f;
				if(enemies.transform.position == checkPos){
					enemyThere = true;
					if(enemies.GetComponent<Scout>() != null) {
						enemies.GetComponent<Scout>().facing = 1;
					} else if(enemies.GetComponent<Ranger>() != null) {
						enemies.GetComponent<Ranger>().facing = 1;
						enemies.GetComponent<Ranger>().isStunned = true;
						enemies.GetComponent<Ranger>().stunLeft = 1;
						enemies.GetComponent<Ranger>().gotDistracted= true;
					} else if(enemies.GetComponent<Bezerker>() != null) {
						enemies.GetComponent<Bezerker>().facing = 1;
					} else if(enemies.GetComponent<Warrior>() != null) {
						enemies.GetComponent<Warrior>().facing = 1;
					}
				}
				checkPos = (Vector3)coordinate;
				checkPos += Vector3.up;
				checkPos.x += 0.5f;
				checkPos.y += 0.5f;
				if(enemies.transform.position == checkPos){
					enemyThere = true;
					if(enemies.GetComponent<Scout>() != null) {
						enemies.GetComponent<Scout>().facing = 2;
					} else if(enemies.GetComponent<Ranger>() != null) {
						enemies.GetComponent<Ranger>().facing = 2;
						enemies.GetComponent<Ranger>().isStunned = true;
						enemies.GetComponent<Ranger>().stunLeft = 1;
						enemies.GetComponent<Ranger>().gotDistracted= true;
					} else if(enemies.GetComponent<Bezerker>() != null) {
						enemies.GetComponent<Bezerker>().facing = 2;
					} else if(enemies.GetComponent<Warrior>() != null) {
						enemies.GetComponent<Warrior>().facing = 2;
					}
				}
			}
			}
			if(tilemap.GetTile(coordinate).name != "NonPath" && enemyThere){
				Vector3 distract = (Vector3) coordinate;
				distract.x += 0.5f;
				distract.y += 0.5f;
				Instantiate(FlourPoof, distract, Quaternion.identity);
				abilityCharges--;
				distractActive = false;
				cancelButton.SetActive(false);
				TurnManager.nextState();
			}
		}
	}

	public void stun() {
		stunActive = true;
		moved = true;
		clicked = false;
		abilityButtons.SetActive(false);
		cancelButton.SetActive(true);
		//TurnManager.nextState();
	}

	void stunMovement(){
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
					if(enemies.GetComponent<Scout>() != null) {
						enemies.GetComponent<Scout>().isStunned = true;
						enemies.GetComponent<Scout>().stunLeft = 3;
					} else if(enemies.GetComponent<Ranger>() != null) {
						enemies.GetComponent<Ranger>().isStunned = true;
						enemies.GetComponent<Ranger>().stunLeft = 3;
					} else if(enemies.GetComponent<Bezerker>() != null) {
						enemies.GetComponent<Bezerker>().isStunned = true;
						enemies.GetComponent<Bezerker>().stunLeft = 3;
					} else if(enemies.GetComponent<Warrior>() != null) {
						enemies.GetComponent<Warrior>().isStunned = true;
						enemies.GetComponent<Warrior>().stunLeft = 3;
					}
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
					Instantiate(FlourPoof, newPosition, Quaternion.identity);
					this.transform.position = newPosition;
					abilityCharges--;
					stunActive = false;
					cancelButton.SetActive(false);
					TurnManager.nextState();
				}
			}
		}
	}

	
	public void cancel() {
		stunActive = false;
		distractActive = false;
		moved = false;
		clicked = false;
		abilityButtons.SetActive(true);
		cancelButton.SetActive(false);
	}
}
