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
    public Transform[] directions;
    [Header("UI")]
    public Text levelTxt;
    public Text NickTxt;
    public Slider bushSlider, waterSlider,lifeSlider;

    private Vector3 target;
    private float timerReprodution;
    private bool IsGettingDamage;
    [HideInInspector]public PhotonView view;
    private Camera cam;
    private float Targetzoom;


    //offline
    private float TimerDirection;
    private int Direction;
    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }
    void Start()
    {
        cam = Camera.main;
        Targetzoom = cam.orthographicSize;
        IsGettingDamage = false;
        target = transform.position;
        levelTxt.text = "Level " + birdLevel.ToString();
        Water = 100;
        Bushes = 100;

        if (!IsPlaying && !PhotonNetwork.IsConnected)
        {
            switch (Random.Range(0, 3))//direction
            {
                case 0:
                    Direction = 1;
                    break;
                case 1:
                    Direction = 2;
                    break;
                case 2:
                    Direction = 1;
                    break;
                case 3:
                    Direction = 2;
                    break;
            }
        }
    }
    void Update()
    {
        if (IsPlaying)
        {
            timerReprodution += Time.deltaTime;
            Water -= (1f * birdLevel) * Time.deltaTime;
            Bushes -= (1f * birdLevel) * Time.deltaTime;
            if (timerReprodution >= reprodutionSpeed)//REPRODUZIU
            {
                timerReprodution = 0;
                birdPopulation += birdLevel * Random.Range(3, 5);
                Xp += birdLevel + Random.Range(3, 8);
            }

            if (Xp >= birdLevel * 5)//SUBIU DE NIVEL
            {
                Xp = 0;
                birdLevel += 1;
                life += 5 * birdLevel;
                UpdateLevel();
                levelTxt.text = "Level " + birdLevel.ToString();
            }

            if (IsGettingDamage)
            {
                life -= 50 * Time.deltaTime;
                if (life <= 0 && PhotonNetwork.IsConnected)//bird died
                {
                    FindObjectOfType<NewGameController>().view.RPC("EndMatch", RpcTarget.All, 1);//nature ganha
                }
                else if (life <= 0 && !PhotonNetwork.IsConnected)
                {
                    if (IsPlaying)
                    {
                        FindObjectOfType<NewGameController>().GameOver();
                    }
                    else
                    {
                        FindObjectOfType<NewGameController>().Win();
                    }
                }
            }

            if (Water <= 1 || Bushes <= 1)
            {
                if (!PhotonNetwork.IsConnected)
                {
                    if (IsPlaying)
                    {
                        FindObjectOfType<NewGameController>().GameOver();
                    }
                    else
                    {
                        FindObjectOfType<NewGameController>().Win();
                    }

                }
                else
                {
                    FindObjectOfType<NewGameController>().view.RPC("EndMatch", RpcTarget.All, 1);//nature ganha
                }

            }

            if (FindObjectOfType<NewGameController>().sliderBirds.value <= 0)//se a contagem zerar
            {

                if (PhotonNetwork.IsConnected)
                {
                    FindObjectOfType<NewGameController>().view.RPC("EndMatch", RpcTarget.All, 0);//bird ganha
                }
                else
                {
                    if (IsPlaying)
                    {
                        FindObjectOfType<NewGameController>().Win();
                    }
                    else
                    {
                        FindObjectOfType<NewGameController>().GameOver();
                    }
                }

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
            if (Input.GetMouseButtonDown(1) && IsPlaying)//rightclick
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;
                //direcaoClique();
            }

            if (PhotonNetwork.IsConnected)
            {

                view.RPC("move", RpcTarget.AllBuffered);
            }
            else
            {
                move();
            }
            
  
            #endregion

            if (transform.localScale.x < 20)
            {
                transform.localScale += new Vector3(birdPopulation / (100 * birdLevel), birdPopulation / (100 * birdLevel), birdPopulation / (100 * birdLevel)) * Time.deltaTime;
            }
        }
        else if(!IsPlaying && !PhotonNetwork.IsConnected)//SE FOR UM BOT
        {
            timerReprodution += Time.deltaTime;
            TimerDirection += Time.deltaTime;
            waterSlider.gameObject.SetActive(false);
            bushSlider.gameObject.SetActive(false);

            if (timerReprodution >= reprodutionSpeed)//REPRODUZIU
            {
                timerReprodution = 0;
                birdPopulation += birdLevel * Random.Range(3, 5);
                Xp += birdLevel + Random.Range(3, 8);
            }

            if (Xp >= birdLevel * 5)//SUBIU DE NIVEL
            {
                Xp = 0;
                birdLevel += 1;
                life += 5 * birdLevel;
                UpdateLevel();
                levelTxt.text = "Level " + birdLevel.ToString();
            }

            if (IsGettingDamage)
            {
                life -= 50 * Time.deltaTime;
                if (life <= 0)
                {
                  FindObjectOfType<NewGameController>().Win();
                }
            }

            #region Sliders
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

            #region movementBot
            if (TimerDirection > 5)
            {
              Direction =  Random.Range(0, 7);
              TimerDirection = 0;
            }

            switch (Direction)
            {
                case 0:
                    transform.position += Vector3.up * Time.deltaTime * movementSpeed;
                    break;
                case 1:
                    transform.position -= Vector3.up * Time.deltaTime * movementSpeed;
                    transform.position -= Vector3.right * Time.deltaTime * movementSpeed;
                    break;
                case 2:
                    transform.position += Vector3.right * Time.deltaTime * movementSpeed;
                    break;
                case 3:
                    transform.position += Vector3.up * Time.deltaTime * movementSpeed;
                    transform.position += Vector3.right * Time.deltaTime * movementSpeed;
                    break;
                case 4:
                    transform.position -= Vector3.right * Time.deltaTime * movementSpeed;
                    break;
                case 5:
                    transform.position -= Vector3.up * Time.deltaTime * movementSpeed;
                    break;
                case 6:
                    transform.position += Vector3.up * Time.deltaTime * movementSpeed;
                    transform.position -= Vector3.right * Time.deltaTime * movementSpeed;
                    break;
                case 7:
                    transform.position -= Vector3.up * Time.deltaTime * movementSpeed;
                    transform.position += Vector3.right * Time.deltaTime * movementSpeed;
                    break;

            }
            #endregion

            if (transform.localScale.x < 20)
            {
                transform.localScale += new Vector3(birdPopulation / (100 * birdLevel), birdPopulation / (100 * birdLevel), birdPopulation / (100 * birdLevel)) * Time.deltaTime;
            }
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
    void UpdateLevel()
    {
        switch (birdLevel)
        {
            case 3:
                reprodutionSpeed = 1.95f;
                cam.orthographicSize += 1.2f;
                break;
            case 5:
                reprodutionSpeed = 1.9f;
                cam.orthographicSize += 1.5f;
                movementSpeed += 3;
                break;
            case 7:
                reprodutionSpeed = 1.8f;
                cam.orthographicSize += 1.7f;
                break;
            case 10:
                reprodutionSpeed = 1.7f;
                cam.orthographicSize += 1.7f;
                movementSpeed += 2.5f;
                break;
            case 12:
                cam.orthographicSize += 1.7f;
                movementSpeed += 1.5f;
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

    [PunRPC]
    private void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
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
                    Water += birdLevel * 5 * Time.deltaTime;
                    waterSlider.value = Water;
                }
             
                break;
            case "folhas":
                if (col.gameObject.transform.localScale.x >= 2 && col.gameObject.transform.localScale.y >= 2 && col.gameObject.transform.localScale.z >= 2)
                {
                    col.gameObject.transform.localScale -= new Vector3(1 * Time.deltaTime, 1 * Time.deltaTime, 1 * Time.deltaTime);
                    Bushes += birdLevel * 5 * Time.deltaTime;
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

     [PunRPC]private void birdName(string name)
    {
        gameObject.SetActive(true);
        NickTxt.text = name;
    }

}
