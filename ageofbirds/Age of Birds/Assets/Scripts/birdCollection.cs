using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Habilities
{
    Fire, Speed, Bomb
}
public class birdCollection : MonoBehaviour
{
    [Header("Estatisticas Gerais")]
    public int birdsCount;
    public int specieLevel;

    [Header("Estatisticas da Espécie")]
    public int strenght;
    public Habilities habilidadeEspecial;

    [Header("Resources")]
    [SerializeField]private GameObject birdPrefab;
    [SerializeField]private GameObject[] birdsSpawned;
    public GameObject birdOptions;

    void Start()
    {
        birdsCount = Random.Range(100, 150);
        specieLevel = 1;
        strenght = 5;

        for (int i = 0; i < birdsCount / 10; ++i)
        {
            GameObject birds = Instantiate(birdPrefab, new Vector3(transform.position.x + Random.Range(i,5), transform.position.y, transform.position.z + Random.Range(i,5)),Quaternion.identity); // spawna aves rotacionando elas
            birds.transform.parent = transform;
            
        }


    }
    void Update()
    {
        birdOptions.transform.rotation = Quaternion.LookRotation(birdOptions.transform.position - Camera.main.transform.position);//clamp a rotacao do canvas de opceos
    }

    public void MoveTo()
    {

    }
}
