using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	
	public GameObject pauseMenu;
	public GameObject winMenu;
	public GameObject flour;
	public Text levelName;
	public Text turns;
	public Text chargeNum;
	
	public void reset() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void pause() {
		pauseMenu.SetActive(true);
		foreach(GameObject enemy in TurnManager.enemies) {
			enemy.SetActive(false);
		}
		TurnManager.player.SetActive(false);		
	}
	public void winLevel() {
		foreach(GameObject enemy in TurnManager.enemies) {
			enemy.SetActive(false);
		}
		TurnManager.player.SetActive(false);	
		winMenu.SetActive(true);
		levelName.text = SceneManager.GetActiveScene().name;
		turns.text = "Turns: " + TurnManager.turnCount;
	}

	void Update() {
		chargeNum.text = "x " + TurnManager.player.GetComponent<Player>().abilityCharges;
		if(TurnManager.player.GetComponent<Player>().abilityCharges <= 0) {
			flour.GetComponent<Image>().color = Color.grey;
		} else {
			flour.GetComponent<Image>().color = Color.white;
		}
	}

}
