using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public enum Habilities
{
    Fire, Speed, Bomb
}
public class birdCollection : MonoBehaviour
{
    [Header("Estatisticas Gerais")]
    public int birdsCount;
    public int specieLevel;
    public float specieLife;
    [SerializeField] private float AttackRadius;
    [SerializeField] private LayerMask AllbirdsMask;

    [Header("Estatisticas da Espécie")]
    public string Specie_Name;
    public int Specie_strenght, specie_reprodution, specie_hability_force, specie_speed;
    public Habilities habilidadeEspecial;
    public Color corEspecie;


    [Header("Resources")]
    [SerializeField]private GameObject birdPrefab;
    [SerializeField]private GameObject[] birdsSpawned;
    public GameObject birdOptions;
    public bool selectedToMove, selectedToAttack, moving,attacking;
    private Vector3 target;

    void Start()
    {
        birdsCount = Random.Range(100, 150);
        specieLevel = 1;
        specieLife = 1000;
        AttackRadius = 20f;

        for (int i = 0; i < birdsCount / 10; ++i)//spawna passaros pela qtd
        {
            GameObject birds = Instantiate(birdPrefab, new Vector3(transform.position.x + Random.Range(i,5), transform.position.y, transform.position.z + Random.Range(i,5)),Quaternion.identity); // spawna aves rotacionando elas
            birds.transform.parent = transform;
            birds.GetComponent<MeshRenderer>().material.color = corEspecie;
            
        }

        StartCoroutine(FindEnemiesWithDelay(2f));

    }
    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, specie_speed * Time.deltaTime);

            if(transform.position == target)
            {
                if (attacking)
                {
                    print(gameObject.name + " atacando");
                    moving = false;
                }
                else
                {
                    moving = false;
                    print(gameObject.name + " chegou no destino");
                }

            }
        }


    }

    private void FixedUpdate()
    {
        if (birdOptions.gameObject.activeInHierarchy)
        {
            birdOptions.transform.rotation = Quaternion.LookRotation(birdOptions.transform.position - Camera.main.transform.position);//clamp a rotacao do canvas das opcoes
        }
    }

    #region BirdOptions
    public void ToggleMove()
    {
        if (!moving && !selectedToMove)
        {
            print(gameObject.name + " IsSelected");;
            birdOptions.gameObject.SetActive(false);
            selectedToMove = true;
        }
    }
    public void ToggleAttack()
    {
        if (!moving && !selectedToMove)
        {
            print(gameObject.name + " selectedToAttack");
            birdOptions.gameObject.SetActive(false);
            selectedToAttack = true;
        }
    }
    public void AttackEnemie(Vector3 positionToAttack)//attack from btn
    {
        print(gameObject.name + " moving to attack");
        MoveBirds(positionToAttack);
        attacking = true;
    }
    #endregion
    public void MoveBirds(Vector3 pos)
    {
        target = pos;
        moving = true;
    }


    #region AttackBirds

    IEnumerator FindEnemiesWithDelay(float cooldown)
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            FindEnemiesOnRadius();
        }
    }

    private void FindEnemiesOnRadius()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, AttackRadius, AllbirdsMask);

        for(int i = 0; i< targets.Length; i++)
        {
            Transform targetTransform = targets[i].transform;

            print("contato" + targets[i].gameObject.name);
        }
    }

    #endregion

}
