using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class levelSelect : MonoBehaviour {
	public GameObject levelSelectPanal;
	
	public void LoadLevel() {
		SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.name);
	}

	public void playButton() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void levelSelection() {
		levelSelectPanal.SetActive(true);
	}
	

}
