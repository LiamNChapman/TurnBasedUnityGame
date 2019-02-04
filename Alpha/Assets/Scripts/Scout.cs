using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Scout : MonoBehaviour {
	public Tilemap tilemap;
	public Grid grid;
	public int facing; //1 = left, 2 = down, 3 = right, 4 = up
	bool moving = false;
	public Transform cross;
	public Sprite[] spriteList;
	public bool isStunned = false;
	public int stunLeft = 0;

	float speed = 2.0f;
	
	Vector3 nextPos;
	Vector3 destination;

	// Use this for initialization
	void Start () {
		nextPos = this.transform.position;
		destination = nextPos;
		Transform x;
		if(facing == 1){
			destination += Vector3.left;
			x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
		} else if(facing == 2){
			destination += Vector3.down;
			x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
		} else if(facing == 3){
			destination += Vector3.right;
			x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
		} else {
			destination += Vector3.up;
			x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
		}
		x.parent = transform;
		this.GetComponent<SpriteRenderer>().sprite = spriteList[facing-1];
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isStunned){
			if(!moving){
				foward();
			}
			move();
		} else {
			stunLeft--;
			if(stunLeft < 1){
				isStunned = false;
			}
			TurnManager.enemyMoves--;
			this.enabled = false;
		}
	}

	void foward(){
		foreach (Transform child in transform) {
    		Destroy(child.gameObject);
    	}
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
		this.GetComponent<SpriteRenderer>().sprite = spriteList[facing-1];
		moving = true;
	}

	void move() {
		float step = speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards(transform.position, nextPos, step);
		
		if(this.transform.position == destination){
			if(destination == TurnManager.player.transform.position){
				TurnManager.killed();
			}
			Transform x;
			if(facing == 1){				
				destination += Vector3.left;
				if(((Tile)tilemap.GetTile(grid.WorldToCell(destination))).name == "NonPath") {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(this.transform.position + Vector3.right), transform.rotation);
				} else {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
				}			
			} else if(facing == 2){					
				destination += Vector3.down;
				if(((Tile)tilemap.GetTile(grid.WorldToCell(destination))).name == "NonPath") {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(this.transform.position + Vector3.up), transform.rotation);
				} else {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
				}
			} else if(facing == 3){
				destination += Vector3.right;
				if(((Tile)tilemap.GetTile(grid.WorldToCell(destination))).name == "NonPath") {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(this.transform.position + Vector3.left), transform.rotation);
				} else {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
				}
			} else {
				destination += Vector3.up;
				if(((Tile)tilemap.GetTile(grid.WorldToCell(destination))).name == "NonPath") {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(this.transform.position + Vector3.down), transform.rotation);
				} else {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
				}	
			}
			x.parent = transform;
			moving = false;
			TurnManager.enemyMoves--;
			this.enabled = false;
		}
	}
}
