using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour {

	static int[] scores = new int[9];

	void Start () {
		scores[0] = 100;
		scores[1] = 100;
		scores[2] = 100;
		scores[3] = 100;
		scores[4] = 15;
		scores[5] = 16;
		scores[6] = 24;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
