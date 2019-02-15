using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject turnManager;
	public void resume() {
		pauseMenu.SetActive(false);
		foreach(GameObject enemy in TurnManager.enemies) {
			enemy.SetActive(true);
		}
		turnManager.SetActive(true);
		TurnManager.player.SetActive(true);	
		if(GameObject.Find("PressurePad") != null) {
			GameObject.Find("PressurePad").SetActive(true);
		}
	}
	public void quit() {
		SceneManager.LoadScene(0);
	}
}
