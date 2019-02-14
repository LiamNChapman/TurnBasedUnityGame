using System.Collections;
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
	public Sprite[] spriteList;
	public bool isStunned = false;
	public int stunLeft = 0;
	bool deleteLos = false;

	public GameObject Stun;
	GameObject StunInstance;

	// Use this for initialization
	void Start () {
		Vector3 initialPos = this.transform.position;
		hitBoxes[0] = grid.WorldToCell(initialPos + Vector3.left);
		hitBoxes[1] = grid.WorldToCell(initialPos + Vector3.down);
		hitBoxes[2] = grid.WorldToCell(initialPos + Vector3.right);
		hitBoxes[3] = grid.WorldToCell(initialPos + Vector3.up);
		Transform x = Instantiate(cross, (Vector3)hitBoxes[facing-1], transform.rotation);
		x.parent = transform;
		x.GetComponent<SpriteRenderer>().color = Color.grey; 
		TurnManager.killTiles.Add(hitBoxes[facing-1]);
		initialLOS = facing;
		this.GetComponent<SpriteRenderer>().sprite = spriteList[facing-1];
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<SpriteRenderer>().sprite = spriteList[facing-1];
		if(!isStunned){

			Vector3Int playerPos = grid.WorldToCell(TurnManager.player.transform.position);
			if(playerPos == hitBoxes[facing-1]) {
				TurnManager.killed();
			}
			if(!turn) {
				TurnManager.killTiles.Add(hitBoxes[facing-1]);
				TurnManager.enemyMoves--;
				turn = true;
			}
			if(facing != initialLOS) {
				foreach (Transform child in transform) {
    	         		Destroy(child.gameObject);
    	    	}
				Transform x = Instantiate(cross, (Vector3)hitBoxes[facing-1], transform.rotation);
				x.parent = transform;
				x.GetComponent<SpriteRenderer>().color = Color.grey; 
				TurnManager.killTiles.Add(hitBoxes[facing-1]);
				initialLOS = facing;
			}
		} else {
			if(stunLeft == 3){
				StunInstance = Instantiate(Stun, transform.position, Quaternion.identity);
			}
			if(deleteLos == false) {
				foreach (Transform child in transform) {
    	    		Destroy(child.gameObject);
    	    	}
				deleteLos = true;
			}
			if(!turn) {
				stunLeft--;
				if(stunLeft < 1){
					Destroy(StunInstance);
					isStunned = false;
					Transform x = Instantiate(cross, (Vector3)hitBoxes[facing-1], transform.rotation);
					x.parent = transform;
					x.GetComponent<SpriteRenderer>().color = Color.grey; 
					TurnManager.killTiles.Add(hitBoxes[facing-1]);
					deleteLos = false;
				}
				TurnManager.enemyMoves--;
				turn = true;
			}
		}
	}
}
