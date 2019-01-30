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

    void Update()
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
    }

    void Turn () {
        
            player.GetComponent<Character>().Movement(playerTurn);
            
            foreach (GameObject go in enemies)
            {
                go.GetComponent<Character>().Movement(playerTurn);
            }
        
    }
}


