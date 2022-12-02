using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private Image HUDCandyDisplay;
    [SerializeField] private Image[] HUDKeyDisplayImages;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Image keyImage in HUDKeyDisplayImages)
        {
            keyImage.color = Color.black;
        }

        SetCandyInvisible();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCandyVisible()
    {
        HUDCandyDisplay.color = Color.white;
    }
    public void SetCandyInvisible()
    {
        HUDCandyDisplay.color = Color.black;
    }
    public void SetKeyVisible(int keyIndex)
    {
        HUDKeyDisplayImages[keyIndex].color = Color.white;
    }
}
