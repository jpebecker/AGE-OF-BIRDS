using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bird : MonoBehaviour
{
    [Header("Configuracoes")]
    public float movementSpeed = 5f;
    public float birdPopulation = 100;
    public float birdLevel = 1;
    public float Xp = 0;
    public float life = 100;
    public float increaseRate;
    public float reprodutionSpeed = 2;//em segundos
    [Header("UI")]
    public Text levelTxt;
    public Text NickTxt;

    private Vector3 target;
    private float timerReprodution;
    void Start()
    {
        target = transform.position;
        levelTxt.text = "Level " + birdLevel.ToString();
    }
    void Update()
    {
        timerReprodution += Time.deltaTime;
        if (timerReprodution >= reprodutionSpeed)//REPRODUZIU
        {
            timerReprodution = 0;
            birdPopulation += birdLevel * Random.Range(3,5);
            Xp += birdLevel / Random.Range(8,10);
            life = birdPopulation * birdLevel;
        }

        if (Xp >= birdLevel * 5)//SUBIU DE NIVEL
        {
            birdLevel += 1;
            levelTxt.text = "Level " + birdLevel.ToString();
        }


        //altera o tamanho com base na populacao
        transform.localScale = new Vector3(birdPopulation / (birdLevel * 100), birdPopulation / (birdLevel * 100), birdPopulation / (birdLevel * 100));


        #region Movement
        if (Input.GetMouseButtonDown(1))//rightclick
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            direcaoClique();
        }

        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);

        #endregion
    }

    void direcaoClique()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = new Vector3(
            mousePos.x - transform.position.x,
            mousePos.y = transform.position.y,
            0
            );

        transform.up -= direction;
    }
    private void OnMouseDown()
    {
        print("click");
    }

    void UpdateLevel()
    {
        switch (birdLevel)
        {
            case 3:
                reprodutionSpeed = 1.9f;
                break;
            case 5:
                reprodutionSpeed = 1.7f;
                break;
            case 10:
                reprodutionSpeed = 1.5f;
                break;
            case 12:
                reprodutionSpeed = 1.35f;
                break;
            case 16:
                reprodutionSpeed = 1.25f;
                break;
            case 20:
                reprodutionSpeed = 1.1f;
                break;
            case 24:
                reprodutionSpeed = 1f;
                break;
            case 30:
                reprodutionSpeed = 0.8f;
                break;
            case 35:
                reprodutionSpeed = 0.7f;
                break;
            case 40:
                reprodutionSpeed = 0.5f;
                break;
        }
    }
}
