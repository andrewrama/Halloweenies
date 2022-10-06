using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candy : MonoBehaviour
{
    // Reference to the player
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerArmature");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected");
        if(other.gameObject == player)
        {
            // Tell the player that they got a candy
            player.GetComponent<playerScare>().CollectCandy();

            // Destroy this candy
            Destroy(gameObject);
        }
    }
}
