using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class levelSelect : MonoBehaviour {
	
	public void LoadPrototype() {
		SceneManager.LoadScene(EventSystem.current.currentSelectedGameObject.name);
	}

}
