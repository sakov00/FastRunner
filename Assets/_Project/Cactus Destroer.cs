using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusDestroer : MonoBehaviour
{
    public LayerMask layer;
    public GameObject splashPref;
    public ParticleSystem psSplash; 

    private void Start() 
    {
        if(psSplash != null)
        {
            splashPref = FindObjectOfType<PsSplash>().gameObject;
            psSplash = splashPref.GetComponent<ParticleSystem>(); 
        }   
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);
            if(psSplash != null)
            {
                splashPref.transform.position = other.transform.position + new Vector3(0, 4, 0);
                psSplash.Play();
            }
        }   
    }
}
