using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class testingTileHighlights : MonoBehaviour {

	public Grid grid;
	public Tilemap tilemap;
	public Transform highlight;
	Vector3Int currentTile;
	Vector3Int[] enemies;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(enemies == null) {
			enemies = new Vector3Int[TurnManager.enemies.Length];
			for(int i = 0; i < TurnManager.enemies.Length; i++) {
				enemies[i] = grid.WorldToCell(TurnManager.enemies[i].transform.position);
			}
		}
	
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
		if(currentTile != coordinate) {
			foreach (Transform child in transform) {
             Destroy(child.gameObject);
        	}
			currentTile = coordinate;
			playerMoveTiles();		
		}
		
	}

	void playerMoveTiles() {
		Vector3Int upTile = currentTile + Vector3Int.up;
		Vector3Int rightTile = currentTile + Vector3Int.right;
		Vector3Int downTile = currentTile + Vector3Int.down;
		Vector3Int leftTile = currentTile + Vector3Int.left;
		Transform upHighLight = null;
		Transform rightHighLight = null;
		Transform downHighLight = null;
		Transform leftHighLight = null;
		if(tilemap.GetTile(upTile).name != "NonPath") {
			upHighLight = Instantiate(highlight, upTile, transform.rotation);
			upHighLight.parent = transform;
		}
		if(tilemap.GetTile(rightTile).name != "NonPath") {
			rightHighLight = Instantiate(highlight, rightTile, transform.rotation);
			rightHighLight.parent = transform;
		}
		if(tilemap.GetTile(downTile).name != "NonPath") {
			downHighLight = Instantiate(highlight, downTile, transform.rotation);
			downHighLight.parent = transform;
		}
		if(tilemap.GetTile(leftTile).name != "NonPath") {
			leftHighLight = Instantiate(highlight, leftTile, transform.rotation);
			leftHighLight.parent = transform;
		}
		for(int i = 0; i < TurnManager.enemies.Length; i++) {			
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
				x.parent = transform;
				x.GetComponent<SpriteRenderer>().color = Color.red;
			}
		}
		
	}

}
