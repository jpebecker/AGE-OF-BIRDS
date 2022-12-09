using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NewGameController : MonoBehaviour
{
    [Header("paineis")]
    [SerializeField] private GameObject painelTimes;
    [SerializeField] private GameObject controlBirds, controlNature, gameOverPanel, winPanel, exitPanel,practiceWin,practiceFail,waitPanel,waitPanel2;
    [SerializeField] public Slider sliderBirds, sliderNature;
    [SerializeField] private Button birdBtn, natureBtn;
    [SerializeField] private Text winText, gameoverText, txtRoomName;
    private string birdNickname, natureNickname;
    [HideInInspector] public PhotonView view;

    [Header("Codes")]
    [SerializeField] private bird passaro;
    [SerializeField] private IncisivePlay nature;
    private bool TimerIsActive;
    private int Team;

    void Start()
    {
        painelTimes.SetActive(true);
        view = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Update()
    {
        if (!exitPanel.activeInHierarchy && !PhotonNetwork.IsConnected)
        {
            Time.timeScale = 1;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            //Exit();
            if (!PhotonNetwork.IsConnected)
            {
                Time.timeScale = 0;
            }
            exitPanel.SetActive(true);
        }

        if (TimerIsActive)//jogo ativo
        {
            sliderBirds.value -= Time.deltaTime;
            sliderNature.value -= Time.deltaTime;
        }

        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
            {
                //birdBtn.interactable = false;
                //natureBtn.interactable = false;
                waitPanel.gameObject.SetActive(true);
                if (PlayerPrefs.GetInt("language") == 1)//ingles
                {
                    txtRoomName.text = "Room: " + PhotonNetwork.CurrentRoom.Name;
                }
                else//portugues
                {
                    txtRoomName.text = "Sala: " + PhotonNetwork.CurrentRoom.Name;
                }

                if (TimerIsActive)
                {
                    print("player left");
                    winPanel.SetActive(true);
                    Time.timeScale = 0;
                }
               
            }
            else
            {
                //birdBtn.interactable = true;
                //natureBtn.interactable = true;
                waitPanel.gameObject.SetActive(false);
            }
        }
     

    }

    public void ChooseTeamSinglePlayer(int team)
    {
        if (PhotonNetwork.IsConnected)
        {
            if (team == 0)//bird
            {
                Team = 0;
                passaro.view.RPC("birdName", RpcTarget.AllBuffered, PhotonNetwork.NickName.ToString());
                FindObjectOfType<camera>().player = passaro.gameObject.transform;
                waitPanel2.gameObject.SetActive(true);
                view.RPC("BtnSwitch", RpcTarget.AllBuffered, 0);

            }
            else//nature
            {
                Team = 1;
                waitPanel2.gameObject.SetActive(true);
                view.RPC("BtnSwitch", RpcTarget.AllBuffered, 1);
            }
        }
        else
        {
            if (team == 0)//bird
            {
                controlBirds.SetActive(true);
                TimerIsActive = true;
                passaro.gameObject.SetActive(true);
                passaro.IsPlaying = true;
                FindObjectOfType<spawner>().Offline = true;
                painelTimes.SetActive(false);

            }
            else//nature
            {
                controlNature.SetActive(true);
                TimerIsActive = true;
                nature.isPlaying = true;
                passaro.IsPlaying = false;
                passaro.gameObject.SetActive(true);
                painelTimes.SetActive(false);
            }
        }
       
    }


    private void TriggerControls()
    {
        if(Team == 0)
        {
            controlBirds.SetActive(true);
            passaro.IsPlaying = true;
        }
        else
        {
            controlNature.SetActive(true);
            nature.isPlaying = true;
        }
    }
    public void GameOver()
    {
       Time.timeScale = 0;
       practiceFail.SetActive(true);
    }

    [PunRPC]
    public void EndMatch(int win)
    {
        if(win == 0)//bird ganhou
        {
            if(Team == 0)//se for bird
            {
                if (PlayerPrefs.GetInt("language") == 1)
                {
                    winText.text = natureNickname + " lost the match";
                }
                else
                {
                    winText.text = natureNickname + " perdeu o jogo";
                }
                winPanel.SetActive(true);
                Time.timeScale = 0;
            }
            else//se for nature
            {
                if (PlayerPrefs.GetInt("language") == 1)//ingles
                {
                    gameoverText.text = birdNickname + " won the match";
                }
                else//portugues
                {
                    gameoverText.text = birdNickname + " ganhou o jogo";
                }
                gameOverPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else//bird perdeu
        {
            if (Team == 0)//se for bird
            {
                if (PlayerPrefs.GetInt("language") == 1)//ingles
                {
                    gameoverText.text = natureNickname + " won the match";
                }
                else//portugues
                {
                    gameoverText.text = natureNickname + " ganhou o jogo";
                }
                gameOverPanel.SetActive(true);
                Time.timeScale = 0;
            }
            else//se for nature
            {
                if (PlayerPrefs.GetInt("language") == 1)
                {
                    winText.text = birdNickname + " lost the match";
                }
                else
                {
                    winText.text = birdNickname + " perdeu o jogo";
                }
                winPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void Win()
    {
       Time.timeScale = 0;
       practiceWin.SetActive(true); 
    }

    [PunRPC]
    public void BtnSwitch(int btn)
    {
        if(btn == 0)
        {
            if (natureBtn.interactable)
            {
                birdBtn.interactable = false;
                birdNickname = PhotonNetwork.NickName.ToString();
            }
            else
            {
                print("start");
                waitPanel2.SetActive(false);
                painelTimes.SetActive(false);
                TriggerControls();
                TimerIsActive = true;
                passaro.gameObject.SetActive(true);
            }
        
        }
        else
        {
            if (birdBtn.interactable)
            {
                natureBtn.interactable = false;
                natureNickname = PhotonNetwork.NickName.ToString();
            }
            else
            {
                print("start");
                waitPanel2.SetActive(false);
                painelTimes.SetActive(false);
                TriggerControls();
                TimerIsActive = true;
                passaro.gameObject.SetActive(true);
                
            }
           
        }
    }

    public void Exit()
    {
        Time.timeScale = 1;
        if (PhotonNetwork.IsConnected)
        {
            Photon.Pun.PhotonNetwork.Disconnect();
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
