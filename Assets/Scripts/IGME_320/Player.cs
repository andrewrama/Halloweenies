using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Keys inventory
    [SerializeField] private GameObject keyPrefab;
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

    // Reference to the enemy
    public GameObject enemy;

    // Records if the player can scare or not
    bool canScare = false;

    // Start is called before the first frame update
    void Start()
    {
        keys = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

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

    public void CollectCandy()
    {
        Debug.Log("Candy Collected");
        canScare = true;
    }
}
