using UnityEngine;

public class GoundCreater : MonoBehaviour
{
    public GameObject[] go;
    public Vector3[] offsetPos;
    public GameObject[] posTriggers;
    public GameObject player;

    public int currentIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            go[currentIndex].transform.position = go[currentIndex].transform.position + offsetPos[currentIndex];
            for(int i = 0; i < go.Length; i++)
            {
                float dist = Vector3.Distance(player.transform.position, go[i].transform.position);
                if(dist > 300)
                {
                    go[i].SetActive(false);
                }
                if(dist <= 300)
                {
                    go[i].SetActive(true);
                }
            }
            }
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

