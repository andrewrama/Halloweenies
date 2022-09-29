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

    // If they are afraid and running away from the player
    bool scared = false;

    // Keeps track of how many fixedUpdate()s until its not scared anymore
    int scaredTimer = 0;

    // How long it takes the enemy for them to not be scared anymore
    const int SCARED_TIME = 300;

    // Distance away that the monster will try to run from the player
    const int SCARE_RUN_DISTANCE = 5;


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
        // Update the target position

        // Check if they're scared of the player or not
        if (!scared) // They are not scared of the player and should be moving towards them
        {
            agent.SetDestination(player.transform.position);
        }
        else // They are scared of the player and should be moving away from them
        {
            // Calculate a new target position that is in the oposite direction of the player
            Vector3 directionAwayFromPlayer = player.transform.position - transform.position;
            Vector3 targetLocation = directionAwayFromPlayer.normalized * SCARE_RUN_DISTANCE;
            agent.SetDestination(targetLocation);
        }
    }

    // Called every frame
    void UpdateScared()
    {
        // Decrease the scared timer down by 1 to 0
        if (scaredTimer > 0)
        {
            scaredTimer -= 1;
        }

        // Check if its still scared
        scared = scaredTimer > 0;
    }

    void Spook()
    {
        scaredTimer = SCARED_TIME;
        scared = true;
    }
}
