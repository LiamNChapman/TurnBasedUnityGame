using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut1mono : MonoBehaviour {

	public GameObject Ranger;
	public GameObject TurnManager;

	public void GameStart(){
		TurnManager.GetComponent<TurnManager>().enabled = true;
		Ranger.GetComponent<Ranger>().enabled = true;
	}
}
