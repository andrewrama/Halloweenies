using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Reference to the player
    [SerializeField] private GameObject player;
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
