using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMono : MonoBehaviour {

	public GameObject Player;
	public GameObject TurnManager;
	public GameObject Scout;
	public GameObject Scout1;
	public GameObject Scout2;

	public void StartGame(){
		GetComponent<UIManager>().enabled = true;
		Player.GetComponent<Player>().enabled = true;
		Player.GetComponent<testingTileHighlights>().enabled = true;
		TurnManager.GetComponent<TurnManager>().enabled = true;
		Scout.GetComponent<Scout>().enabled = true;
		Scout1.GetComponent<Scout>().enabled = true;
		Scout2.GetComponent<Scout>().enabled = true;
	}
}