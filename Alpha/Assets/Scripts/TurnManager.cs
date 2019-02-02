using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour {

	public static GameObject player;
	public static GameObject[] enemies;
	public static int enemyMoves;
	public static PressurePad pressurePad;
	//Made this public so the enemy scripts could access
	public enum TurnStates {
		PLAYERMOVE,
		ENEMYMOVE

	}
	public static int turnCount = 0;
	public static TurnStates currentState;
	// Use this for initialization
	void Start () {
		currentState = TurnStates.PLAYERMOVE;
		player = GameObject.Find("Player");
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		if(GameObject.Find("PressurePad") != null) {
			pressurePad = GameObject.Find("PressurePad").GetComponent<PressurePad>();
		}
		enemyMoves = enemies.Length;
	}
	
	//Switch move states if the object in question is in a different space than they were?
	// Update is called once per frame
	void Update () {
		if(enemyMoves <= 0) {
			enemyMoves = enemies.Length;
			nextState();
		}
		if(pressurePad != null){
			pressurePad.checkPad();
		}
	}
	public static void nextState() {
		if(currentState == TurnStates.PLAYERMOVE) {
			
			currentState = TurnStates.ENEMYMOVE;
			enemyMoveSetup();
		} else {
			currentState = TurnStates.PLAYERMOVE;
			playerMoveSetup();
			turnCount++;
		}
		
	}

	static void playerMoveSetup() {
		player.GetComponent<Player>().moved = false;
		player.GetComponent<Player>().enabled = true;
	}
	static void enemyMoveSetup() {
		for(int i = 0; i < enemies.Length;i++){
			if(enemies[i].GetComponent<Scout>() != null) {
				enemies[i].GetComponent<Scout>().enabled = true;
			} else if(enemies[i].GetComponent<Ranger>() != null) {
				enemies[i].GetComponent<Ranger>().enabled = true;
			} else if(enemies[i].GetComponent<Bezerker>() != null) {
				enemies[i].GetComponent<Bezerker>().enabled = true;
			} else if(enemies[i].GetComponent<Warrior>() != null) {
				enemies[i].GetComponent<Warrior>().turn = false;
			}
		}
		player.GetComponent<Player>().enabled = false;
	}

	public TurnStates getCurrentState() {
		return currentState;
	}
	public static void killed() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
