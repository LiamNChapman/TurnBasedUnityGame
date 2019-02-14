using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelSelectStars : MonoBehaviour {
	public GameObject oneStar;
	public GameObject twoStar;
	public GameObject threeStar;
	public int index;
	// Use this for initialization
	void Start () {
		if(Scoring.playerScores[index-1] >= 1) {
			oneStar.GetComponent<Image>().color = Color.white;
		}
		if(Scoring.playerScores[index-1] >= 2) {
			twoStar.GetComponent<Image>().color = Color.white;
		}
		if(Scoring.playerScores[index-1] > 2) {
			threeStar.GetComponent<Image>().color = Color.white;
		}
	}
	
	
}
