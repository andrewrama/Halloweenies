using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReset : MonoBehaviour
{
    [SerializeField] private bool gameIsWon;

    // Start is called before the first frame update
    void Start()
    {
        //Intialize Variables

        // reset player position
        // reset monster position
        // reset powerups/other collectibles
        gameIsWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndGame()
    {
        gameIsWon = true;
        // stop the game
        // pull up win screen scene here
    }

    void ResetGame()
    {
        gameIsWon = false;
        //reintialize variables
    }
}
