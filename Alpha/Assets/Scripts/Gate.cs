using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {
	public bool open = false;
	GameObject OpenInstance;
	GameObject CloseInstance;
	public GameObject GateClose;
	public GameObject GateOpen;
	
	// Update is called once per frame
	void Update () {
		if(open){
			//CloseInstance = Instantiate(GateOpen, transform.position, Quaternion.identity);
			//Destroy(CloseInstance);
			gameObject.SetActive(false);
		}
		if (!open){
			//CloseInstance = Instantiate(GateClose, transform.position, Quaternion.identity);
			//Destroy(CloseInstance);
			gameObject.SetActive(true);
		}
	}
}
