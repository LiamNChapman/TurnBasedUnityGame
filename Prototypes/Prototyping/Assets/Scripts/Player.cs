/*
	Created by Joe Marshall
	Jan 22nd, 2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public bool exitPath = false;

	public int prev;
	
	//Update is called once per frame
	
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

		//If the player leaves the path, determine where they were and put them back
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

	// When player collides with an object that is not a trigger 
    void OnCollisionEnter2D(Collision2D other) {
        // If the player has collided with an enemy
        if (other.gameObject.tag == "Enemy") {
            // Reload the prototype
            SceneManager.LoadScene("GridMovement");
        }
    }

}
