using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
	
	public GameObject pauseMenu;

	public void reset() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void pause() {
		pauseMenu.SetActive(true);
		//This will be where I disable all scripts and set the time scale to 0;
	}

}
