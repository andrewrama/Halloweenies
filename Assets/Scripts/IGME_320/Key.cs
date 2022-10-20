using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyType
{
    End,
    Basic
}

public class Key : MonoBehaviour
{
    public KeyType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }
    // Reference to the player
    [SerializeField] private GameObject player;
    [SerializeField] private KeyType type;
}
