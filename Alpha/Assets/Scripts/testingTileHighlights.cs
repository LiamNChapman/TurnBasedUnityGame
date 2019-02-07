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
			Debug.Log("Cunt");
			enemies = new Vector3Int[TurnManager.enemies.Length];
			for(int i = 0; i < TurnManager.enemies.Length - 1; i++) {
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
			Transform x = Instantiate(highlight, currentTile, transform.rotation);
			x.parent = transform;
			Debug.Log("" + currentTile);
		}
		
	}
}
