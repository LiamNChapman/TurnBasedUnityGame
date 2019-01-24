/*
    Created by Joe Marshall
    Jan 23rd, 2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : MonoBehaviour
{

    // Only move if it is the enemy turn state
    void Update()
    {
        //if(TurnManagerLOS.currentState == TurnManagerLOS.TurnStates.ENEMYMOVE){
        
        this.transform.Rotate(0, 0, 90);
        TurnManagerLOS.nextState();
        //}
    }

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (vertical)
        {
            //If the Sprite exited at the top of a path
            if (direction == 1)
            {
                //Move it back onto the path
                this.transform.position += Vector3.down;
                //Set it to move downwards
                direction = 0;
                //Move down one space
                this.Move();
            }
            else
            {
                this.transform.position += Vector3.up;
                direction = 1;
                this.Move();
            }
        }
        else
        {
            //If the sprite exited on the right of a path
            if (direction == 1)
            {
                //Move back onto path
                this.transform.position += Vector3.left;
                //Set to move left
                direction = 0;
                //Move
                this.Move();
            }
            else
            {
                this.transform.position += Vector3.right;
                direction = 1;
                this.Move();
            }
        }
    }*/
}
