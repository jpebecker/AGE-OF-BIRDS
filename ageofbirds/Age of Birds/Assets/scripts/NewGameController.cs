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

    void Start()
    {
        painelTimes.SetActive(true);
        view = GetComponent<PhotonView>();
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

        if (TimerIsActive)
        {
            sliderBirds.value -= Time.deltaTime;
            sliderNature.value -= Time.deltaTime;

            if (PhotonNetwork.CurrentRoom.PlayerCount < 2)//alguem saiu
            {

            }
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
                passaro.view.RPC("birdName", RpcTarget.AllBuffered, PhotonNetwork.NickName.ToString());
                FindObjectOfType<camera>().player = passaro.gameObject.transform;
                view.RPC("Nick", RpcTarget.AllBuffered, 0, PhotonNetwork.NickName.ToString());
                natureBtn.interactable = false;
                waitPanel2.gameObject.SetActive(true);
            }
            else//nature
            {
                view.RPC("Nick", RpcTarget.AllBuffered, 1, PhotonNetwork.NickName.ToString());
                birdBtn.interactable = false;
                waitPanel2.gameObject.SetActive(true);
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

    [PunRPC]
    public void GameOver(int whoWins)
    {
        if (PhotonNetwork.IsConnected)
        {
            if (whoWins == 0)//birds Lose
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
            else//nature lose
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
        else
        {
            Time.timeScale = 0;
            practiceFail.SetActive(true);
        }

        //Juan
       
    }
    [PunRPC]
    public void Win(int whoWins)
    {
        if (PhotonNetwork.IsConnected)
        {
            if (whoWins == 0)//birds Win
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
            else
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
        else
        {
            Time.timeScale = 0;
            practiceWin.SetActive(true);
        }
    }

    [PunRPC]
    public void Nick(int team, string nick)
    {
        if (team == 0 && natureBtn.interactable == true)
        {
            print("bird selected");
            birdBtn.interactable = false;
            controlBirds.SetActive(true);
            birdNickname = nick;
        }
        else if(team == 1 && birdBtn.interactable == true)
        {
            print("nature selected");
            natureBtn.interactable = false;
            controlNature.SetActive(true);
            natureNickname = nick;
        }
        else if(team == 0 && natureBtn.interactable == false)
        {
            print("bird activate");
            controlBirds.SetActive(true);
            TimerIsActive = true;
            passaro.IsPlaying = true;
            waitPanel2.SetActive(false);
            painelTimes.SetActive(false);
            passaro.gameObject.SetActive(true);
        }
        else if(team == 1 && birdBtn.interactable == false)
        {
            print("nature activate");
            controlNature.SetActive(true);
            TimerIsActive = true;
            nature.isPlaying = true;
            waitPanel2.SetActive(false);
            painelTimes.SetActive(false);
            passaro.gameObject.SetActive(true);
        }
    }
    public void Exit()
    {
        Time.timeScale = 1;
        Photon.Pun.PhotonNetwork.Disconnect();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


}
