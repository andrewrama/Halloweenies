using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private bool gameIsWon;
    [SerializeField] private bool gameIsPaused;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject circleKey;
    [SerializeField] private GameObject triangleKey;
    [SerializeField] private GameObject squareKey;
    [SerializeField] private GameObject popUpButtonInstructions;
    [SerializeField] private Vector3[] enemySpawns;
    [SerializeField] private Vector3[] circleKeySpawns;
    [SerializeField] private Vector3[] triangleKeySpawns;
    [SerializeField] private Vector3[] squareKeySpawns;


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

        if(gameIsPaused)
        {
            // Pause game;
            pauseMenu.SetActive(true);
            //TODO: Get the goddamn monster to shut up.
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }


    }

    public void EndGame()
    {
        gameIsWon = true;
        // stop the game
        // pull up win screen scene here
    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
    }

    public void ShowDoorButton(string message)
    {
        popUpButtonInstructions.SetActive(true);
        popUpButtonInstructions.GetComponent<Text>().text = message;
    }

    public void HideDoorButton()
    {
        popUpButtonInstructions.SetActive(false);
    }

    void ResetGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // reset player position
        player.transform.position = new Vector3(0.0f, 0.0f, -25.0f);
        // reset monster position
        enemy.transform.position = enemySpawns[Random.Range(0, enemySpawns.Length)];
        // reset powerups/other collectibles
        circleKey.transform.position = circleKeySpawns[Random.Range(0, circleKeySpawns.Length)];
        triangleKey.transform.position = triangleKeySpawns[Random.Range(0, triangleKeySpawns.Length)];
        squareKey.transform.position = squareKeySpawns[Random.Range(0, squareKeySpawns.Length)];

        gameIsWon = false;
    }

    //Functions that change the scene based on winning or losing

    public void GameWon()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(2);
    }

    public void GameLost()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(4);
    }
}
