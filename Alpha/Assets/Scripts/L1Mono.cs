using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1Mono : MonoBehaviour {

	public GameObject Player;
	public GameObject TurnManager;
	public GameObject Bezerker;
	public GameObject Bezerker1;

	public void StartGame(){
		GetComponent<UIManager>().enabled = true;
		Player.GetComponent<Player>().enabled = true;
		Player.GetComponent<testingTileHighlights>().enabled = true;
		TurnManager.GetComponent<TurnManager>().enabled = true;
		Bezerker.GetComponent<Bezerker>().enabled = true;
		Bezerker1.GetComponent<Bezerker>().enabled = true;
	}
}