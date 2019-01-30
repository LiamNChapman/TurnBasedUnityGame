﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Scout : MonoBehaviour {
	public Tilemap tilemap;
	public Grid grid;
	public int facing; //1 = left, 2 = down, 3 = right, 4 = up
	bool moving = false;

	float speed = 2.0f;
	
	Vector3 nextPos;
	Vector3 destination;

	// Use this for initialization
	void Start () {
		nextPos = this.transform.position;
		destination = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!moving){
			foward();
		}

		float step = speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards(transform.position, nextPos, step);
		
		if(this.transform.position == destination){
			if(destination == TurnManager.player.transform.position){
				TurnManager.killed();
			}
			moving = false;
			TurnManager.enemyMoves--;
		}
	}

	void foward(){
		Vector3Int pos;
		Tile tile;

		if(facing == 1){
			nextPos += Vector3.left;
		} else if(facing == 2){
			nextPos += Vector3.down;
		} else if(facing == 3){
			nextPos += Vector3.right;
		} else if(facing == 4){
			nextPos += Vector3.up;
		}

		pos = grid.WorldToCell(nextPos);
		tile = (Tile)tilemap.GetTile(pos);

		if(tile.name == "NonPath"){
			facing = (facing + 2)%4;
			if(facing == 0){
				facing = 4;
			}
		
			nextPos = this.transform.position;

			if(facing == 1){
				nextPos += Vector3.left;
			} else if(facing == 2){
				nextPos += Vector3.down;
			} else if(facing == 3){
				nextPos += Vector3.right;
			} else if(facing == 4){
				nextPos += Vector3.up;
			}
		}
		destination = nextPos;
		//destination.x += 0.5f;
		//destination.y += 0.5f;
		Debug.Log(destination);
		moving = true;
	}
}
