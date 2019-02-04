using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleClass : MonoBehaviour {

	public static ParticleSystem FlourPoof;

	public static void emitHere(Vector3 pos){
		FlourPoof.transform.Translate(pos);
		FlourPoof.Emit(1);
	}
}
