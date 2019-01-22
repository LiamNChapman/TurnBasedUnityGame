using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public void playerMove() {
		transform.position += Vector3.up;
	}
}
