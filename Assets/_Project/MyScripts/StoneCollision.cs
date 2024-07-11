using UnityEngine;

public class StoneCollision : MonoBehaviour
{

    public float raycastDistance;
    public Transform raycastPoint;
    public float speed;
    public GameObject spotPS;
    public GameObject psDust;
    public bool hitG;


    void Update()
    {
        Raycast();
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void Raycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastPoint.position, -Vector3.up, out hit, raycastDistance, 1 << 3))
        {
            speed = 0;
            psDust.SetActive(true);
        }
        if (Physics.Raycast(raycastPoint.position, -Vector3.up, out hit, 1000, 1 << 3) && !hitG)
        {
            Vector3 hitNormal = hit.normal;
            float angle = Vector3.Angle(Vector3.up, hitNormal);
            Vector3 cross = Vector3.Cross(Vector3.up, hitNormal);
            Quaternion targetRot = Quaternion.AngleAxis(angle, cross);
            Destroy(Instantiate(spotPS, hit.point + new Vector3(0, 0.5f, 0), targetRot), 1f);
            hitG = true;
        }

    }
}
