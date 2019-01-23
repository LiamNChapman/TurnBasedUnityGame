using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClickToMove : MonoBehaviour {

	public Grid grid;
	public Tilemap tilemap;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
			
		if(Input.GetMouseButtonDown(0)) {
			Debug.Log("player start position: " + this.transform.position);
			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        	Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);
			if(tilemap.GetTile(coordinate).name != "NonPath"){
				Vector3 newPosition = (Vector3)coordinate;
				newPosition.x += 0.5f;
				newPosition.y += 0.5f;
				this.transform.position = newPosition;
			}
        	Debug.Log("click: " + coordinate);
			Debug.Log("New Player Position: " + this.transform.position);
		}
	}
}
