using System.Collections;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public float boundX = 100;
    public float boundY = 50f;
    public float boundZ = 10;
    public GameObject player;
    public GameObject[] stones;
    public GameObject stonePortal;
    public Vector3 offsetPortal = new Vector3(0, 0, 0);
    public float timeToSpawn;
    public bool isSpawn = true;
    public bool isSpawnStone = true;
    int indexRandom;
    float boundVectorX;
    float randomAngleX;
    float randomAngleY;
    float randomAngleZ;
    float randomAnglePort;
    public GameObject pointToSpawn;
    public bool isEnabled;

    void FixedUpdate()
    {
        StoneSpawn();
    }

    public void StoneSpawn()
    {
        if (isSpawnStone && isEnabled)
        {
            int indexRandom = Random.Range(0, stones.Length);
            boundVectorX = Random.Range(boundX, -boundX);
            randomAngleX = Random.Range(-180, 180);
            randomAngleY = Random.Range(-180, 180);
            randomAngleZ = Random.Range(-180, 180);
            randomAnglePort = Random.Range(30, -30);
            Vector3 pos = new Vector3(pointToSpawn.transform.position.x, player.transform.position.y + boundY, pointToSpawn.transform.position.z);
            //Destroy(Instantiate(stonePortal, pos,Quaternion.Euler(100, 0, randomAnglePort)), timeToSpawn);
            StartCoroutine(SpawnActivater(stones[indexRandom], pos, stones[indexRandom].transform.rotation));
            //Quaternion.Euler(randomAngleX, randomAngleY, randomAngleZ)));
            isSpawnStone = false;
        }
    }


    IEnumerator SpawnActivater(GameObject go, Vector3 pos, Quaternion rot)
    {
        yield return new WaitForSeconds(timeToSpawn);
        SpawnStone(go, pos, rot);
        isSpawnStone = true;
    }

    public void SpawnStone(GameObject go, Vector3 pos, Quaternion rot)
    {
        Destroy(Instantiate(go, pos, rot), 4f);
    }
}
