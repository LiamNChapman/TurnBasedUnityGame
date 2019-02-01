using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

	public GameObject pauseMenu;

	public void resume() {
		pauseMenu.SetActive(false);
		foreach(GameObject enemy in TurnManager.enemies) {
			enemy.SetActive(true);
		}
		TurnManager.player.SetActive(true);	
	}
	public void quit() {
		SceneManager.LoadScene(0);
	}
}
