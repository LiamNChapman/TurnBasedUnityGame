/*
	Created by Liam Chapman
	23rd Jan 2019
	Player movement by clicking, only one tile at a time.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System;

public class ClickToMoveAbilities : MonoBehaviour {

	public Grid grid;
	public Tilemap tilemap;
	public bool canMove = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		
		if(canMove && Input.GetMouseButtonDown(0)) {
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
			if(tilemap.GetTile(coordinate).name != "NonPath" && canMove){

				//Check to make sure the player doesnt move diagonal.
				if((upAndDown && leftAndRight2) || (leftAndRight && upAndDown2)){
					Vector3 newPosition = (Vector3)coordinate;

					// Shift the players character to the center of the tile.
					newPosition.x += 0.5f;
					newPosition.y += 0.5f;
					this.transform.position = newPosition;
					canMove = false;
				}
			}
		}
		if(!canMove){
			if(Input.GetKeyDown(KeyCode.S)){
				gameObject.GetComponent<Stun>().inUse = true;
				Debug.Log("Stun Active");
			} else if(Input.GetKeyDown(KeyCode.D)){
				gameObject.GetComponent<Distraction>().inUse = true;
			}else if(Input.GetKeyDown(KeyCode.Return)){
				TurnManagerAbilities.nextState();
				canMove = true;
			}
		}
	}

	// When player collides with an object that is not a trigger 
    void OnCollisionEnter2D(Collision2D other) {
        // If the player has collided with an enemy
        if (other.gameObject.tag == "Enemy") {
			if(gameObject.GetComponent<Stun>().inUse){
				gameObject.GetComponent<ScoutStunned>().isStunned = true;
				gameObject.GetComponent<ScoutStunned>().stunLeft = 3;
				gameObject.GetComponent<BoxCollider2D>().enabled = false;
				gameObject.GetComponent<Stun>().inUse = false;
			}else{
            // Reload the prototype
            	SceneManager.LoadScene("PlayerAbilities");
			}
        }
    }
}