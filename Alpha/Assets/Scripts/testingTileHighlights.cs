using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class testingTileHighlights : MonoBehaviour {

	public Grid grid;
	public Tilemap tilemap;
	public Transform highlight;
	public Transform dad;
	Vector3Int currentTile;
	Vector3Int[] enemies;
	Vector3Int coordinate;
	Transform upHighLight = null;
	Transform rightHighLight = null;
	Transform downHighLight = null;
	Transform leftHighLight = null;
	// Use this for initialization
	void Start () {
		coordinate = grid.WorldToCell(transform.position);
		currentTile = coordinate;
		enemies = new Vector3Int[TurnManager.enemies.Length];

	}
	
	// Update is called once per frame
	void Update () {
		if(enemies == null) {
			enemies = new Vector3Int[TurnManager.enemies.Length];
			for(int i = 0; i < TurnManager.enemies.Length; i++) {
				enemies[i] = grid.WorldToCell(TurnManager.enemies[i].transform.position);
			}
		}
	
		//Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        coordinate = grid.WorldToCell(transform.position);
		if(currentTile != coordinate) {		
			currentTile = coordinate;
		}		
	}

	public void playerMoveTiles() {
		coordinate = grid.WorldToCell(transform.position);
		if(currentTile != coordinate) {		
			currentTile = coordinate;
		}		
		Vector3Int upTile = currentTile + Vector3Int.up;
		Vector3Int rightTile = currentTile + Vector3Int.right;
		Vector3Int downTile = currentTile + Vector3Int.down;
		Vector3Int leftTile = currentTile + Vector3Int.left;
		if(tilemap.GetTile(upTile).name != "NonPath") {
			upHighLight = Instantiate(highlight, upTile, transform.rotation);
			upHighLight.parent = dad;
		}
		if(tilemap.GetTile(rightTile).name != "NonPath") {
			rightHighLight = Instantiate(highlight, rightTile, transform.rotation);
			rightHighLight.parent = dad;
		}
		if(tilemap.GetTile(downTile).name != "NonPath") {
			downHighLight = Instantiate(highlight, downTile, transform.rotation);
			downHighLight.parent = dad;
		}
		if(tilemap.GetTile(leftTile).name != "NonPath") {
			leftHighLight = Instantiate(highlight, leftTile, transform.rotation);
			leftHighLight.parent = dad;
		}
	}
	public void colorTiles() {
		Vector3Int upTile = currentTile + Vector3Int.up;
		Vector3Int rightTile = currentTile + Vector3Int.right;
		Vector3Int downTile = currentTile + Vector3Int.down;
		Vector3Int leftTile = currentTile + Vector3Int.left;
		for(int i = 0; i < TurnManager.enemies.Length; i++) {
			Debug.Log(TurnManager.enemies[i].name);			
			enemies[i] = grid.WorldToCell(TurnManager.enemies[i].transform.position);
		}
		foreach(Vector3Int enemy in enemies) {			
			if(upHighLight!=null && enemy == upTile) {
				upHighLight.GetComponent<SpriteRenderer>().color = Color.red;
			}
			if(rightHighLight!=null && enemy == rightTile) {
				rightHighLight.GetComponent<SpriteRenderer>().color = Color.red;
			}
			if(downHighLight!=null && enemy == downTile) {
				downHighLight.GetComponent<SpriteRenderer>().color = Color.red;
			}
			if(leftHighLight!=null && enemy == leftTile) {
				leftHighLight.GetComponent<SpriteRenderer>().color = Color.red;
			}
		}
		foreach(Vector3Int killTile in TurnManager.killTiles) {
			if(upHighLight!=null && killTile == upTile) {
				upHighLight.GetComponent<SpriteRenderer>().color = Color.red;
			}
			if(rightHighLight!=null && killTile == rightTile) {
				rightHighLight.GetComponent<SpriteRenderer>().color = Color.red;
			}
			if(downHighLight!=null && killTile == downTile) {
				downHighLight.GetComponent<SpriteRenderer>().color = Color.red;
			}
			if(leftHighLight!=null && killTile == leftTile) {
				leftHighLight.GetComponent<SpriteRenderer>().color = Color.red;
			}
			if(currentTile == killTile) {
				Transform x = Instantiate(highlight, currentTile, transform.rotation);
				x.parent = dad;
				x.GetComponent<SpriteRenderer>().color = Color.red;
			}
		}
		
	}
}
