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
    public string Specie_Name;
    public int Specie_strenght, specie_reprodution, specie_hability_force, specie_speed;
    public Habilities habilidadeEspecial;
    public Color corEspecie;


    [Header("Resources")]
    [SerializeField]private GameObject birdPrefab;
    [SerializeField]private GameObject[] birdsSpawned;
    public GameObject birdOptions;
    public bool isSelected, moving;
    private Vector3 target;

    void Start()
    {
        birdsCount = Random.Range(100, 150);
        specieLevel = 1;

        for (int i = 0; i < birdsCount / 10; ++i)//spawna passaros pela qtd
        {
            GameObject birds = Instantiate(birdPrefab, new Vector3(transform.position.x + Random.Range(i,5), transform.position.y, transform.position.z + Random.Range(i,5)),Quaternion.identity); // spawna aves rotacionando elas
            birds.transform.parent = transform;
            birds.GetComponent<MeshRenderer>().material.color = corEspecie;
            
        }

    }
    void Update()
    {
        birdOptions.transform.rotation = Quaternion.LookRotation(birdOptions.transform.position - Camera.main.transform.position);//clamp a rotacao do canvas de opceos

        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, specie_speed * Time.deltaTime);

            if(transform.position == target)
            {
                moving = false;
                print("destino");
            }
        }
    }


    #region BirdOptions
    public void ToggleMove()
    {
        if (!moving && !isSelected)
        {
            print("IsSelected");
            birdOptions.gameObject.SetActive(false);
            isSelected = true;
        }
    }
    #endregion
    public void MoveBirds(Vector3 pos)
    {
        target = pos;
        moving = true;
    }
}
