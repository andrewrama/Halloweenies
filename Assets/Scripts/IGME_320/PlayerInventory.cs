using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
    public int Keys
    {
        get
        {
            return keys;
        }
        set
        {
            keys = value;
        }
    }
    private int keys;
    // Start is called before the first frame update
    void Start()
    {
        keys = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
