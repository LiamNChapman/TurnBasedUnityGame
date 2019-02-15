using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut3mono : MonoBehaviour {

	public GameObject Player;
	public GameObject TurnManager;
	public GameObject Scout;

	public GameObject Panel1;
	public GameObject Panel2;
	public GameObject Panel3;

	public GameObject Button;

	Vector3 pos;
	Vector3 pos1;
	Vector3 pos2;
	Vector3 pos3;

	public void StartGame(){
		GetComponent<UIManager>().enabled = true;
		Player.GetComponent<Player>().enabled = true;
		Player.GetComponent<testingTileHighlights>().enabled = true;
		TurnManager.GetComponent<TurnManager>().enabled = true;
		Scout.GetComponent<Scout>().enabled = true;
		Panel1.SetActive(true);
	}

	void Start(){
		pos = Player.transform.position;
		pos1 = pos;
		pos1.x += 1;
		pos2 = pos1;
		pos2.x += 1;
		pos3 = pos2;
		pos3.x += 1;
	}

	void Update(){
		if(Player.transform.position == pos1){
			Panel1.SetActive(false);
			Panel2.SetActive(true);
			Button.SetActive(true);
			if(Player.GetComponent<Player>().abilityCharges == 0){
				Panel2.SetActive(false);
				Panel3.SetActive(true);
			}
		}

		if(Player.transform.position == pos2){
			Panel3.SetActive(false);
		}

		if(Player.transform.position == pos3){
			Panel3.SetActive(false);
		}
	}
}
