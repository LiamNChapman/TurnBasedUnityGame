using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	
	public GameObject pauseMenu;
	public GameObject winMenu;
	public GameObject flour;
	public GameObject keyIcon;
	public GameObject turnManager;
	public Text levelName;
	public Text turns;
	public Text chargeNum;
	public Image TwoStar;
	public Image ThreeStar;
	public void reset() {
		TurnManager.turnCount = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void pause() {
		pauseMenu.SetActive(true);
		foreach(GameObject enemy in TurnManager.enemies) {
			enemy.SetActive(false);
		}		
		if(GameObject.Find("PressurePad") != null) {
			GameObject.Find("PressurePad").SetActive(false);
		}		
		TurnManager.player.SetActive(false);
		turnManager.SetActive(false);
	}
	public void winLevel() {
		foreach(GameObject enemy in TurnManager.enemies) {
			enemy.SetActive(false);
		}
		TurnManager.player.SetActive(false);
		turnManager.SetActive(false);
		winMenu.SetActive(true);
		int stars = Scoring.score();
		if(stars == 3) {
			TwoStar.color = Color.white;
			ThreeStar.color = Color.white;
		} else if(stars == 2) {
			TwoStar.color = Color.white;
		}
		levelName.text = SceneManager.GetActiveScene().name;
		turns.text = "Turns: " + TurnManager.turnCount;
		if(GameObject.Find("PressurePad") != null) {
			GameObject.Find("PressurePad").SetActive(false);
		}
	}

	void Update() {
		checkCharges();
		checkKeys();
	}
	void checkCharges() {
		chargeNum.text = "x " + TurnManager.player.GetComponent<Player>().abilityCharges;
		if(TurnManager.player.GetComponent<Player>().abilityCharges <= 0) {
			flour.GetComponent<Image>().color = Color.grey;
		} else {
			flour.GetComponent<Image>().color = Color.white;
		}
	}
	void checkKeys() {
		if(!TurnManager.player.GetComponent<Player>().haveKey) {
			keyIcon.GetComponent<Image>().color = Color.grey;
		} else {
			keyIcon.GetComponent<Image>().color = Color.white;
		}
	}

	public void nextLevel() {
		if(SceneManager.GetActiveScene().buildIndex == 7) {
			SceneManager.LoadScene(0);
		}
		TurnManager.turnCount = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void mainMenu() {
		TurnManager.turnCount = 0;
		SceneManager.LoadScene(0);
	}
}
