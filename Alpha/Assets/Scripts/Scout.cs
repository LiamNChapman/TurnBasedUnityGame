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
	bool deleteLOS;
	public GameObject Stun;
	GameObject StunInstance;

	float speed = 2.0f;
	
	Vector3 nextPos;
	Vector3 destination;

	public Sprite[] spriteListLeft;
	public Sprite[] spriteListDown;
	public Sprite[] spriteListUp;
	public Sprite[] spriteListRight;
	int spriteCounter = 0;
	int animationDelay = 0;

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
		foreach(Transform child in transform){
			child.GetComponent<SpriteRenderer>().color = new Color(255, 215, 0, 1);
		}
		TurnManager.killTiles.Add(grid.WorldToCell(destination));
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
			if(stunLeft == 3){
				StunInstance = Instantiate(Stun, transform.position, Quaternion.identity);
			}
			stunLeft--;
			if(deleteLOS == false) {
				foreach (Transform child in transform) {
             		Destroy(child.gameObject);
        		}
				deleteLOS = true;
			}
			if(stunLeft < 1){
				Destroy(StunInstance);
				isStunned = false;
				Transform x;
				if(facing == 1){
					if(((Tile)tilemap.GetTile(grid.WorldToCell(destination))).name == "NonPath") {
						x = Instantiate(cross, (Vector3)grid.WorldToCell(this.transform.position + Vector3.right), transform.rotation);
						TurnManager.killTiles.Add(grid.WorldToCell(this.transform.position + Vector3.right));
					} else {
						x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
						TurnManager.killTiles.Add(grid.WorldToCell(destination));
					}			
				} else if(facing == 2){
					if(((Tile)tilemap.GetTile(grid.WorldToCell(destination))).name == "NonPath") {
						x = Instantiate(cross, (Vector3)grid.WorldToCell(this.transform.position + Vector3.up), transform.rotation);
						TurnManager.killTiles.Add(grid.WorldToCell(this.transform.position + Vector3.up));
					} else {
						x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
						TurnManager.killTiles.Add(grid.WorldToCell(destination));
					}
				} else if(facing == 3){
					if(((Tile)tilemap.GetTile(grid.WorldToCell(destination))).name == "NonPath") {
						x = Instantiate(cross, (Vector3)grid.WorldToCell(this.transform.position + Vector3.left), transform.rotation);
						TurnManager.killTiles.Add(grid.WorldToCell(this.transform.position + Vector3.left));
					} else {
						x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
						TurnManager.killTiles.Add(grid.WorldToCell(destination));
					}
				} else {
					if(((Tile)tilemap.GetTile(grid.WorldToCell(destination))).name == "NonPath") {
						x = Instantiate(cross, (Vector3)grid.WorldToCell(this.transform.position + Vector3.down), transform.rotation);
						TurnManager.killTiles.Add(grid.WorldToCell(this.transform.position + Vector3.down));
					} else {
						x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
						TurnManager.killTiles.Add(grid.WorldToCell(destination));
					}	
				}
				x.parent = transform;
				foreach(Transform child in transform){
					child.GetComponent<SpriteRenderer>().color = new Color(255, 215, 0, 1);
				}
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

		GameObject gate;
		if(GameObject.Find("Gate") != null){
			gate = GameObject.Find("Gate");
			if(pos == gate.transform.position){
				facing = (facing + 2)%4;
				if(facing == 0){
					facing = 4;
				}
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

	if(animationDelay == 10){
		animationDelay = 0;
	}

	if(animationDelay == 0){
		if(facing == 1){
			GetComponent<SpriteRenderer>().sprite = spriteListLeft[spriteCounter];
			spriteCounter++;
			if(spriteCounter > 3){
				spriteCounter = 0;
			}
		}
		if(facing == 2){
			GetComponent<SpriteRenderer>().sprite = spriteListDown[spriteCounter];
			spriteCounter++;
			if(spriteCounter > 3){
				spriteCounter = 0;
			}
		}
		if(facing == 3){
			GetComponent<SpriteRenderer>().sprite = spriteListRight[spriteCounter];
			spriteCounter++;
			if(spriteCounter > 3){
				spriteCounter = 0;
			}
		}
		if(facing == 4){
			GetComponent<SpriteRenderer>().sprite = spriteListUp[spriteCounter];
			spriteCounter++;
			if(spriteCounter > 3){
				spriteCounter = 0;
			}
		}
	}
	animationDelay++;

		float step = speed * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards(transform.position, nextPos, step);
		
		if(this.transform.position == destination){
			if(destination == TurnManager.player.transform.position){
				TurnManager.killed();
			}
			animationDelay = 0;
			GameObject gate;
			Vector3 gatePos = new Vector3(0, 0, -1);
			if(GameObject.Find("Gate") != null){
				gate = GameObject.Find("Gate");
				gatePos = gate.transform.position;
				gatePos.x += 0.5f;
				gatePos.y += 0.5f;
			}
			Transform x;
			if(facing == 1){				
				destination += Vector3.left;
				if(((Tile)tilemap.GetTile(grid.WorldToCell(destination))).name == "NonPath" || destination == gatePos) {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(this.transform.position + Vector3.right), transform.rotation);
					TurnManager.killTiles.Add(grid.WorldToCell(this.transform.position + Vector3.right));
				} else {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
					TurnManager.killTiles.Add(grid.WorldToCell(destination));
				}			
			} else if(facing == 2){					
				destination += Vector3.down;
				if(((Tile)tilemap.GetTile(grid.WorldToCell(destination))).name == "NonPath" || destination == gatePos) {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(this.transform.position + Vector3.up), transform.rotation);
					TurnManager.killTiles.Add(grid.WorldToCell(this.transform.position + Vector3.up));
				} else {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
					TurnManager.killTiles.Add(grid.WorldToCell(destination));
				}
			} else if(facing == 3){
				destination += Vector3.right;
				if(((Tile)tilemap.GetTile(grid.WorldToCell(destination))).name == "NonPath" || destination == gatePos) {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(this.transform.position + Vector3.left), transform.rotation);
					TurnManager.killTiles.Add(grid.WorldToCell(this.transform.position + Vector3.left));
				} else {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
					TurnManager.killTiles.Add(grid.WorldToCell(destination));
				}
			} else {
				destination += Vector3.up;
				if(((Tile)tilemap.GetTile(grid.WorldToCell(destination))).name == "NonPath" || destination == gatePos) {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(this.transform.position + Vector3.down), transform.rotation);
					TurnManager.killTiles.Add(grid.WorldToCell(this.transform.position + Vector3.down));
				} else {
					x = Instantiate(cross, (Vector3)grid.WorldToCell(destination), transform.rotation);
					TurnManager.killTiles.Add(grid.WorldToCell(destination));
				}	
			}
			x.parent = transform;
			foreach(Transform child in transform){
				child.GetComponent<SpriteRenderer>().color = new Color(255, 215, 0, 1);
			}

			moving = false;
			TurnManager.enemyMoves--;
			this.enabled = false;
			
		}
	}
}
