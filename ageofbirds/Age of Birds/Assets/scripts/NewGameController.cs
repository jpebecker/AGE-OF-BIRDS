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
    [SerializeField] private GameObject controlBirds, controlNature, gameOverPanel, winPanel;
    [SerializeField] public Slider sliderBirds, sliderNature;
    [SerializeField] private Button birdBtn, natureBtn;
    [SerializeField] private Text winText, gameoverText, txtWait;
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
        if (Input.GetButtonDown("Cancel"))
        {
            Exit();
        }

        if (TimerIsActive)
        {
            sliderBirds.value -= Time.deltaTime;
            sliderNature.value -= Time.deltaTime;
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            birdBtn.interactable = false;
            natureBtn.interactable = false;
            txtWait.gameObject.SetActive(true);
        }
        else
        {
            birdBtn.interactable = true;
            natureBtn.interactable = true;
            txtWait.gameObject.SetActive(false);
        }

    }

    private void ChooseTeamSinglePlayer(int team)
    {
        if (team == 0)//bird
        {
            controlBirds.SetActive(true);
            TimerIsActive = true;
            passaro.IsPlaying = true;
            passaro.view.RPC("birdName", RpcTarget.AllBuffered, PhotonNetwork.NickName.ToString());
            FindObjectOfType<camera>().player = passaro.gameObject.transform;
            view.RPC("Nick", RpcTarget.AllBuffered, 0, PhotonNetwork.NickName.ToString());
        }
        else//nature
        {
            controlNature.SetActive(true);
            TimerIsActive = true;
            nature.isPlaying = true;
            view.RPC("Nick", RpcTarget.AllBuffered, 1, PhotonNetwork.NickName.ToString());
        }
    }

    [PunRPC]
    public void GameOver(int whoWins)
    {
        if (whoWins == 0)//birds Lose
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            gameoverText.text = natureNickname + " ganhou o jogo";
        }
        else//nature lose
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            gameoverText.text = birdNickname + " ganhou o jogo";
        }
       
    }
    [PunRPC]
    public void Win(int whoWins)
    {
        if (whoWins == 0)//birds Win
        {
            winPanel.SetActive(true);
            Time.timeScale = 0;
            winText.text = natureNickname + " perdeu o jogo";
        }
        else
        {
            winPanel.SetActive(true);
            Time.timeScale = 0;
            winText.text = birdNickname + " perdeu o jogo";
        }
       

    }

    [PunRPC]
    private void Nick(int team, string nick)
    {
        if (team == 0)
        {
            birdBtn.interactable = false;
            birdNickname = nick;
        }
        else
        {
            natureBtn.interactable = false;
            natureNickname = nick;
        }
    }
    public void Exit()
    {
        Photon.Pun.PhotonNetwork.Disconnect();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


}
