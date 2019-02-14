using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class levelSelect : MonoBehaviour {
	public GameObject levelSelectPanal;
	public GameObject playerStars;

	void Start() {
		if(playerStars != null) {
			int score = 0;
			foreach(int star in Scoring.playerScores){
				score += star;
			}
			playerStars.GetComponent<Text>().text = ""+score;
		}
	}
	
	public void LoadLevel() {
		SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.name);
	}

	public void playButton() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void levelSelection() {
		levelSelectPanal.SetActive(true);
	}

	public void playAgain() {
		SceneManager.LoadScene(1);
	}
	
	public void backToMainMenu() {
		SceneManager.LoadScene(0);
	}

	public void backButton() {
		levelSelectPanal.SetActive(false);
	}

}
