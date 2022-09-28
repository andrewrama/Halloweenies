using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movement : MonoBehaviour
{
    // VARIABLES:

    // Get a reference to the nav mesh agent component on the enemy
    public NavMeshAgent agent;

    // Get a reference to the player
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // Update target position to be the player's position
        agent.SetDestination(player.transform.position);
    }
}
