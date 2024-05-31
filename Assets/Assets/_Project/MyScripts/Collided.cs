using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collided : MonoBehaviour
{
    public GameObject canvasWarningImage;
    public StoneSpawner stoneSpawner;
    private void OnTriggerEnter(Collider other) 
    {
        canvasWarningImage.SetActive(true);
        Destroy(canvasWarningImage, 60f);
        other.gameObject.GetComponentInChildren<StoneSpawner>().isEnabled = true;
    }
}
