using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    // VARIABLES:

    // References
    public NavMeshAgent agent; // Reference to the agent component
    public GameObject player; // Reference to the player

    public GameObject angryEyebrows;
    public GameObject scaredEyebrows;

    // Fear variables
    bool scared = false; // If they are afraid and running away from the player
    int scaredTimer = 0; // Keeps track of how many fixedUpdate()s until its not scared anymore
    const int SCARED_TIME = 300; // How long it takes the enemy for them to not be scared anymore
    const float SCARED_SMALL_STEP = 0.5f; // Arbitrarilly small distance that should be less than the thickness of the walls
    Vector3 directionAwayFromPlayer; // Set when it is spooked
    float originalSpeed; // Records the original speed of the monster to be set back once its no longer scared
    public const float SCARED_SPEED = 0.2f; // Should be very small

    // Start is called before the first frame update
    void Start()
    {
        // Record the original speed of the agent
        originalSpeed = agent.speed;

        // Start with only the angry eyebrows visible
        angryEyebrows.SetActive(true);
        scaredEyebrows.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateScared();
        Move();
    }

    void Move()
    {
        // Check if they're scared of the player or not
        if (!scared) // They are not scared of the player and should be moving towards them
        {
            agent.SetDestination(player.transform.position);
        }
        else // They are scared of the player and should be moving away from them
        {
            // Move the enemy in the oposite direction of the player
            transform.position += directionAwayFromPlayer * SCARED_SPEED;
        }
    }

    // Called every frame
    void UpdateScared()
    {
        // Decrease the scared timer down by 1 to 0
        if (scaredTimer > 0) // They are still scared
        {
            scaredTimer -= 1;
        }
        else if (scared && scaredTimer == 0) // This is the last frame of them being scared
        {
            agent.speed = originalSpeed;
            scared = false;
            angryEyebrows.SetActive(true);
            scaredEyebrows.SetActive(false);
            Debug.Log("Monster no longer scared");
        }
    }

    public void Spook()
    {
        directionAwayFromPlayer = (transform.position - player.transform.position).normalized;
        scaredTimer = SCARED_TIME;
        scared = true;
        agent.speed = 0;
        angryEyebrows.SetActive(false);
        scaredEyebrows.SetActive(true);
        Debug.Log("Monster scared");
    }
}
