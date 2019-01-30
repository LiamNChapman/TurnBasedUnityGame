﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Warrior : MonoBehaviour {
	public int facing;
	int initialLOS;
	Vector3Int[] hitBoxes = new Vector3Int[4];
	public Grid grid;
	public Tilemap tilemap;
	public bool turn = false;
	public Transform cross;

	public bool isStunned = false;
	public int stunLeft = 0;
	// Use this for initialization
	void Start () {
		Vector3 initialPos = this.transform.position;
		hitBoxes[0] = grid.WorldToCell(initialPos + Vector3.left);
		hitBoxes[1] = grid.WorldToCell(initialPos + Vector3.down);
		hitBoxes[2] = grid.WorldToCell(initialPos + Vector3.right);
		hitBoxes[3] = grid.WorldToCell(initialPos + Vector3.up);
		Transform x = Instantiate(cross, (Vector3)hitBoxes[facing-1], transform.rotation);
		x.parent = transform;
		initialLOS = facing;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isStunned){
			Vector3Int playerPos = grid.WorldToCell(TurnManager.player.transform.position);
			if(playerPos == hitBoxes[facing-1]) {
				Debug.Log("ded");
				TurnManager.killed();
			}
			if(!turn) {
				TurnManager.enemyMoves--;
				turn = true;
			}
			if(facing != initialLOS) {
				foreach (Transform child in transform) {
    	         		Destroy(child.gameObject);
    	    	}
				Transform x = Instantiate(cross, (Vector3)hitBoxes[facing-1], transform.rotation);
				x.parent = transform;
				initialLOS = facing;
			}
		} else {
			
			if(!turn) {
				stunLeft--;
				if(stunLeft < 1){
					isStunned = false;
				}
				TurnManager.enemyMoves--;
				turn = true;
			}
		}
	}
}