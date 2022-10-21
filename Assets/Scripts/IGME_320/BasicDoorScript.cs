using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDoorScript : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CheckDist()
    {
        float dist = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - door.transform.position.x, 2) +
            Mathf.Pow(player.transform.position.z - door.transform.position.z, 2));
        return dist < 3.0f;
    }

    public void OpenDoor()
    {
        if (CheckDist())
        {
            Debug.Log("Text: the door is open");
            player.GetComponent<Player>().removeDoor(gameObject);
            Destroy(gameObject);
        }
    }
}
