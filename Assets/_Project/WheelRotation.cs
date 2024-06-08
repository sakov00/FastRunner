using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public float rotationSpeed;
    private void Update() 
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed);  
    }
}
