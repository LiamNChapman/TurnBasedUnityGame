using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut3mono : MonoBehaviour {

	public GameObject Player;
	public GameObject TurnManager;
	public GameObject Scout;

	public void StartGame(){
		GetComponent<UIManager>().enabled = true;
		Player.GetComponent<Player>().enabled = true;
		Player.GetComponent<testingTileHighlights>().enabled = true;
		TurnManager.GetComponent<TurnManager>().enabled = true;
		Scout.GetComponent<Scout>().enabled = true;
	}
}
