﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public static GameObject player;
	public static GameObject[] enemies;
	public static int enemyMoves;
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
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		enemyMoves = enemies.Length;
	}
	
	//Switch move states if the object in question is in a different space than they were?
	// Update is called once per frame
	void Update () {
		Debug.Log(currentState + " " + turnCount + " " + enemyMoves);
		if(enemyMoves <= 0) {
			enemyMoves = enemies.Length;
			nextState();
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
		for(int i = 0; i < enemies.Length;i++){
			if(enemies[i].GetComponent<Scout>() != null) {
				enemies[i].GetComponent<Scout>().enabled = false;
			} else if(enemies[i].GetComponent<Ranger>() != null) {
				enemies[i].GetComponent<Ranger>().enabled = false;
			} else if(enemies[i].GetComponent<Bezerker>() != null) {
				enemies[i].GetComponent<Bezerker>().enabled = false;
			} else if(enemies[i].GetComponent<Warrior>() != null) {
				enemies[i].GetComponent<Warrior>().enabled = false;
			}
		}
		player.GetComponent<Player>().enabled = true;
	}
	static void enemyMoveSetup() {
		for(int i = 0; i < enemies.Length;i++){
			if(enemies[i].GetComponent<Scout>() !=  != null) {
				enemies[i].GetComponent<Scout>().enabled = true;
			} else if(enemies[i].GetComponent<Ranger>() != null) {
				enemies[i].GetComponent<Ranger>().enabled = true;
			} else if(enemies[i].GetComponent<Bezerker>() != null) {
				enemies[i].GetComponent<Bezerker>().enabled = true;
			} else if(enemies[i].GetComponent<Warrior>() != null) {
				enemies[i].GetComponent<Warrior>().enabled = true;
			}
		}
		player.GetComponent<Player>().enabled = false;
	}

	public TurnStates getCurrentState() {
		return currentState;
	}

	
}
