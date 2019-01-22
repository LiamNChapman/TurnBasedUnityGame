using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public GameObject player;
	public enum TurnStates {
		PLAYER,
		PLAYERMOVE,
		ENEMYMOVE

	}
	int turnCount = 0;
	TurnStates currentState;
	// Use this for initialization
	void Start () {
		currentState = TurnStates.PLAYER;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(currentState);
		switch(currentState) {
			case(TurnStates.PLAYER):
				break;
			case(TurnStates.PLAYERMOVE):
				nextState();
				break;
			case(TurnStates.ENEMYMOVE):
				nextState();
				break;
		}
	}
	public void nextState() {
		if(currentState == TurnStates.PLAYER) {
			currentState = TurnStates.PLAYERMOVE;
			playerMoveSetup();
		} else if(currentState == TurnStates.PLAYERMOVE) {
			currentState = TurnStates.ENEMYMOVE;
		} else {
			currentState = TurnStates.PLAYER;
			playerTurnSetup();
		}
		turnCount++;
	}

	void playerTurnSetup() {
		player.GetComponent<Player>().enabled = true;
	}
	void playerMoveSetup() {
		player.GetComponent<Player>().enabled = false;
	}
}
