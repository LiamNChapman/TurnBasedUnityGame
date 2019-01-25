using UnityEngine;
using System.Collections;

public class Bezerker : MonoBehaviour {
    public bool charging = false;
    public bool ready= false;

    void Update() {
        if (TurnManagerBez.currentState == TurnManagerBez.TurnStates.ENEMYMOVE) {

            if(charging){
                ready = true;
                charging = false;
            }else if(ready){
                this.transform.Translate(new Vector3(-4, 0, 0));
                this.transform.Rotate(0, 0, 180);
                ready = false;
            }
            TurnManagerBez.nextState();
        }

    }

    public void Charging(){
        charging = true;
        
    }
}
