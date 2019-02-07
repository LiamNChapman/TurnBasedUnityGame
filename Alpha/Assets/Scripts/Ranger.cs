using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Ranger : MonoBehaviour {

	List<Vector3Int> list = new List<Vector3Int>();
	public Tilemap tilemap;
	public Grid grid;
	public int facing = 1;
	public Transform cross;
	public Transform arrow;
	public Sprite[] spriteList;
	Transform instanceArrow;
	Vector3Int[] hitBoxes = new Vector3Int[4];

	public bool gotDistracted = false;
	public bool isStunned = false;
	public int stunLeft = 0;

	bool needArrow = true;
	bool attacking = false;
	float speed = 20.0f;

	void Start() {
		Vector3 initialPos = this.transform.position;
		Vector3Int pos = grid.WorldToCell(initialPos);
		Tile tile = (Tile)tilemap.GetTile(pos);
		hitBoxes[0] = grid.WorldToCell(initialPos + Vector3.left);
		hitBoxes[1] = grid.WorldToCell(initialPos + Vector3.down);
		hitBoxes[2] = grid.WorldToCell(initialPos + Vector3.right);
		hitBoxes[3] = grid.WorldToCell(initialPos + Vector3.up);
		while(tile.name != "NonPath") {			
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
				Transform x = Instantiate(cross, pos, transform.rotation);
				x.parent = transform;
			}
		}
		
		this.GetComponent<SpriteRenderer>().sprite = spriteList[facing-1];
		this.enabled = false;

	}

	void Update(){
		if(!isStunned){
			rotate();
			if(!attacking){
				TurnManager.enemyMoves--;
				this.enabled = false;
			}
		} else {
			if(gotDistracted){
				rotate();
				gotDistracted = false;
			}
			stunLeft--;
			if(stunLeft < 1){
				isStunned = false;
			}
			TurnManager.enemyMoves--;
			this.enabled = false;
		}
	}
	void rotate() {
		if(!gotDistracted){
			if(attacking){
				if(needArrow){
						if(facing == 1){
							instanceArrow = Instantiate(arrow, transform.position, Quaternion.Euler(0,0,0));
						} else if(facing == 2){
							instanceArrow = Instantiate(arrow, transform.position, Quaternion.Euler(0,0,270));
						} else if(facing == 3){
							instanceArrow = Instantiate(arrow, transform.position, Quaternion.Euler(0,0,180));
						} else if(facing == 4){
							instanceArrow = Instantiate(arrow, transform.position, Quaternion.Euler(0,0,90));
						}
						needArrow = false;
					}
				float step = speed * Time.deltaTime;
				instanceArrow.transform.position = Vector3.MoveTowards(instanceArrow.transform.position, TurnManager.player.transform.position, step);
				if(instanceArrow.transform.position == TurnManager.player.transform.position){
					attacking = false;
					TurnManager.killed();
				}
				return;
			}
			foreach(Vector3 los in list) {
				Vector3 hit = los;
				hit.x += 0.5f;
				hit.y += 0.5f;
				if(TurnManager.player.transform.position == hit) {
					attacking = true;
					foreach (Transform child in transform) {
         		   		Destroy(child.gameObject);
        			}
					return;
				}
			}
			if(facing == 4){
				facing = 0;
			}
			facing++;
		}
		list = new List<Vector3Int>();
		Vector3 initialPos = this.transform.position;
		Vector3Int pos = grid.WorldToCell(initialPos);
		Tile tile = (Tile)tilemap.GetTile(pos);
		foreach (Transform child in transform) {
             Destroy(child.gameObject);
        }
		while(((Tile)tilemap.GetTile(hitBoxes[facing-1])).name == "NonPath") {
			facing++;
			if(facing == 5){
				facing = 1;
			}
		}

		this.GetComponent<SpriteRenderer>().sprite = spriteList[facing-1];
		
		

		while(tile.name != "NonPath") {			
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
				Transform x = Instantiate(cross, pos, transform.rotation);
				x.parent = transform;
			}
		}
	}
}
