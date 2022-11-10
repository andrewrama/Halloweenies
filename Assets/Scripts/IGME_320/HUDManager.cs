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

        HUDCandyDisplay.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCandyVisible()
    {
        HUDCandyDisplay.enabled = true;
    }
    public void SetCandyInvisible()
    {
        HUDCandyDisplay.enabled = false;
    }
    public void SetKeyVisible(int keyIndex)
    {

        switch(keyIndex)
        {
            case 0:
                // set first key image here
                // HUDKeyDisplayImages[keyIndex].sprite = sourceImg;
                break;
            case 2:
                // set end key image here
                // HUDKeyDisplayImages[HUDKeyDisplayImages.Length - 1].sprite = sourceImg;
                HUDKeyDisplayImages[HUDKeyDisplayImages.Length - 1].color = Color.white;
                return;
        }

        HUDKeyDisplayImages[keyIndex].color = Color.white;

    }
}
