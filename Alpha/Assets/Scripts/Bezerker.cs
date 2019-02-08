using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bezerker : MonoBehaviour {

	List<Vector3Int> list = new List<Vector3Int>();
	public Tilemap tilemap;
	public Grid grid;
	public int facing; //1 = left, 2 = down, 3 = right, 4 = up
	public Transform cross;
	public Sprite[] spriteList;
	public bool isStunned = false;
	public int stunLeft = 0;

	bool enraged = false;
	int tillCharge = 0;
	bool charging = false;
	public float speed = 10.0f;

	Vector3 destination;
	int chargeDelay = 0;

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer>().sprite = spriteList[facing-1];
		Vector3 initialPos = this.transform.position;
		Vector3Int pos = grid.WorldToCell(initialPos);
		Tile tile = (Tile)tilemap.GetTile(pos);
		while(tile.name != "NonPath"){
			
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
			if(tile.name != "NonPath"){
				list.Add(pos);
			}
		}
		destination = (Vector3)list[list.Count-1];
		destination.x += 0.5f;
		destination.y += 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isStunned){
			this.GetComponent<SpriteRenderer>().sprite = spriteList[facing-1];
			if(chargeDelay != 1){
				if(!enraged){
					checkLOS();
				}

				if(enraged && tillCharge == 0){	
				attack();
				}
				if(transform.position == destination){
					endTurn();
				}
		
				if(!enraged || tillCharge == 1){
					tillCharge = 0;
					TurnManager.enemyMoves--;
					this.enabled = false;
				}
			} else {
				chargeDelay = 0;
				TurnManager.enemyMoves--;
				charging = false;
				this.enabled = false;
			}
		} else {
			stunLeft--;
			if(stunLeft < 1){
				isStunned = false;
			}		
			TurnManager.enemyMoves--;
			this.enabled = false;
		}
	}

	void checkLOS(){
		list = new List<Vector3Int>();
		Vector3 initialPos = this.transform.position;
		Vector3Int pos = grid.WorldToCell(initialPos);
		Tile tile = (Tile)tilemap.GetTile(pos);
		while(tile.name != "NonPath"){
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
			if(tile.name != "NonPath"){
				list.Add(pos);
			}
			destination = (list[list.Count-1]);
			destination.x += 0.5f;
			destination.y += 0.5f;
		}
		for(int i = 0; i < list.Count; i++){
			Vector3 charge = list[i];
			charge.x += 0.5f;
			charge.y += 0.5f;
			if(TurnManager.player.transform.position == charge){
				TurnManager.killTiles.Add(grid.WorldToCell(list[i]));
				if(i - 1 >= 0 && tilemap.GetTile(grid.WorldToCell(list[i-1])).name != "NonPath") {
					TurnManager.killTiles.Add(grid.WorldToCell(list[i-1]));
				}
				if(i + 1 <= list.Count - 1 && tilemap.GetTile(grid.WorldToCell(list[i+1])).name != "NonPath") {
					TurnManager.killTiles.Add(grid.WorldToCell(list[i+1]));
				}
				enraged = true;
				tillCharge = 1;
				for(int j = 0; j < list.Count; j++) {
					Transform x = Instantiate(cross, (Vector3)list[j], transform.rotation);
					x.parent = transform;
				}
			}
		}
	}

	void attack(){
			if(!charging) {
				charging = true;
				foreach (Transform child in transform) {
             		Destroy(child.gameObject);
        		}			
			}
			Vector3 end = list[list.Count-1];
			end.x += 0.5f;
			end.y += 0.5f;
			
			
			float step = speed * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards(transform.position, end, step);
			
			if(transform.position.x <= (TurnManager.player.transform.position.x+0.5f)&& transform.position.x >= (TurnManager.player.transform.position.x-0.5f)){
				if(transform.position.y <= (TurnManager.player.transform.position.y+0.5f)&& transform.position.y >= (TurnManager.player.transform.position.y-0.5f)){
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
				if(tile.name != "NonPath"){
					list.Add(pos);
				}
			}
			destination = (list[list.Count-1]);
			destination.x += 0.5f;
			destination.y += 0.5f;
			chargeDelay = 1;
		TurnManager.enemyMoves--;
	}
}
