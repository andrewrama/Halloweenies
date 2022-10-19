using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyType
{
    End,
    Basic
}

public class Key : MonoBehaviour
{
    // Reference to the player
    [SerializeField] private GameObject player;
    public KeyType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }
    [SerializeField] private KeyType type;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Key collision detected");
        if (other.gameObject == player)
        {
            // Tell the player that they got a candy
            player.GetComponent<Player>().Keys++;

            // Destroy this candy
            Destroy(gameObject);
        }
    }
}
