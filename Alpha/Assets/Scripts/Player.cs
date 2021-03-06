﻿using System.Collections;
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
	bool stunActive = false;
	bool distractActive = false;
	public GameObject UIManager;
	public int abilityCharges = 1;
	public bool haveKey = false;
	public ParticleSystem FlourPoof;
	float speed = 3.0f;
	Vector3Int coordinate;
	bool clicked = false;
	bool blocked = false;
	bool validTile = false;
	bool kidDead = false;
	public Transform dad;
	public Transform highLight;
	public int facing;
	bool changefacing;
	public Sprite[] spriteList;
	public Sprite[] spriteListLeft;
	public Sprite[] spriteListDown;
	public Sprite[] spriteListUp;
	public Sprite[] spriteListRight;
	int spriteCounter = 0;
	int animationDelay = 0;
	public AudioClip keypick;
	public AudioClip poof;
	public AudioClip flourpick;
	AudioSource Key;
	AudioSource Flour;
	AudioSource Stun;

	// Use this for initialization
	void Start () {
		Key = AddAudio(keypick);
		Flour = AddAudio(flourpick);
		Stun = AddAudio(poof);
	}

	public AudioSource AddAudio(AudioClip clip){
		AudioSource newAudio = gameObject.AddComponent<AudioSource>();
		newAudio.loop = false;
		newAudio.playOnAwake = false;
		newAudio.clip = clip;
     	return newAudio; 
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
					spriteCounter = 0;
					Vector3 tempCoord = coordinate;
					tempCoord.x += 0.5f;
					tempCoord.y += 0.5f;
					Vector3 temp = transform.position;
					temp += Vector3.left;
					if(tempCoord == temp){
						facing = 1;
					} 
					temp = transform.position;
					temp += Vector3.down;
					if (tempCoord == temp){
						facing = 2;
					}
					temp = transform.position;
					temp += Vector3.right;
					if (tempCoord == temp){
						facing = 3;
					}
					temp = transform.position;
					temp += Vector3.up;
					if(tempCoord == temp){
						facing = 4;
					}
					changefacing = true;
					if(blocked){
						clicked = false;
					}
				}
			}
			

		}
			
			if(!blocked && clicked){
				//Check to make sure the player doesnt move diagonal.
				if(validTile){
					if(kidDead == false) {
						foreach (Transform child in dad.transform) {
             				Destroy(child.gameObject);
        				}
						kidDead = true;
					}
					Vector3 newPosition = (Vector3)coordinate;
					
					// Shift the players character to the center of the tile.
					newPosition.x += 0.5f;
					newPosition.y += 0.5f;

					if(animationDelay == 15){
						animationDelay = 0;
					}

					if(animationDelay == 0){
						if(facing == 1){
							GetComponent<SpriteRenderer>().sprite = spriteListLeft[spriteCounter];
							spriteCounter++;
							if(spriteCounter > 3){
								spriteCounter = 0;
							}
						}
						if(facing == 2){
							GetComponent<SpriteRenderer>().sprite = spriteListDown[spriteCounter];
							spriteCounter++;
							if(spriteCounter > 3){
								spriteCounter = 0;
							}
						}
						if(facing == 3){
							GetComponent<SpriteRenderer>().sprite = spriteListRight[spriteCounter];
							spriteCounter++;
							if(spriteCounter > 3){
								spriteCounter = 0;
							}
						}
						if(facing == 4){
							GetComponent<SpriteRenderer>().sprite = spriteListUp[spriteCounter];
							spriteCounter++;
							if(spriteCounter > 3){
								spriteCounter = 0;
							}
						}
					}
					animationDelay++;


					float step = speed * Time.deltaTime;
					transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
					float x =  (float)Math.Round(transform.position.x * 100f)/100f;
					float y = (float)Math.Round(transform.position.y * 100f)/100f;
					transform.position = new Vector3(x, y, 0);

					if(transform.position == newPosition){
						animationDelay = 0;
						if(changefacing){
							GetComponent<SpriteRenderer>().sprite = spriteList[facing-1];
							changefacing = false;
						}

						GameObject flour;
						GameObject flourimage;
						if(GameObject.Find("FlourItem") != null){
							flour = GameObject.Find("FlourItem");
							flourimage = GameObject.Find("FlourImage");
							if(transform.position == flour.transform.position){
								Flour.Play();
								Destroy(flour);
								Destroy(flourimage);
								abilityCharges++;
							}
						}

						GameObject key;
						GameObject keyimage;
						if(GameObject.Find("Key") != null){
							key = GameObject.Find("Key");
							keyimage = GameObject.Find("KeyImage");
							if(transform.position == key.transform.position){
								Key.Play();
								Destroy(keyimage);
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
	}

	public void distract() {
		distractActive = true;
		moved = true;
		clicked = false;
		abilityButtons.SetActive(false);
		cancelButton.SetActive(true);
		foreach (Transform child in dad.transform) {
			if(child.GetComponent<SpriteRenderer>().color != Color.red) {
				Destroy(child.gameObject);
			}        	
        }
		foreach(GameObject enemy in TurnManager.enemies) {
			if(enemy.GetComponent<Bezerker>() == null || enemy.GetComponent<Bezerker>().enraged == false) { 
				if(tilemap.GetTile(grid.WorldToCell(enemy.transform.position) + Vector3Int.up).name != "NonPath") {
					Transform x = Instantiate(highLight, grid.WorldToCell(enemy.transform.position) + Vector3Int.up, transform.rotation);
					x.GetComponent<SpriteRenderer>().color = Color.cyan;
					x.parent = dad;
				}
				if(tilemap.GetTile(grid.WorldToCell(enemy.transform.position) + Vector3Int.down).name != "NonPath") {
					Transform x = Instantiate(highLight, grid.WorldToCell(enemy.transform.position) + Vector3Int.down, transform.rotation);
					x.GetComponent<SpriteRenderer>().color = Color.cyan;
					x.parent = dad;
				}
				if(tilemap.GetTile(grid.WorldToCell(enemy.transform.position) + Vector3Int.left).name != "NonPath") {
					Transform x = Instantiate(highLight, grid.WorldToCell(enemy.transform.position) + Vector3Int.left, transform.rotation);
					x.GetComponent<SpriteRenderer>().color = Color.cyan;
					x.parent = dad;
				}
				if(tilemap.GetTile(grid.WorldToCell(enemy.transform.position) + Vector3Int.right).name != "NonPath") {
					Transform x = Instantiate(highLight, grid.WorldToCell(enemy.transform.position) + Vector3Int.right, transform.rotation);
					x.GetComponent<SpriteRenderer>().color = Color.cyan;
					x.parent = dad;
				}
			} 
		}
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
				Stun.Play();
				abilityCharges--;
				foreach (Transform child in dad.transform) {
         			Destroy(child.gameObject);
        		}				
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
		foreach (Transform child in dad.transform) {
        	Destroy(child.gameObject);
        }
		foreach(GameObject enemy in TurnManager.enemies) {
			if(grid.WorldToCell(enemy.transform.position) + Vector3Int.up == grid.WorldToCell(transform.position)) {
				Transform x = Instantiate(highLight, grid.WorldToCell(enemy.transform.position), transform.rotation);
				x.GetComponent<SpriteRenderer>().color = Color.cyan;
				x.parent = dad;
			}else if(grid.WorldToCell(enemy.transform.position) + Vector3Int.down == grid.WorldToCell(transform.position)) {
				Transform x = Instantiate(highLight, grid.WorldToCell(enemy.transform.position), transform.rotation);
				x.GetComponent<SpriteRenderer>().color = Color.cyan;
				x.parent = dad;
			} else if(grid.WorldToCell(enemy.transform.position) + Vector3Int.left == grid.WorldToCell(transform.position)) {
				Transform x = Instantiate(highLight, grid.WorldToCell(enemy.transform.position), transform.rotation);
				x.GetComponent<SpriteRenderer>().color = Color.cyan;
				x.parent = dad;
			} else if(grid.WorldToCell(enemy.transform.position) + Vector3Int.right == grid.WorldToCell(transform.position)) {
				Transform x = Instantiate(highLight, grid.WorldToCell(enemy.transform.position), transform.rotation);
				x.GetComponent<SpriteRenderer>().color = Color.cyan;
				x.parent = dad;
			}
		}
		abilityButtons.SetActive(false);
		cancelButton.SetActive(true);
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
					
				}
			}

			// Check if the tile isn't a path.
			if(tilemap.GetTile(coordinate).name != "NonPath" && enemyThere){

				//Check to make sure the player doesnt move diagonal.
				if((upAndDown && leftAndRight2) || (leftAndRight && upAndDown2)){
					Vector3 newPosition = (Vector3)coordinate;

                    foreach (GameObject enemies in TurnManager.enemies)
                    {
                        Vector3 checkPos = (Vector3)coordinate;
                        checkPos.x += 0.5f;
                        checkPos.y += 0.5f;
                        if (enemies.transform.position == checkPos)
                        {
                            enemyThere = true;
                            if (enemies.GetComponent<Scout>() != null)
                            {
                                enemies.GetComponent<Scout>().isStunned = true;
                                enemies.GetComponent<Scout>().stunLeft = 3;
                            }
                            else if (enemies.GetComponent<Ranger>() != null)
                            {
                                enemies.GetComponent<Ranger>().isStunned = true;
                                enemies.GetComponent<Ranger>().stunLeft = 3;
                            }
                            else if (enemies.GetComponent<Bezerker>() != null)
                            {
                                enemies.GetComponent<Bezerker>().isStunned = true;
                                enemies.GetComponent<Bezerker>().stunLeft = 3;
                                enemies.GetComponent<Bezerker>().tillCharge = 0;
                                enemies.GetComponent<Bezerker>().enraged = false;
                            }
                            else if (enemies.GetComponent<Warrior>() != null)
                            {
                                enemies.GetComponent<Warrior>().isStunned = true;
                                enemies.GetComponent<Warrior>().stunLeft = 3;
                            }
                        }
                    }

                    // Shift the players character to the center of the tile.
                    newPosition.x += 0.5f;
					newPosition.y += 0.5f;
					Instantiate(FlourPoof, newPosition, Quaternion.identity);
					Stun.Play();
					this.transform.position = newPosition;
					abilityCharges--;
					stunActive = false;
					foreach (Transform child in dad.transform) {
         				Destroy(child.gameObject);
        			}	
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
		foreach (Transform child in dad.transform) {
        	Destroy(child.gameObject);
        }
		GetComponent<testingTileHighlights>().playerMoveTiles();
		GetComponent<testingTileHighlights>().colorTiles();
		cancelButton.SetActive(false);
	}
}
