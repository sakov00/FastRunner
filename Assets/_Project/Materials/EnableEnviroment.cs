using System.Collections;
using UnityEngine;

public class EnableEnviroment : MonoBehaviour
{
    public GameObject[] enviromnets;
    public Material mat;
    void Awake()
    {
        StartCoroutine(Timer());
    }

    private void Start()
    {
        mat.SetVector("_Pos", new Vector3(0, 0, 0));
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.01f);
        foreach (GameObject env in enviromnets)
        {
            env.SetActive(true);
        }
    }
}
