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
    public GameObject enemy;

    public GameObject angryEyebrows;
    public GameObject scaredEyebrows;

    // Fear variables
    bool scared = false; // If they are afraid and running away from the player
    int scaredTimer = 0; // Keeps track of how many fixedUpdate()s until its not scared anymore
    public const int MAX_SCARED_TIME = 360; // 6 seconds
    public const int MIN_SCARED_TIME = 120; // 2 Seconds
    const float SCARED_SMALL_STEP = 0.5f; // Arbitrarilly small distance that should be less than the thickness of the walls
    Vector3 directionAwayFromPlayer; // Set when it is spooked
    float originalSpeed; // Records the original speed of the monster to be set back once its no longer scared
    public const float SCARED_SPEED = 0.2f; // Should be very small
    public const float MAX_SCARE_DISTANCE = 6.0f;

    // Audio
    public AudioClip[] scaredSounds;
    public AudioClip[] roamingSounds;
    public AudioClip walkingSound;
    AudioSource audioSource;
    const int ROAMING_NOISE_TIME = 300;
    int noiseTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Record the original speed of the agent
        originalSpeed = agent.speed;

        // Start with only the angry eyebrows visible
        angryEyebrows.SetActive(true);
        scaredEyebrows.SetActive(false);

        audioSource = enemy.GetComponents<AudioSource>()[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateScared();
        UpdateRoamAudio();
        Move();
    }

    void Move()
    {
        // Check if they're scared of the player or not
        if (!scared) // They are not scared of the player and should be moving towards them
        {
            agent.SetDestination(player.transform.position);
            //PlayWalkingSound();
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

    void UpdateRoamAudio()
    {
        if(!scared && noiseTimer > 0)
        {
            noiseTimer -= 1;
        }
        else if(noiseTimer == 0)
        {
            PlayRoamAudio();
        }
    }

    void PlayRoamAudio()
    {
        audioSource.clip = roamingSounds[Random.Range(0, 3)];
        audioSource.loop = false;
        audioSource.Play();
        noiseTimer = ROAMING_NOISE_TIME;
    }

    //void PlayWalkingSound()
    //{
    //    if (!audioSource.isPlaying)
    //    {
    //        audioSource.clip = walkingSound;
    //        audioSource.loop = true;
    //        audioSource.Play();
    //    }
    //}

    public void Spook(float dist)
    {
        // Check if the monster is in range
        if (dist <= MAX_SCARE_DISTANCE)
        {
            // Calculate how long it should be scared (somewhere between min and max proportionally to how close the monster is)
            scaredTimer += MIN_SCARED_TIME + (int)((MAX_SCARED_TIME - MIN_SCARED_TIME) * (dist / MAX_SCARE_DISTANCE));

            Debug.Log("Monster Scared. scaredTimer = " + scaredTimer);

            directionAwayFromPlayer = (transform.position - player.transform.position).normalized;
            scared = true;
            agent.speed = 0;
            angryEyebrows.SetActive(false);
            scaredEyebrows.SetActive(true);

            // Play scared sound
            audioSource.clip = scaredSounds[Random.Range(0, 3)];
            audioSource.loop = false;
            audioSource.Play();
        }

        Debug.Log("Monster scared. Dist = " + dist);
    }
}
