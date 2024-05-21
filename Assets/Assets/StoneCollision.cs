using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCollision : MonoBehaviour
{

    public float raycastDistance;
    public Transform raycastPoint;
    public float speed;
    public Material mat;
    public GameObject flame;
    public GameObject secondFlame;
    MeshRenderer meshRenderer;
    Rigidbody rb;

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();   
        meshRenderer = flame.GetComponent<MeshRenderer>(); 
        mat = secondFlame.GetComponent<MeshRenderer>().material;
    }
    void Update()
    {
        Raycast();
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void Raycast()
    {
        RaycastHit hit;
        if(Physics.Raycast(raycastPoint.position, -Vector3.up, out hit, raycastDistance, 1<<3))
        {
            meshRenderer.material = mat;
            float h =  mat.GetFloat("_Height");
            speed = 0;
            h -= 1f * Time.deltaTime;
            mat.SetFloat("_Height", h);
        }

    }
}
