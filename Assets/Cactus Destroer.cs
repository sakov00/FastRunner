using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusDestroer : MonoBehaviour
{
    public LayerMask layer;
    public GameObject splashPref;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);
            Instantiate(splashPref, other.transform.position + new Vector3(0, 4, 0), splashPref.transform.rotation);
        }   
    }
}
