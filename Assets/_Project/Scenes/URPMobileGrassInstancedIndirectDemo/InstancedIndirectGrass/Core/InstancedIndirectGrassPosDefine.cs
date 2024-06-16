using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancedIndirectGrassPosDefine : MonoBehaviour
{
    [Range(1, 10000000)]
    public int instanceCount = 1000000;
    public float drawDistance = 125;
    public GameObject[] islands;  // Массив объектов островов
    private int cacheCount = -1;

    // Start is called before the first frame update
    void Awake()
    {
    }

    private void Start()
    {
        UpdatePosIfNeeded();
    }

    private void Update()
    {
        //UpdatePosIfNeeded();
    }


    private void UpdatePosIfNeeded()
    {
        if (instanceCount == cacheCount)
            return;

        Debug.Log("UpdatePos (Slow)");

        UnityEngine.Random.InitState(123);

        float scale = Mathf.Sqrt((instanceCount / 4)) / 2f;
        //transform.localScale = new Vector3(250, transform.localScale.y, 250);

        List<Vector3> positions = new List<Vector3>(instanceCount);

        foreach (var island in islands)
        {
            Mesh islandMesh = island.GetComponent<MeshFilter>().sharedMesh;
            Vector3[] vertices = islandMesh.vertices;
            int[] triangles = islandMesh.triangles;
            Transform islandTransform = island.transform;

            for (int i = 0; i < instanceCount / islands.Length; i++)  // распределить количество экземпляров между всеми островами
            {
                Vector3 randomPoint = Vector3.zero;
                int triangleIndex = Random.Range(0, triangles.Length / 3) * 3;
                Vector3 vertex1 = vertices[triangles[triangleIndex]];
                Vector3 vertex2 = vertices[triangles[triangleIndex + 1]];
                Vector3 vertex3 = vertices[triangles[triangleIndex + 2]];
                Vector3 normal = Vector3.Cross(vertex2 - vertex1, vertex3 - vertex1).normalized;

                randomPoint = GetRandomPointInTriangle(vertex1, vertex2, vertex3);
                if (normal.y > 0)
                {
                    Vector3 worldPoint = islandTransform.TransformPoint(randomPoint);
                    positions.Add(worldPoint);
                }
            }
        }

        InstancedIndirectGrassRenderer.instance.allGrassPos = positions;
        cacheCount = positions.Count;
        Debug.Log(positions.Count);
    }

    Vector3 GetRandomPointInTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        float a = Random.value;
        float b = Random.value;

        if (a + b > 1f)
        {
            a = 1f - a;
            b = 1f - b;
        }

        float c = 1f - a - b;

        Vector3 randomPoint = a * v1 + b * v2 + c * v3;
        return randomPoint;
    }
}