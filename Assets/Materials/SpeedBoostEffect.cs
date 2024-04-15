using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostEffect : MonoBehaviour
{
    public  Material mat;
    public float clipBoost;
    public float clipStand;
    public ParticleSystem ps;
    public CharacterController ch;
    
    void Start()
    {
        mat.SetFloat("_clip", clipStand);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            mat.SetFloat("_clip", clipBoost);
        }
        else{
            mat.SetFloat("_clip", clipStand);
        }
        if(!ch.isGrounded)
        {
            ps.Play();
            Debug.Log("стою же");
        }
    }
}
