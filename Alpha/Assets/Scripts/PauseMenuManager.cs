using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

	public GameObject pauseMenu;

	public void resume() {
		pauseMenu.SetActive(false);
		//This is where I will reenable all scripts disabled when pausing and set the time scale to 1
	}
	public void quit() {
		SceneManager.LoadScene(0);
	}
}
