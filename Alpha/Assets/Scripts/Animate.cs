using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour {

    public Sprite[] spriteList;
    int count = 0;
    int delay = 0;
    // Update is called once per frame

    void Update () {
        if (delay > 20) {
            this.GetComponent<SpriteRenderer>().sprite = spriteList[count];
            count++;
            if (count == 4)
            {
                count = 0;
            }
            delay = 0;
        } else {
            delay++;
        }
	}
}
