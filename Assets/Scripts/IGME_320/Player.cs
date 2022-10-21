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
    public bool HasEndKey
    {
        get
        {
            return hasEndKey;
        }
        set
        {
            hasEndKey = value;
        }
    }

    [SerializeField] private GameObject gamePauser;

    // Reference to the enemy
    public GameObject enemy;
    public GameObject exitDoor;
    public GameObject gameStateManagerObject;
    GameStateManager gameStateManager;
    [SerializeField] private GameObject[] doors;
    private bool[] closedDoors;

    // Records if the player can scare or not
    bool canScare = false;
    private bool hasEndKey;
    private int keys;

    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = gameStateManagerObject.GetComponent<GameStateManager>();
        keys = 0;
        hasEndKey = false;
        closedDoors = new bool[doors.Length];
        for (int i = 0; i < closedDoors.Length; i++)
        {
            closedDoors[i] = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckDist(exitDoor))
        {
            if (hasEndKey)
            {
                ShowButtonInst("Press [E] to exit");
            }
            else
            {
                ShowButtonInst("Front door key required");
            }
        }
        else
        {
            bool nearDoor = false;
            for (int i = 0; i < doors.Length; i++)
            {
                if (closedDoors[i] && CheckDist(doors[i]))
                {
                    if (keys > 0)
                    {
                        Debug.Log("Keys");
                        ShowButtonInst("Press [E] to unlock");
                    }
                    else
                    {
                        Debug.Log("No keys");
                        ShowButtonInst("Key required");
                    }
                    nearDoor = true;
                    Debug.Log("Near door");
                    break;
                }
            }
            if (!nearDoor)
            {
                HideButtonInst();
            }
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
        if(hasEndKey)
        {
            exitDoor.GetComponent<EndDoorScript>().OpenDoor();
        }
        if (keys > 0)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                if (closedDoors[i] && CheckDist(doors[i]))
                {
                    doors[i].GetComponent<BasicDoorScript>().OpenDoor();
                    keys--;
                    break;
                }
            }
        }
    }

    public void OnPause(InputValue value)
    {
        gamePauser.GetComponent<GameStateManager>().PauseGame();
    }

    public void ShowButtonInst(string message)
    {
        gamePauser.GetComponent<GameStateManager>().ShowDoorButton(message);
    }

    public void HideButtonInst()
    {
        gamePauser.GetComponent<GameStateManager>().HideDoorButton();
    }

    public void CollectCandy()
    {
        Debug.Log("Candy Collected");
        canScare = true;
    }

    public void CollectKey(KeyType keyType)
    {
        Debug.Log("Key Collected");
        switch (keyType)
        {
            case KeyType.End:
                // Open End door
                hasEndKey = true;
                break;
            case KeyType.Basic:
                // Open a "generic" door
                ++keys;
                break;
        }
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
            case "Key": // Key
                CollectKey(other.gameObject.GetComponent<Key>().Type);
                Destroy(other.gameObject);
                break;
        }
    }

    bool CheckDist(GameObject thing)
    {
        float dist = Mathf.Sqrt(Mathf.Pow(this.gameObject.transform.position.x - thing.transform.position.x, 2) +
            Mathf.Pow(this.gameObject.transform.position.z - thing.transform.position.z, 2));
        return dist < 3.0f;
    }

    public void removeDoor(GameObject openedDoor)
    {
        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i].Equals(openedDoor))
            {
                closedDoors[i] = false;
                break;
            }
        }
    }
}
