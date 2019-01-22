/*
	Created by Joe Marshall
	Jan 22nd, 2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int prev;
	public float speed = 1.0f;
	
	//Update is called once per frame
	//If one of the four directional buttons pushed, determine which one, set the movePosition,
	// and set move to true so that the fixedUpdate method will move the player.
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			transform.position += Vector3.left;
			prev = 1;
		}
		
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			transform.position += Vector3.right ;
			prev = 2;
		}
		
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			transform.position += Vector3.up;
			prev = 3;
		}
		
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			transform.position += Vector3.down;
			prev = 4;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Path"){
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

}
