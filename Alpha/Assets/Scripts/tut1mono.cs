using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut1mono : MonoBehaviour {

	public GameObject Player;
	public GameObject Ranger;
	public GameObject TurnManager;

	public void GameStart(){
		GetComponent<UIManager>().enabled = true;
		Player.GetComponent<Player>().enabled = true;
		Player.GetComponent<testingTileHighlights>().enabled = true;
		TurnManager.GetComponent<TurnManager>().enabled = true;
		Ranger.GetComponent<Ranger>().enabled = true;
	}
}
