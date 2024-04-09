using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostEffect : MonoBehaviour
{
    public  Material mat;
    public float clipBoost;
    public float clipStand;
    
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
    }
}
