using UnityEngine;

public class GoundCreater : MonoBehaviour
{
    public GameObject[] go;
    public Vector3[] offsetPos;
    public GameObject[] posTriggers;

    public int currentIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            go[currentIndex].transform.position = go[currentIndex].transform.position + offsetPos[currentIndex];
            currentIndex++;
            if (currentIndex <= 2)
            {
                transform.position = posTriggers[currentIndex].transform.position;
            }
            if (currentIndex >= 3)
            {
                currentIndex = 0;
                transform.position = posTriggers[currentIndex].transform.position;
            }
        }
    }
}
