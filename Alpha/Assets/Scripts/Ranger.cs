using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Ranger : MonoBehaviour {

	List<Vector3Int> list = new List<Vector3Int>();
	public Tilemap tilemap;
	public Grid grid;
	int facing = 1;
	public Transform cross;

	void Start() {
		Vector3 initialPos = this.transform.position;
		Vector3Int pos = grid.WorldToCell(initialPos);
		Tile tile = (Tile)tilemap.GetTile(pos);
		while(tile.name != "NonPath") {
			list.Add(pos);
			//Instantiate(cross, pos, transform.rotation);
			initialPos += Vector3.left;
			pos = grid.WorldToCell(initialPos);
			tile = (Tile)tilemap.GetTile(pos);
			Debug.Log(pos);
		}
	}

	void Update(){
		rotate();
		TurnManager.enemyMoves--;
	}
	void rotate() {
		foreach(Vector3 los in list) {
			Vector3 hit = los;
			hit.x += 0.5f;
			hit.y += 0.5f;
			if(TurnManager.player.transform.position == hit) {
				TurnManager.killed();
				return;
			}
		}
		facing++;
		list = new List<Vector3Int>();
		Vector3 initialPos = this.transform.position;
		Vector3Int pos = grid.WorldToCell(initialPos);
		Tile tile = (Tile)tilemap.GetTile(pos);
		foreach (Transform child in transform) {
             Destroy(child.gameObject);
        }
		while(tile.name != "NonPath") {
			list.Add(pos);
			Transform x = Instantiate(cross, pos, transform.rotation);
			x.parent = transform;
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
			Debug.Log(pos);
		}
		if(facing == 4){
			facing = 0;
		}
	}
}
