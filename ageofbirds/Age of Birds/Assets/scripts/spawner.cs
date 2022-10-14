using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawner : MonoBehaviour
{
    public GameObject[] points;
    public GameObject[] armadilhapoints;
    public GameObject[] armadilhas;
    public GameObject water, bushes;
    public float DelayToNextSpawn = 1f;
    public float spawnColectable = 0.3f;
    public float spawnArmadilha = 2;

    public float xMin,xMan;
    public float yMin,yMan;

    public bool Offline = false;
    void Update()
    {
        spawnColectable -= Time.deltaTime;
        spawnArmadilha -= Time.deltaTime;
        if(spawnColectable <= 0)
        {
            Spawn();
            spawnColectable = DelayToNextSpawn;
        }
        if (spawnArmadilha <= 0 && !PhotonNetwork.IsConnected)
        {
            SpawnArmadilha();
            spawnArmadilha = 6;
        }
    }

    public void Spawn()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (Random.Range(0, 10) <= 5)
            {
                Vector2 pos = new Vector2(Random.Range(xMin, xMan), Random.Range(yMin, yMan));
                GameObject SpawnPoint = points[Random.Range(0, points.Length)];
                PhotonNetwork.Instantiate(water.name, SpawnPoint.transform.position, Quaternion.identity);
            }
            else
            {
                Vector2 pos2 = new Vector2(Random.Range(xMin, xMan), Random.Range(yMin, yMan));
                GameObject SpawnPoint2 = points[Random.Range(0, points.Length)];
                PhotonNetwork.Instantiate(bushes.name, SpawnPoint2.transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (Random.Range(0, 10) <= 5)
            {
                Vector2 pos = new Vector2(Random.Range(xMin, xMan), Random.Range(yMin, yMan));
                GameObject SpawnPoint = points[Random.Range(0, points.Length)];
                Instantiate(water, SpawnPoint.transform.position, Quaternion.identity);
            }
            else
            {
                Vector2 pos2 = new Vector2(Random.Range(xMin, xMan), Random.Range(yMin, yMan));
                GameObject SpawnPoint2 = points[Random.Range(0, points.Length)];
                Instantiate(bushes, SpawnPoint2.transform.position, Quaternion.identity);
            }
        }
        
       

       
    }

    public void SpawnArmadilha()
    {
        Vector2 pos3 = new Vector2(Random.Range(xMin, xMan), Random.Range(yMin, yMan));
        GameObject SpawnPoint3 = armadilhapoints[Random.Range(0, armadilhapoints.Length)];
        Instantiate(armadilhas[Random.Range(0, armadilhas.Length)], SpawnPoint3.transform.position, Quaternion.identity);
    }
}
