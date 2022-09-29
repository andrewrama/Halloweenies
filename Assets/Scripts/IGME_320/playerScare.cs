using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerScare : MonoBehaviour
{
    // Reference to the enemy
    public GameObject enemy;

    // Scare cooldown ability
    int scareCooldownTimer = 0; // How many fixedUpdate()s until they can use scare again
    public int SCARE_COOLDOWN = 600; // How long it will take to recharge

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateScare();
    }

    public void OnScare(InputValue value)
    {
        // Test if they can use it
        if (scareCooldownTimer == 0) // It is off cooldown
        {
            // Scare the enemy
            enemy.GetComponent<movement>().Spook();

            // Set it on cooldown
            scareCooldownTimer = SCARE_COOLDOWN;
        }
    }

    void UpdateScare()
    {
        // Decrease the scared timer down by 1 to 0
        if (scareCooldownTimer > 0) // They are still scared
        {
            scareCooldownTimer -= 1;
        }
    }
}
