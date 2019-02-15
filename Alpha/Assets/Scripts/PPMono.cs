using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPMono : MonoBehaviour {

	public GameObject Player;
	public GameObject TurnManager;
	public GameObject Bezerker;
	public GameObject Ranger;
	public GameObject Scout;
	public GameObject Warrior1;
	public GameObject Warrior;
	public GameObject PressurePad;

	public void StartGame(){
		GetComponent<UIManager>().enabled = true;
		Player.GetComponent<Player>().enabled = true;
		Player.GetComponent<testingTileHighlights>().enabled = true;
		TurnManager.GetComponent<TurnManager>().enabled = true;
		Bezerker.GetComponent<Bezerker>().enabled = true;
		Ranger.GetComponent<Ranger>().enabled = true;
		Scout.GetComponent<Scout>().enabled = true;
		Warrior1.GetComponent<Warrior>().enabled = true;
		Warrior.GetComponent<Warrior>().enabled = true;
		PressurePad.GetComponent<PressurePad>().enabled = true;
	}
}