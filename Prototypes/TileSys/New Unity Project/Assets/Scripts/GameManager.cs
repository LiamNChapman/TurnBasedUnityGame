using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject[] enemies;
    public GameObject player;
    public bool playerTurn;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.Find("Player");
        playerTurn = true;
        Turn();
    }

    /*void Update()
    {
        player.GetComponent<Character>().LocalUpdate();
        foreach (GameObject go in enemies)
        {
            go.GetComponent<Character>().LocalUpdate();
            if (go.GetComponent<Character>().xLoc == player.GetComponent<Character>().xLoc && go.GetComponent<Character>().yLoc == player.GetComponent<Character>().yLoc)
            {
                Debug.Log("death");
                SceneManager.LoadScene("SampleScene");
            }
        }
    }*/

    void Turn () {
        if (playerTurn){
        player.GetComponent<ClickToMove>().canMove = true;
        player.GetComponent<ClickToMove>().Movement();

       } else {
            playerTurn = true;
            foreach (GameObject go in enemies)
            {
                go.GetComponent<ClickToMove>().Movement();
            }
        }
        player.GetComponent<Character>().LocalUpdate();

        foreach (GameObject go in enemies)
        {
            go.GetComponent<Character>().LocalUpdate();
            if (go.GetComponent<Character>().xLoc == player.GetComponent<Character>().xLoc && go.GetComponent<Character>().yLoc == player.GetComponent<Character>().yLoc)
            {
                Debug.Log("death");
                SceneManager.LoadScene("SampleScene");
            }
        }
        Turn();

    }
}


