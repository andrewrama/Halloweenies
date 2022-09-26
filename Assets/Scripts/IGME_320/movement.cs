using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // VARIABLES:

    // Update to point towards the player every frame
    public Vector3 direction = Vector3.right;

    // How far it moves in a second
    public float speed = 100;

    // How many seconds pass every frame (much less than 1)
    float timeStep;

    // Start is called before the first frame update
    void Start()
    {
        // Update time step once rather than calling it every frame
        timeStep = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // TODO: Calculate direction
        direction = direction.normalized;

        // Calculate distance to move
        float distance = speed * timeStep;

        // Move the fella
        // TODO: Don't move through walls pls
        gameObject.transform.position = gameObject.transform.position + direction * distance;
    }
}
