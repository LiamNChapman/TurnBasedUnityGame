using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut2mono : MonoBehaviour {

	public GameObject Player;
	public GameObject TurnManager;
	public GameObject Warrior;

	public void StartGame(){
		Player.GetComponent<Player>().enabled = true;
		Player.GetComponent<testingTileHighlights>().enabled = true;
		TurnManager.GetComponent<TurnManager>().enabled = true;
		Warrior.GetComponent<Warrior>().enabled = true;
	}
}
