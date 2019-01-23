/*
	Created by Joe Marshall
	Jan 22nd, 2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour {

	public bool exitPath = false;

	public int prev;
	
	//Update is called once per frame
	//If one of the four directional buttons pushed, determine which one, set the movePosition,
	// and set move to true so that the fixedUpdate method will move the player.
	void FixedUpdate () {
		if(Input.GetKeyDown(KeyCode.A)){
			transform.position += Vector3.left;
			prev = 1;

			//We may want to only change state if we're sure the player is in a different place 
			//As in it depends on how we handle collsions with the path collider
			LevelManager.nextState();
		}else if(Input.GetKeyDown(KeyCode.D)){
			transform.position += Vector3.right;
			prev = 2;
			LevelManager.nextState();
		}else if(Input.GetKeyDown(KeyCode.W)){
			transform.position += Vector3.up;
			prev = 3;
			LevelManager.nextState();
		}else if(Input.GetKeyDown(KeyCode.S)){
			transform.position += Vector3.down;
			prev = 4;
			LevelManager.nextState();
		}

		if(exitPath){
			if(prev == 1){
				transform.position += Vector3.right;
			}else if(prev == 2){
				transform.position += Vector3.left;
			}else if(prev == 3){
				transform.position += Vector3.down;
			}else if(prev == 4){
				transform.position += Vector3.up;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		exitPath = true;
	}

	void OnTriggerExit2D(Collider2D other){
		exitPath = false;
	}

}
