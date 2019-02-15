/*
	Created by Joe Marshall
	Jan 25th, 2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class WarriorPlayer : MonoBehaviour {

	public bool exitPath = false;

	public int prev;
	
	//Update is called once per frame
	// This prototype isn't using the turn manager as Warriors don't move and we haven't implemented abilities
	
	void FixedUpdate () {
		if(Input.GetKeyDown(KeyCode.A)){
			transform.position += Vector3.left;
			prev = 1;
		}else if(Input.GetKeyDown(KeyCode.D)){
			transform.position += Vector3.right;
			prev = 2;
		}else if(Input.GetKeyDown(KeyCode.W)){
			transform.position += Vector3.up;
			prev = 3;
		}else if(Input.GetKeyDown(KeyCode.S)){
			transform.position += Vector3.down;
			prev = 4;
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
            SceneManager.LoadScene("WarriorPrototype");
        }
    }

}

