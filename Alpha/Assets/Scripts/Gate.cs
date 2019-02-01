using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {
	public bool open = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(open){
			gameObject.SetActive(false);
		} else if(!open){
			gameObject.SetActive(true);
		}
	}
}
