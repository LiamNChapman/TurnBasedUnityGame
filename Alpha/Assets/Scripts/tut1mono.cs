using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut1mono : MonoBehaviour {

	public GameObject Player;
	public GameObject Ranger;
	public GameObject TurnManager;

	public void GameStart(){
		TurnManager.GetComponent<TurnManager>().enabled = true;
		Player.GetComponent<Player>().enabled = true;
		Player.GetComponent<testingTileHighlights>().enabled = true;
		Ranger.GetComponent<Ranger>().enabled = true;
	}
}
