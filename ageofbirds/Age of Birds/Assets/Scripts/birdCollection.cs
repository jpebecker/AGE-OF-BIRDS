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
    public GameObject birdOptions;

    void Start()
    {
        birdsCount = Random.Range(100, 150);
        specieLevel = 1;
        strenght = 5;
    }
    void Update()
    {
        birdOptions.transform.rotation = Quaternion.LookRotation(birdOptions.transform.position - Camera.main.transform.position);//clamp a rotacao do canvas de opceos
    }
}
