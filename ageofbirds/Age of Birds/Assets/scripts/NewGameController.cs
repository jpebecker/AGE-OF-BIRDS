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
    [SerializeField] private GameObject controlBirds,controlNature,gameOverPanel,winPanel;
    [SerializeField] private Slider sliderBirds,sliderNature;
    [SerializeField] private Text winText, gameoverText;
    private int Team;

    [Header("Codes")]
    [SerializeField] private bird passaro;
    [SerializeField] private IncisivePlay nature;
    private bool TimerIsActive;
    
    void Start()
    {
        painelTimes.SetActive(true);
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Exit();
        }

        if (TimerIsActive)
        {
            sliderBirds.value -= Time.deltaTime;
            sliderNature.value -= Time.deltaTime;
        }

        if(sliderBirds.value <= 0)//birds
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Win();
            }
            else
            {
                GameOver();
            }
        }
    }

    private void ChooseTeamSinglePlayer(int team)
    {
        if(team == 0)//bird
        {
            controlBirds.SetActive(true);
            TimerIsActive = true;
            passaro.IsPlaying = true;
            passaro.view.RPC("birdName",RpcTarget.AllBuffered, PhotonNetwork.NickName.ToString());
        }
        else//nature
        {
            controlNature.SetActive(true);
            TimerIsActive = true;
            nature.isPlaying = true;
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        string nome = Photon.Pun.PhotonNetwork.CurrentRoom.GetPlayer(2).NickName;
        gameoverText.text = nome + " ganhou o jogo";
    }
    private void Win()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
        string nome = Photon.Pun.PhotonNetwork.CurrentRoom.GetPlayer(2).NickName;
        winText.text = nome + " perdeu o jogo";

    }

    public void Exit()
    {
        Photon.Pun.PhotonNetwork.Disconnect();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    
}
