using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut4mono : MonoBehaviour {

	public GameObject Player;
	public GameObject TurnManager;
	public GameObject Bezerker1;
	public GameObject Bezerker2;

	public void StartGame(){
		Player.GetComponent<Player>().enabled = true;
		Player.GetComponent<testingTileHighlights>().enabled = true;
		TurnManager.GetComponent<TurnManager>().enabled = true;
		Bezerker1.GetComponent<Bezerker>().enabled = true;
		Bezerker2.GetComponent<Bezerker>().enabled = true;
	}
}