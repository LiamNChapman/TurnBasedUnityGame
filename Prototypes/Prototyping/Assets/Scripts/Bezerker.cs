using UnityEngine;
using System.Collections;

public class Bezerker : MonoBehaviour
{

    public bool angry = false;
    // Update is called once per frame
    void Update()
    {
        if (TurnManagerLOS.currentState == TurnManagerLOS.TurnStates.ENEMYMOVE)
        {

            TurnManagerLOS.nextState();
        }
    }
}
