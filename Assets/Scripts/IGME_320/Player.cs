using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Keys inventory
    public bool HasTier1Key
    {
        get
        {
            return hasTier1Key;
        }
        set
        {
            hasTier1Key = value;
        }
    }
    public bool HasTier2Key
    {
        get
        {
            return hasTier2Key;
        }
        set
        {
            hasTier2Key = value;
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

    // HUD
    public GameObject scareTextObject;
    Text scareText;
    string scareTextAvailable;
    string scareTextNotAvailable;
    [SerializeField] private HUDManager HUD;


    // Records if the player can scare or not
    bool canScare = false;
    private bool hasTier1Key;
    private bool hasTier2Key;
    private bool hasEndKey;

    // Scare Sounds
    public AudioClip[] scareSounds;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameStateManager = gameStateManagerObject.GetComponent<GameStateManager>();
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
                ShowButtonInst("Square key required");
            }
        }
        else
        {
            bool nearDoor = false;
            for (int i = 0; i < doorsTier1.Length; i++)
            {
                if (closedDoorsTier1[i] && CheckDist(doorsTier1[i]))
                {
                    if (hasTier1Key)
                    {
                        Debug.Log("Keys");
                        ShowButtonInst("Press [E] to unlock");
                    }
                    else
                    {
                        Debug.Log("No triangle key");
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
                    if (hasTier2Key)
                    {
                        Debug.Log("Keys");
                        ShowButtonInst("Press [E] to unlock");
                    }
                    else
                    {
                        Debug.Log("No triangle key");
                        ShowButtonInst("Triangle Key required");
                    }
                    nearDoor = true;
                    Debug.Log("Near triangle door");
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

            audioSource.clip = scareSounds[Random.Range(0, scareSounds.Length)];
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
        if (hasTier1Key)
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
        if (hasTier2Key)
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
            case KeyType.Tier1:
                //Update HUD to show key
                HUD.SetKeyVisible(0);

                // You can open a "circle" door
                hasTier1Key = true;
                break;
            case KeyType.Tier2:
                //Update HUD
                HUD.SetKeyVisible(1);

                // You can open a "triangle" door
                hasTier2Key = true;
                break;
            case KeyType.End:
                // Update HUD
                HUD.SetKeyVisible(2);

                // You can open the End door
                hasEndKey = true;
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

    public float GetDist(GameObject thing)
    {
        return (
            Mathf.Sqrt(
                Mathf.Pow(this.gameObject.transform.position.x - thing.transform.position.x, 2) + // x
                Mathf.Pow(this.gameObject.transform.position.z - thing.transform.position.z, 2) // Y
            ) /*+
            gameObject.GetComponent<CapsuleCollider>().radius + // Radius 1
            thing.GetComponent<CapsuleCollider>().radius // Radius 2*/
        ) ;
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
    }
}
