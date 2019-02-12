using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut2mono : MonoBehaviour {

	public GameObject Player;
	public GameObject TurnManager;
	public GameObject Warrior;
	public GameObject Panel1;
	public GameObject Panel2;
	public GameObject Panel3;
	public GameObject Panel4;
	public GameObject Panel5;
	public GameObject Button;

	Vector3 pos1;
	Vector3 pos2;
	Vector3 pos3;
	Vector3 pos4;
	Vector3 pos5;
	Vector3 pos6;
	Vector3 pos7;

	void Start(){
		pos1 = Player.transform.position; 
		pos2 = pos1;
		pos2.x += 1;
		pos3 = pos2;
		pos3.x += 1;
		pos4 = pos3;
		pos4.x += 1;
		pos5 = pos4;
		pos5.x += 1;
		pos5.y += 1;
		pos6 = pos5;
		pos6.x += 1;
		pos7 = pos6;
		pos7.y -= 1;
	}

	public void StartGame(){
		GetComponent<UIManager>().enabled = true;
		Player.GetComponent<Player>().enabled = true;
		Player.GetComponent<testingTileHighlights>().enabled = true;
		TurnManager.GetComponent<TurnManager>().enabled = true;
		Warrior.GetComponent<Warrior>().enabled = true;
		Panel1.SetActive(true);
	}

	void Update(){
		if(Player.transform.position == pos2){
			Panel1.SetActive(false);
			Panel2.SetActive(true);
		}
		
		if(Player.transform.position == pos3){
			Panel2.SetActive(false);
			Panel3.SetActive(true);
			
		}

		if(Player.transform.position == pos4){
			Panel3.SetActive(false);			
		}

		if(Player.transform.position == pos5){
			Button.SetActive(true);
			Panel4.SetActive(true);
		}

		if(Player.transform.position == pos6){
			Panel4.SetActive(false);
			Panel5.SetActive(true);
		}

		if(Player.transform.position == pos7){
			Panel5.SetActive(false);
		}
	}
}
