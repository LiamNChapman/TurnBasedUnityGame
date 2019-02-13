using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour {

	static int[] scores = new int[9];
	
	public static int score() {
		//ammount of turns for 3 stars per level
		scores[0] = 100;//tutorial1
		scores[1] = 100;//tutorial2
		scores[2] = 100;//tutorial3
		scores[3] = 100;//tutorial4
		scores[4] = 15;//easylevel2 - 15
		scores[5] = 16;//easylevel1 - 16
		scores[6] = 24;//easylevel3 - 24
		scores[7] = 100;//alpha1 - tbd
		scores[8] = 100;//liamstestlevel - tbd
		int turns = TurnManager.turnCount;
		if(turns <= scores[SceneManager.GetActiveScene().buildIndex - 1]) {			
			return 3;
		} else if(turns == scores[SceneManager.GetActiveScene().buildIndex - 1] + 1) {
			return 2;
		} else {
			return 1;
		}
	}
}
