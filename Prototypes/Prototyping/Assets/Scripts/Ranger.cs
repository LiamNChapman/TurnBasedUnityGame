/*
    Created by Joe Marshall
    Jan 23rd, 2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ranger : MonoBehaviour
{
    // Only move if it is the enemy turn state
    void Update()
    {
        if(TurnManagerLOS.currentState == TurnManagerLOS.TurnStates.ENEMYMOVE){



            transform.Rotate(0, 0, 90);

            TurnManagerLOS.nextState();
        }
    }


}
