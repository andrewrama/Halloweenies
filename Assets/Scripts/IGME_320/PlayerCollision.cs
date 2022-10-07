using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckDist())
        {
            //Debug.Log("Text: At the door");
            gameManager.GetComponent<GameStateManager>().GameLost();
        }
    }

    // Checks the player's distance from the enemy
    bool CheckDist()
    {
        float dist = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - enemy.transform.position.x, 2) +
            Mathf.Pow(player.transform.position.z - enemy.transform.position.z, 2));
        return dist < 2.0f;
    }
}
