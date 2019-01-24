/*
	Created by Joe Marshall
	Jan 23rd, 2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : MonoBehaviour {

	//This is going to be consistent with the current prototype even though we have a better iteration.

	public bool vertical = true;

	// Depending on the 'vertical' state, 1 translates to either UP, or RIGHT.
	public int direction = 1;

	// Only move if it is the enemy turn state
	void Update () {
		//if(LevelManager.currentState == LevelManager.TurnStates.ENEMYMOVE){
		Move();
		TurnManager.nextState();
		//}
	}

	public void Move(){
		//Scout movement can be set using the 'vertical' boolean 
		if(vertical){
			//If the sprite is moving upwards
			if(direction == 1){
				this.transform.position += Vector3.up;
			}else{
				this.transform.position += Vector3.down;
			}
		}else{
			//If the sprite is moving ot the right
			if(direction == 1){
				this.transform.position += Vector3.right;
			}else{
				this.transform.position += Vector3.left;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(vertical){
			//If the Sprite exited at the top of a path
			if(direction == 1){
				//Move it back onto the path
				this.transform.position += Vector3.down;
				//Set it to move downwards
				direction = 0;
				//Move down one space
				this.Move();
			}else{
				this.transform.position += Vector3.up;
				direction = 1;
				this.Move();
			}
		}else{
			//If the sprite exited on the right of a path
			if(direction  == 1){
				//Move back onto path
				this.transform.position += Vector3.left;
				//Set to move left
				direction = 0;
				//Move
				this.Move();
			}else{
				this.transform.position += Vector3.right;
				direction = 1;
				this.Move();
			}
		}
	}
}
