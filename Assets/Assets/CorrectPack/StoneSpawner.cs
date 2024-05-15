using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public float boundX = 100;
    public float boundY = 50f;
    public float boundZ = 10;
    public GameObject player;
    public GameObject[] stones;
    public GameObject stonePortal;
    public Vector3 offsetPortal = new Vector3(0,0,0);
    public float timeToSpawn;
    public bool isSpawn = true;
    public bool isSpawnStone = true;
    int indexRandom;
    float boundVectorX;
    float randomAngleX;
    float randomAngleY;
    float randomAngleZ;
    float randomAnglePort;
    
    void FixedUpdate()
    {
        StoneSpawn();
    }

    public void StoneSpawn()
    {
        if(isSpawnStone)
        {
            int indexRandom = Random.Range(0, stones.Length);
            boundVectorX = Random.Range(boundX, -boundX);
            randomAngleX = Random.Range(-180, 180);
            randomAngleY = Random.Range(-180, 180);
            randomAngleZ = Random.Range(-180, 180);
            randomAnglePort = Random.Range(30, -30);
            Vector3 pos = new Vector3(boundVectorX + player.transform.position.x, boundY, player.transform.position.z + boundZ);
            Instantiate(stonePortal, pos,Quaternion.Euler(190, 0, randomAnglePort));
            StartCoroutine(SpawnActivater(stones[indexRandom], pos, Quaternion.Euler(randomAngleX, randomAngleY, randomAngleZ)));
            isSpawnStone = false;
        }
    }


    IEnumerator SpawnActivater(GameObject go, Vector3 pos, Quaternion rot)
    {
        yield return new WaitForSeconds(1f);
        SpawnStone(go, pos, rot);
        isSpawnStone = true;
    }

    public void SpawnStone(GameObject go, Vector3 pos, Quaternion rot)
    {
        Instantiate(go, pos, rot);
    }
}   
