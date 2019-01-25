using UnityEngine;
using System.Collections;

public class Bezerker : MonoBehaviour
{
    public bool angry = true;
    public bool charging = true;


    void Update()
    {
        if (TurnManagerBez.currentState == TurnManagerBez.TurnStates.ENEMYMOVE)
        {

            /*if(angry){
                charging = true;
                angry = false;
            } else*/ if(charging){
                this.transform.Translate(new Vector3(-4, 0, 0));
                this.transform.Rotate(0, 0, 180);
                charging = false;
            }
            TurnManagerBez.nextState();
        }

    }
}
