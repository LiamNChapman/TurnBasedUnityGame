using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut1mono : MonoBehaviour {

	public GameObject Player;
	public GameObject Ranger;

	public void GameStart(){
		Player.GetComponent<Player>().enabled = true;
		Ranger.GetComponent<Ranger>().enabled = true;
	}
}
