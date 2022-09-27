using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawner : MonoBehaviour
{
    public GameObject[] points;
    public GameObject water, bushes;
    public float DelayToNextSpawn = 1f;
    public float StartWait = 0.2f;

    public float xMin,xMan;
    public float yMin,yMan;
    void Update()
    {
        StartWait -= Time.deltaTime;
        if(StartWait <= 0)
        {
            Spawn();
            StartWait = DelayToNextSpawn;
        }
    }

    public void Spawn()
    {
        Vector2 pos = new Vector2(Random.Range(xMin, xMan), Random.Range(yMin, yMan));
        GameObject SpawnPoint = points[Random.Range(0, points.Length)];
        PhotonNetwork.Instantiate(water.name, SpawnPoint.transform.position, Quaternion.identity);

        Vector2 pos2 = new Vector2(Random.Range(xMin, xMan), Random.Range(yMin, yMan));
        GameObject SpawnPoint2 = points[Random.Range(0, points.Length)];
        PhotonNetwork.Instantiate(bushes.name, SpawnPoint.transform.position, Quaternion.identity);
    }
}
