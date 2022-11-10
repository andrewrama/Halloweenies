using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candy : MonoBehaviour
{
    bool collected = false;
    int rechargeTimer = 0;
    const int RECHARGE_TIME = 900; // 900 fixed updates which is 15 seconds
    
    MeshRenderer meshRenderer;
    Material opaqueMaterial;
    public Material transparentMaterial;

    // Reference to the player
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerArmature");
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        opaqueMaterial = meshRenderer.material;
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
            //meshRenderer.enabled = true;
            ChangeTransparency(false);
        }
    }

    void ChangeTransparency(bool transparent)
    {
        if (transparent) // Switch it to transparent
        {
            meshRenderer.material = transparentMaterial;
        }
        else // Switch it back to opaque
        {
            meshRenderer.material = opaqueMaterial;
        }
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
            //meshRenderer.enabled = false;
            ChangeTransparency(true);
            return true;
        }
        return false;
    }
}
