using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
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
    public float Water, Bushes;//porcentagem
    public bool IsPlaying = false;
    [Header("UI")]
    public Text levelTxt;
    public Text NickTxt;
    public Slider bushSlider, waterSlider,lifeSlider;

    private Vector3 target;
    private float timerReprodution;
    private bool IsGettingDamage;
    private PhotonView view;
    void Start()
    {
        view = GetComponent<PhotonView>();
        IsGettingDamage = false;
        target = transform.position;
        levelTxt.text = "Level " + birdLevel.ToString();
        Water = 100;
        Bushes = 100;
    }
    void Update()
    {
        timerReprodution += Time.deltaTime;
        Water -= (0.1f * birdLevel) * Time.deltaTime;
        Bushes -= (0.1f * birdLevel) * Time.deltaTime;
        if (timerReprodution >= reprodutionSpeed)//REPRODUZIU
        {
            timerReprodution = 0;
            birdPopulation += birdLevel * Random.Range(3,5);
            Xp += birdLevel + birdLevel / Random.Range(3, 8);
        }

        if (Xp >= birdLevel * 5)//SUBIU DE NIVEL
        {
            birdLevel += 1;
            life += 50;
            UpdateLevel();
            levelTxt.text = "Level " + birdLevel.ToString();
        }

        if (IsGettingDamage)
        {
            life -= 50 * Time.deltaTime;
            if(life <= 0)
            {
                FindObjectOfType<NewGameController>().GameOver();
            }
        }

        if(Water <=1 || Bushes <= 1)
        {
            FindObjectOfType<NewGameController>().GameOver();
        }

       
        #region Sliders
        if (Bushes > bushSlider.maxValue)
        {
            bushSlider.maxValue = Bushes;
            bushSlider.value = Bushes;
        }
        else
        {
            bushSlider.value = Bushes;
        }
        if (Water > waterSlider.maxValue)
        {
            waterSlider.maxValue = Water;
            waterSlider.value = Water;
        }
        else
        {
            waterSlider.value = Water;
        }
        if (life > lifeSlider.maxValue)
        {
            lifeSlider.maxValue = life;
            lifeSlider.value = life;
        }
        else
        {
            lifeSlider.value = life;
        }
        #endregion

        #region Movement
        if (Input.GetMouseButtonDown(1) && IsPlaying && view.IsMine)//rightclick
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            direcaoClique();
        }

        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);

        #endregion

        if(transform.localScale.x < 10)
        {
            transform.localScale += new Vector3(birdPopulation / 100, birdPopulation / 100, birdPopulation / 100) * Time.deltaTime / 2;
        }
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
                reprodutionSpeed = 1.95f;
                break;
            case 5:
                reprodutionSpeed = 1.9f;
                break;
            case 10:
                reprodutionSpeed = 1.8f;
                break;
            case 12:
                reprodutionSpeed = 1.7f;
                break;
            case 16:
                reprodutionSpeed = 1.6f;
                break;
            case 20:
                reprodutionSpeed = 1.5f;
                break;
            case 24:
                reprodutionSpeed = 1.4f;
                break;
            case 30:
                reprodutionSpeed = 1.3f;
                break;
            case 35:
                reprodutionSpeed = 1.25f;
                break;
            case 40:
                reprodutionSpeed = 1f;
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        col.GetComponent<SpriteRenderer>().color = new Color(col.GetComponent<SpriteRenderer>().color.r, col.GetComponent<SpriteRenderer>().color.g, col.GetComponent<SpriteRenderer>().color.b, .5f);

        switch (col.gameObject.tag)
        {
            case "water":
                print("water");
                col.gameObject.GetComponent<coletaveis>().IsInactive = false;
                break;
            case "folhas":
                print("bush");
                col.gameObject.GetComponent<coletaveis>().IsInactive = false;
                break;
            case "damage":
                IsGettingDamage = true;
                break;
        }


    }

    private void OnTriggerStay2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "water":
                if(col.gameObject.transform.localScale.x >=2 && col.gameObject.transform.localScale.y >= 2 && col.gameObject.transform.localScale.z >= 2)
                {
                    col.gameObject.transform.localScale -= new Vector3(1 * Time.deltaTime, 1 * Time.deltaTime, 1 * Time.deltaTime);
                    Water += 5 * Time.deltaTime;
                    waterSlider.value = Water;
                }
             
                break;
            case "folhas":
                if (col.gameObject.transform.localScale.x >= 2 && col.gameObject.transform.localScale.y >= 2 && col.gameObject.transform.localScale.z >= 2)
                {
                    col.gameObject.transform.localScale -= new Vector3(1 * Time.deltaTime, 1 * Time.deltaTime, 1 * Time.deltaTime);
                    Bushes += 5 * Time.deltaTime;
                    bushSlider.value = Bushes;
                }
                break;
            case "damage":
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        col.GetComponent<SpriteRenderer>().color = new Color(col.GetComponent<SpriteRenderer>().color.r, col.GetComponent<SpriteRenderer>().color.g, col.GetComponent<SpriteRenderer>().color.b, 1);

        switch (col.gameObject.tag)
        {
            case "water":
                if (col.gameObject.transform.localScale.x <= 2 && col.gameObject.transform.localScale.y <= 2 && col.gameObject.transform.localScale.z <= 2)
                {
                    col.gameObject.GetComponent<coletaveis>().IsInactive = true;
                }
                
                break;
            case "folhas":
                if (col.gameObject.transform.localScale.x <= 2 && col.gameObject.transform.localScale.y <= 2 && col.gameObject.transform.localScale.z <= 2)
                {
                    col.gameObject.GetComponent<coletaveis>().IsInactive = true;
                }
                break;
            case "damage":
                IsGettingDamage = false;
                break;
        }
    }


}