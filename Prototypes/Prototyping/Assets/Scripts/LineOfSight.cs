using UnityEngine;
using System.Collections;

public class LineOfSight: MonoBehaviour
{

    public bool playerSeen = false;
    // Use this for initialization

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerSeen = true;
    }
}
