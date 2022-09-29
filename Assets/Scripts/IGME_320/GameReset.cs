using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameStates
{
    Title,
    Game,
    Pause,
    WinScreen,
    GameOver
}

public class GameReset : MonoBehaviour
{
    [SerializeField] private bool gameIsWon;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3[] enemySpawns;

    //Fields for current State
    private GameStates currentState;
    private GameStates prevState;

    // Start is called before the first frame update
    void Start()
    {
        //Intialize Variables
        enemySpawns = new Vector3[2] { new Vector3(30.0f, 0.0f, -28.0f), new Vector3(-30.0f, 0.0f, -28.0f) };

        //makes the title screen the first scene
        currentState = GameStates.Title;

        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        //Game State loop
        switch (currentState)
        {
            //Title Screen
            case GameStates.Title:
                break;


        }


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
        player.transform.position = new Vector3(0.0f, 0.0f, -25.0f);
        // reset monster position
        enemy.transform.position = enemySpawns[Random.Range(0,2)];
        // reset powerups/other collectibles

        gameIsWon = false;
    }
}
