using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public Material material;
    public Transform playerPos;
    public Vector3 pos;


    void Update()
    {
        if (playerPos == null)
        {
            playerPos = FindObjectOfType<CactusDestroer>().gameObject.transform;
        }
        if (playerPos != null)
        {
            material.SetVector("_PlayerPos", playerPos.position);
        }
    }
}
