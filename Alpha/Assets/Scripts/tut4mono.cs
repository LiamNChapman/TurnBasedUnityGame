using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut4mono : MonoBehaviour {

	public GameObject Player;
	public GameObject TurnManager;
	public GameObject Bezerker1;
	public GameObject Bezerker2;

	public GameObject Panel1;
	public GameObject Panel2;
	public GameObject Panel3;
	public GameObject Panel4;


	public GameObject Button;

	Vector3 pos;
	Vector3 pos1;
	Vector3 pos2;
	Vector3 pos3;
	Vector3 pos4;
	Vector3 pos5;

	void Start(){
		pos = Player.transform.position;
		pos1 = pos;
		pos1.x += 1;
		pos2 = pos1;
		pos2.x += 1;
		pos3 = pos2;
		pos3.x += 1;
		pos4 = pos3;
		pos4.x += 2;
		pos4.y += 2;
		pos5 = pos4;
		pos5.x += 1;
	}

	public void StartGame(){
		GetComponent<UIManager>().enabled = true;
		Player.GetComponent<Player>().enabled = true;
		Player.GetComponent<testingTileHighlights>().enabled = true;
		TurnManager.GetComponent<TurnManager>().enabled = true;
		Bezerker1.GetComponent<Bezerker>().enabled = true;
		Bezerker2.GetComponent<Bezerker>().enabled = true;
		Panel1.SetActive(true);
	}

	void Update(){
		if(Player.transform.position == pos1){
			Panel1.SetActive(false);
			Panel2.SetActive(true);
		}

		if(Player.transform.position == pos2){
			Panel2.SetActive(false);
			Panel3.SetActive(true);
		}

		if(Player.transform.position == pos3){
			Panel3.SetActive(false);
			
		}

		if(Player.transform.position == pos4){
			Button.SetActive(true);
			Panel4.SetActive(true);
		}

		if(Player.transform.position == pos5){
			Panel4.SetActive(false);
		}
	}
}