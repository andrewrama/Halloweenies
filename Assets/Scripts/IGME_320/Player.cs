using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Keys inventory
    public int Keys
    {
        get
        {
            return keys;
        }
        set
        {
            keys = value;
        }
    }
    private int keys;
    [SerializeField] private GameObject gamePauser;

    // Reference to the enemy
    public GameObject enemy;
    public GameObject exitDoor;
    public GameObject gameStateManagerObject;
    GameStateManager gameStateManager;

    // Records if the player can scare or not
    bool canScare = false;

    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = gameStateManagerObject.GetComponent<GameStateManager>();
        keys = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckDist())
        {
            ShowButtonInst();
        }
    }

    public void OnScare(InputValue value)
    {
        // Test if they can use it
        if (canScare) // They can scare
        {
            // Scare the enemy
            enemy.GetComponent<Monster>().Spook();

            // Remove their ability to scare
            canScare = false;
        }
        else
        {
            Debug.Log("Can't scare yet IDOT. Get a candy you fucking FOOL");
        }
    }

    // Open the door (if it's close enough) 
    public void OnOpen(InputValue value)
    {
        exitDoor.GetComponent<EndDoorScript>().OpenDoor();
    }

    public void OnPause(InputValue value)
    {
        gamePauser.GetComponent<GameStateManager>().PauseGame();
    }

    public void ShowButtonInst()
    {
        //gamePauser.GetComponent<GameStateManager>().PauseGame();
    }

    public void CollectCandy()
    {
        Debug.Log("Candy Collected");
        canScare = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected");

        // Store the name to check what type of collidable object it is
        string tag = other.gameObject.tag;

        switch (tag) {
            case "Candy": // Candy
                CollectCandy();
                Destroy(other.gameObject);
                break;
            case "Enemy": // Enemy (oh the misery)
                gameStateManager.GameLost();
                break;
            case "EndDoor": // One does not simply walk into enddoor
                gameStateManager.GameWon();
                break;
        }
    }

    bool CheckDist()
    {
        float dist = Mathf.Sqrt(Mathf.Pow(this.gameObject.transform.position.x - exitDoor.transform.position.x, 2) +
            Mathf.Pow(this.gameObject.transform.position.z - exitDoor.transform.position.z, 2));
        return dist < 3.0f;
    }
}
