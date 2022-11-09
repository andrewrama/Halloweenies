using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candy : MonoBehaviour
{
    bool collected = false;
    int rechargeTimer = 0;
    const int RECHARGE_TIME = 900; // 900 fixed updates which is 15 seconds
    MeshRenderer meshRenderer;

    // Reference to the player
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerArmature");
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        updateRecharge();
    }

    void updateRecharge()
    {
        // Decrement recharge by 1 if its above 0
        if (rechargeTimer > 0) // It is currently recharging
        {
            rechargeTimer -= 1;
        }
        else if (collected && rechargeTimer == 0) // This is the last frame of it recharging
        {
            // Respawn
            collected = false;
            meshRenderer.enabled = true;
            //ChangeTransparency(1);
        }
    }

    void ChangeTransparency(float a)
    {
        Color color = meshRenderer.material.color;
        color.a = a;
        meshRenderer.material.color = color;
    }

    // Called when the player walks into the candy
    // Returns true if the player should collect the candy
    public bool Collect()
    {
        // only execute the script if its not collected already
        if (!collected) // It is not already collected
        {
            collected = true;
            rechargeTimer = RECHARGE_TIME;
            meshRenderer.enabled = false;
            //ChangeTransparency(0);
            return true;
        }
        return false;
    }
}
