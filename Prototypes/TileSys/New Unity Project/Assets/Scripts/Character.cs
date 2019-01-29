using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public float xLoc;
    public float yLoc;
    public char type;

    public void LocalUpdate () {
        xLoc = this.transform.position.x;
        yLoc = this.transform.position.y;
    }
}
