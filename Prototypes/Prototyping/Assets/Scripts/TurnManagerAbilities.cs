using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagerAbilities : MonoBehaviour {
	public static GameObject player;
	public static GameObject enemy;
	
	//Made this public so the enemy scripts could access
	public enum TurnStates {
		PLAYERMOVE,
		ENEMYMOVE

	}
	static int turnCount = 0;
	public static TurnStates currentState;
	// Use this for initialization
	void Start () {
		currentState = TurnStates.PLAYERMOVE;
		player = GameObject.Find("Player");
		enemy = GameObject.Find("Scout");
	}
	
	//Switch move states if the object in question is in a different space than they were?
	// Update is called once per frame
	void Update () {
		//Debug.Log(currentState + " " + turnCount);
		//if(currentState == TurnStates.ENEMYMOVE) {
			//nextState();
		//}
		
	}
	public static void nextState() {
		if(currentState == TurnStates.PLAYERMOVE) {
			currentState = TurnStates.ENEMYMOVE;
			enemyMoveSetup();
		} else {
			currentState = TurnStates.PLAYERMOVE;
			playerMoveSetup();
		}
		turnCount++;
	}

	static void playerMoveSetup() {
		player.GetComponent<ClickToMoveAbilities>().enabled = true;
		enemy.GetComponent<ScoutStunned>().enabled = false;
	}
	static void enemyMoveSetup() {
		player.GetComponent<ClickToMoveAbilities>().enabled = false;
		enemy.GetComponent<ScoutStunned>().enabled = true;
	}

	public TurnStates getCurrentState() {
		return currentState;
	}
}
