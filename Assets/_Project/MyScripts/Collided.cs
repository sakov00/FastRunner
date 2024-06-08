using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collided : MonoBehaviour
{
    public GameObject canvasWarningImage;
    public StoneSpawner stoneSpawner;
    public GameObject mapPart;
    public GameObject activePart;
    public bool isActive;
    private void OnTriggerEnter(Collider other) 
    {   
        if(!isActive)
        {
            if(canvasWarningImage != null)
            canvasWarningImage.SetActive(true);
            Destroy(canvasWarningImage, 3f);
            other.gameObject.GetComponentInChildren<StoneSpawner>().isEnabled = true;
            isActive = true;
            if(activePart != null)
            activePart.SetActive(true);
        }
        //mapPart.SetActive(false);
        
    }
}
