using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Disctraction : MonoBehaviour {

	public Grid grid;
	public Tilemap tilemap;
	bool inUse = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			Debug.Log("reeee");
			inUse = true;
		}
			if(inUse && Input.GetMouseButtonDown(0)) {
				//get the coordinates from the mouse click.
				Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        		Vector3Int coordinate = grid.WorldToCell(mouseWorldPos);

				// Check if the tile isn't a path.
				if(tilemap.GetTile(coordinate).name != "NonPath" ){
					Debug.Log("weeeee");
					inUse = false;
				}
			}
	}
}
