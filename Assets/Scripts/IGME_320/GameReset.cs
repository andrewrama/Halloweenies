using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReset : MonoBehaviour
{
    [SerializeField] private bool gameIsWon;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Intialize Variables
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameIsWon)
        {
            ResetGame();
        }
    }

    void EndGame()
    {
        gameIsWon = true;
        // stop the game
        // pull up win screen scene here
    }

    void ResetGame()
    {
        // reset player position
        player.transform.position = Vector3.one;
        // reset monster position
        enemy.transform.position = Vector3.zero;
        // reset powerups/other collectibles

        gameIsWon = false;
    }
}
