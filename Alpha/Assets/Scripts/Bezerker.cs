using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bezerker : MonoBehaviour {

	List<Vector3Int> list = new List<Vector3Int>();
	public Tilemap tilemap;
	public Grid grid;
	public int facing; //1 = left, 2 = down, 3 = right, 4 = up

	bool enraged = false;
	int tillCharge = 0;

	float speed = 10.0f;

	Vector3 destination;
	int chargeDelay = 0;

	// Use this for initialization
	void Start () {
		Vector3 initialPos = this.transform.position;
		Vector3Int pos = grid.WorldToCell(initialPos);
		Tile tile = (Tile)tilemap.GetTile(pos);
		while(tile.name != "NonPath"){
			list.Add(pos);
			if(facing == 1){
				initialPos += Vector3.left;
			} else if(facing == 2){
				initialPos += Vector3.down;
			} else if(facing == 3){
				initialPos += Vector3.right;
			} else if(facing == 4){
				initialPos += Vector3.up;
			}
			pos = grid.WorldToCell(initialPos);
			tile = (Tile)tilemap.GetTile(pos);
		}
		destination = (Vector3)list[list.Count-1];
		destination.x += 0.5f;
		destination.y += 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if(chargeDelay != 1){
			if(!enraged){
				checkLOS();
			}
			if(enraged && tillCharge == 0){
				attack();
			}
			if(transform.position == destination){
				Debug.Log("weeeeeeeee");
				endTurn();
			}
		
			if(!enraged || tillCharge == 1){
				tillCharge = 0;
				TurnManager.enemyMoves--;
			}
		} else {
			chargeDelay = 0;
			TurnManager.enemyMoves--;
		}
	}

	void checkLOS(){
		foreach(Vector3 los in list){
			Vector3 charge = los;
			charge.x += 0.5f;
			charge.y += 0.5f;
			if(TurnManager.player.transform.position == charge){
				Debug.Log("IM GONNA CHAAARGE!!");
				enraged = true;
				tillCharge = 1;
			}
		}
	}

	void attack(){
			Vector3 end = list[list.Count-1];
			end.x += 0.5f;
			end.y += 0.5f;
			
			
			float step = speed * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards(transform.position, end, step);
			
			if(transform.position.x <= (TurnManager.player.transform.position.x+0.5f)&& transform.position.x >= (TurnManager.player.transform.position.x-0.5f)){
				if(transform.position.y <= (TurnManager.player.transform.position.y+0.5f)&& transform.position.y >= (TurnManager.player.transform.position.y-0.5f)){
					Debug.Log("Ded");
					TurnManager.killed();
				}
			}
			if(transform.position == destination){
				facing = (facing + 2)%4;
				if(facing == 0){
					facing = 4;
				}
				enraged = false;
			}
	}
	void endTurn(){
		list = new List<Vector3Int>();
			Vector3 initialPos = this.transform.position;
			Vector3Int pos = grid.WorldToCell(initialPos);
			Tile tile = (Tile)tilemap.GetTile(pos);
			while(tile.name != "NonPath"){
				list.Add(pos);
				if(facing == 1){
					initialPos += Vector3.left;
				} else if(facing == 2){
					initialPos += Vector3.down;
				} else if(facing == 3){
					initialPos += Vector3.right;
				} else if(facing == 4){
					initialPos += Vector3.up;
				}
				pos = grid.WorldToCell(initialPos);
				tile = (Tile)tilemap.GetTile(pos);
			}
			destination = (list[list.Count-1]);
			destination.x += 0.5f;
			destination.y += 0.5f;
			chargeDelay = 1;
		TurnManager.enemyMoves--;
	}
}
