using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Keys inventory
    public int BasicKeys
    {
        get
        {
            return basicKeys;
        }
        set
        {
            basicKeys = value;
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

    // Reference to game objects
    public GameObject enemy;
    public GameObject player;
    public GameObject exitDoor;
    public GameObject gameStateManagerObject;
    GameStateManager gameStateManager;
    [SerializeField] private GameObject[] doorsTier1;
    private bool[] closedDoorsTier1;
    [SerializeField] private GameObject[] doorsTier2;
    private bool[] closedDoorsTier2;
    [SerializeField] private GameObject[] doorsTier3;
    private bool[] closedDoorsTier3;

    // HUD
    public GameObject scareTextObject;
    Text scareText;
    string scareTextAvailable;
    string scareTextNotAvailable;
    [SerializeField] private HUDManager HUD;


    // Records if the player can scare or not
    bool canScare = false;
    private bool hasEndKey;
    private int basicKeys;

    // Scare Sounds
    public AudioClip[] scareSounds;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = gameStateManagerObject.GetComponent<GameStateManager>();
        basicKeys = 0;
        hasEndKey = false;
        closedDoorsTier1 = new bool[doorsTier1.Length];
        for (int i = 0; i < closedDoorsTier1.Length; i++)
        {
            closedDoorsTier1[i] = true;
        }

        closedDoorsTier2 = new bool[doorsTier2.Length];
        for (int i = 0; i < closedDoorsTier2.Length; i++)
        {
            closedDoorsTier2[i] = true;
        }

        closedDoorsTier3 = new bool[doorsTier3.Length];
        for (int i = 0; i < closedDoorsTier3.Length; i++)
        {
            closedDoorsTier3[i] = true;
        }

        audioSource = player.GetComponent<AudioSource>();

        // Save the default scare text
        scareText = scareTextObject.GetComponent<Text>();
        scareTextNotAvailable = scareText.text;
        scareTextAvailable = "Scare: READY (Press Spacebar!)";
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
            for (int i = 0; i < doorsTier1.Length; i++)
            {
                if (closedDoorsTier1[i] && CheckDist(doorsTier1[i]))
                {
                    if (basicKeys >= 1)
                    {
                        Debug.Log("Keys");
                        ShowButtonInst("Press [E] to unlock");
                    }
                    else
                    {
                        Debug.Log("No basicKeys");
                        ShowButtonInst("Circle Key required");
                    }
                    nearDoor = true;
                    Debug.Log("Near circle door");
                    break;
                }
            }
            for (int i = 0; i < doorsTier2.Length; i++)
            {
                if (closedDoorsTier2[i] && CheckDist(doorsTier2[i]))
                {
                    if (basicKeys >= 2)
                    {
                        Debug.Log("Keys");
                        ShowButtonInst("Press [E] to unlock");
                    }
                    else
                    {
                        Debug.Log("Not enough basicKeys");
                        ShowButtonInst("Triangle Key required");
                    }
                    nearDoor = true;
                    Debug.Log("Near triangle door");
                    break;
                }
            }
            for (int i = 0; i < doorsTier3.Length; i++)
            {
                if (closedDoorsTier3[i] && CheckDist(doorsTier3[i]))
                {
                    if (basicKeys >= 3)
                    {
                        Debug.Log("Keys");
                        ShowButtonInst("Press [E] to unlock");
                    }
                    else
                    {
                        Debug.Log("Not enough basicKeys");
                        ShowButtonInst("Square Key required");
                    }
                    nearDoor = true;
                    Debug.Log("Near square door");
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
            enemy.GetComponent<Monster>().Spook(GetDist(enemy));

            audioSource.clip = scareSounds[Random.Range(0, 2)];
            audioSource.Play();

            // Remove their ability to scare
            canScare = false;

            // Update the text
            scareText.text = scareTextNotAvailable;

            //Change candy display in HUD
            HUD.SetCandyInvisible();
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
        if (basicKeys >= 1)
        {
            for (int i = 0; i < doorsTier1.Length; i++)
            {
                if (closedDoorsTier1[i] && CheckDist(doorsTier1[i]))
                {
                    doorsTier1[i].GetComponent<BasicDoorScript>().OpenDoor();
                    break;
                }
            }
        }
        if (basicKeys >= 2)
        {
            for (int i = 0; i < doorsTier2.Length; i++)
            {
                if (closedDoorsTier2[i] && CheckDist(doorsTier2[i]))
                {
                    doorsTier2[i].GetComponent<BasicDoorScript>().OpenDoor();
                    break;
                }
            }
        }
        if (basicKeys >= 3)
        {
            for (int i = 0; i < doorsTier3.Length; i++)
            {
                if (closedDoorsTier3[i] && CheckDist(doorsTier3[i]))
                {
                    doorsTier3[i].GetComponent<BasicDoorScript>().OpenDoor();
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
        scareText.text = scareTextAvailable;

        //Change candy display in HUD
        HUD.SetCandyVisible();
    }

    public void CollectKey(KeyType keyType)
    {
        Debug.Log("Key Collected");
        switch (keyType)
        {
            case KeyType.End:
                // Update HUD
                HUD.SetKeyVisible(-1);

                // Open End door
                hasEndKey = true;
                break;
            case KeyType.Basic:
                //Update HUD
                HUD.SetKeyVisible(basicKeys);

                // Open a "generic" door
                ++basicKeys;
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
                // Only trigger code if they don't already have a candy
                if (!canScare) // They can't scare yet so the code should trigger
                {
                    // Make sure you can collect it (its not currently recharging)
                    if (other.gameObject.GetComponent<candy>().Collect()) // You can collect it!
                    {
                        CollectCandy();
                    }
                }
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
        return GetDist(thing) < 3.0f;
    }

    float GetDist(GameObject thing)
    {
        return Mathf.Sqrt(Mathf.Pow(this.gameObject.transform.position.x - thing.transform.position.x, 2) +
            Mathf.Pow(this.gameObject.transform.position.z - thing.transform.position.z, 2));
    }

    public void removeDoor(GameObject openedDoor)
    {
        for (int i = 0; i < doorsTier1.Length; i++)
        {
            if (doorsTier1[i].Equals(openedDoor))
            {
                closedDoorsTier1[i] = false;
                break;
            }
        }
        for (int i = 0; i < doorsTier2.Length; i++)
        {
            if (doorsTier2[i].Equals(openedDoor))
            {
                closedDoorsTier2[i] = false;
                break;
            }
        }
        for (int i = 0; i < doorsTier3.Length; i++)
        {
            if (doorsTier3[i].Equals(openedDoor))
            {
                closedDoorsTier3[i] = false;
                break;
            }
        }
    }
}
