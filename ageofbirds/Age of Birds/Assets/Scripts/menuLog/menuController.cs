using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class menuController : MonoBehaviourPunCallbacks
{
    [Header("Configs")]
    [SerializeField] private byte playersporsala = 4;//quantidade maxima de jogadores por sala
    private string versao_aplicativo = "0.1";//versao do aplicativo
    private UiManager uiController;//controlador de botões e interface de jogo
    [SerializeField] private AudioMixer mix;
    [SerializeField] private Toggle toggleMusic,toggleFullscreen,toggleFps;
    [SerializeField] private Slider sliderMusic, sliderSfx;
    [SerializeField] private Text txtFPS;

    [SerializeField] private GameObject painelLinguagem;
    private float deltaTime;
    private void Start()
    {
        uiController = GetComponent<UiManager>();

        if (PlayerPrefs.HasKey("language"))
        {
            painelLinguagem.SetActive(false);
        }
        else
        {
            painelLinguagem.SetActive(true);
        }

        if (PlayerPrefs.HasKey("music"))
        {
            if (PlayerPrefs.GetInt("music") == 1)//som ativado
            {
                ToggleMusic(true);
                toggleMusic.isOn = true;
                sliderMusic.value = PlayerPrefs.GetInt("musicVolume");
                MusicVolume(sliderMusic.value);
            }
            else
            {
                ToggleMusic(false);
                toggleMusic.isOn = false;
                sliderMusic.value = PlayerPrefs.GetInt("musicVolume");
            }
          
        }
        else
        {
            toggleMusic.isOn = true;
            sliderMusic.value = 5;
            MusicVolume(sliderMusic.value);
        }
        if (!PlayerPrefs.HasKey("Fps"))
        {
            txtFPS.gameObject.SetActive(false);
            toggleFps.isOn = false;
        }
        if (!PlayerPrefs.HasKey("fullScreen"))
        {
            toggleFullscreen.isOn = true;
        }
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sliderSfx.value = PlayerPrefs.GetInt("sfxVolume");
            SoundEffectVolume(sliderSfx.value);
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("Fps") == 1)
        {
            txtFPS.gameObject.SetActive(true);
            toggleFps.isOn = true;
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            txtFPS.text = Mathf.Ceil(fps).ToString();
        }
        else
        {
            toggleFps.isOn = false;
            txtFPS.gameObject.SetActive(false);
        }
       
    }
    public void StartConnection(string nickdejogador)//chamado para entrar no lobby dps de definir o nick
    {
        PhotonNetwork.GameVersion = versao_aplicativo;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = nickdejogador;
        uiController.PrintLog("Conectando...");
    }

    public void CreateRoom(string nomedasala, bool criarsala = true)//chamado quando o jogador quer criar uma sala
    {
        RoomOptions roomOpt = new RoomOptions();
        roomOpt.MaxPlayers = playersporsala;
        roomOpt.BroadcastPropsChangeToAll = true;
        PhotonNetwork.JoinOrCreateRoom(nomedasala, roomOpt, TypedLobby.Default);//metodo chamado para criar ou dar join
        uiController.ToggleLobbyPanel(false);

        if (criarsala)//ilustra no console que esta criando
        {
            uiController.ShowMessage("Criando...");
        }
        else//ilustra no console que esta joinando
        {
            uiController.ShowMessage("Entrando...");
        }
    }

    public void NewRoomDefinitions(byte PlayersSala)
    {
        playersporsala = PlayersSala;
    }

    #region PhotonProcedures

    public override void OnConnectedToMaster()
    {
        uiController.ShowServerData(string.Format("Conectado a região: " + PhotonNetwork.CloudRegion + " | Versão : " + versao_aplicativo + " | Número máximo de jogadores: " + playersporsala));
        uiController.PrintLog("Conectado");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()//atualiza a UI do lobby e dá feedback ao jogador
    {
        uiController.ToggleLobbyPanel();
        uiController.PrintLog("Você se conectou ao lobby");
    }

    public override void OnJoinedRoom()//chamado quando a sala ja foi criada e carrega a fase
    {
        base.OnJoinedRoom();
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            uiController.ShowMessage("Entrando...");

            if(uiController.mapaNum == 1)
            {
                PhotonNetwork.LoadLevel("florestas");
            }
            else if(uiController.mapaNum == 2)
            {
                PhotonNetwork.LoadLevel("ilhas");
            }
            else if(uiController.mapaNum == 3)
            {
                PhotonNetwork.LoadLevel("montanhas");
            }    
 
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)//chamado quando a lista de salas tem alteracao
    {
        uiController.AtualizarListaSalas(roomList);
    }

    public override void OnDisconnected(DisconnectCause causa)
    {
        uiController.ToggleLobbyPanel(false);
        uiController.PrintLog(causa.ToString());
    }

    public override void OnCreateRoomFailed(short codigoderetorno, string causa)//chamado quando o jogador não consegue criar ou logar numa sala
    {
        uiController.ToggleLobbyPanel(false);
        uiController.PrintLog(causa);
        uiController.PrintLog(codigoderetorno.ToString());
        SceneManager.LoadScene(0);
    }
    #endregion

    //outras funções do menu inicial
    #region StandardFunctions
    public void SinglePlayer()
    {
        print("singleplayer");
        SceneManager.LoadScene(1);
    }

    public void Disconnect()
    {
        print("lobbyQuit");
        PhotonNetwork.Disconnect();
    }
    public void QuitApp()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    #endregion

    #region Configs

    public void MusicVolume(float value)
    {
        if (PlayerPrefs.GetInt("music") == 1)
        {
            if (value == 5)
            {
                mix.SetFloat("music", -5);
                PlayerPrefs.SetInt("musicVolume", 5);
            }
            else if (value == 4)
            {
                mix.SetFloat("music", -20);
                PlayerPrefs.SetInt("musicVolume", 4);

            }
            else if (value == 3)
            {
                mix.SetFloat("music", -25);
                PlayerPrefs.SetInt("musicVolume", 3);
            }
            else if (value == 2)
            {
                mix.SetFloat("music", -35);
                PlayerPrefs.SetInt("musicVolume", 2);
            }
            else if (value == 1)
            {
                mix.SetFloat("music", -50);
                PlayerPrefs.SetInt("musicVolume", 1);
            }
            else
            {
                mix.SetFloat("music", -80);
                PlayerPrefs.SetInt("musicVolume", 0);
            }
        }
      
    }

    public void ToggleMusic(bool isOn)
    {
        if (!isOn)
        {
            mix.SetFloat("music", -80);
            PlayerPrefs.SetInt("music", 0);
        }
        else
        {
            mix.SetFloat("music", -5);
            PlayerPrefs.SetInt("music", 1);
        }
        
    }
    public void SoundEffectVolume(float value)
    {
            if (value == 5)
            {
                mix.SetFloat("sfx", -5);
                PlayerPrefs.SetInt("sfxVolume", 5);
            }
            else if (value == 4)
            {
                mix.SetFloat("sfx", -20);
                PlayerPrefs.SetInt("sfxVolume", 4);

            }
            else if (value == 3)
            {
                mix.SetFloat("sfx", -25);
                PlayerPrefs.SetInt("sfxVolume", 3);
            }
            else if (value == 2)
            {
                mix.SetFloat("sfx", -35);
                PlayerPrefs.SetInt("sfxVolume", 2);
            }
            else if (value == 1)
            {
                mix.SetFloat("sfx", -50);
                PlayerPrefs.SetInt("sfxVolume", 1);
            }
            else
            {
                mix.SetFloat("sfx", -80);
                PlayerPrefs.SetInt("sfxVolume", 0);
            }
    }

    public void Fullscreen(bool isFullscreen)
    {
        if (isFullscreen)
        {
            PlayerPrefs.SetInt("fullScreen", 1);
            Screen.fullScreen = true;
        }
        else
        {
            PlayerPrefs.SetInt("fullScreen", 0);
            Screen.fullScreen = false;
        }
    }

    public void Fps(bool isON)
    {
        if (isON)
        {
            PlayerPrefs.SetInt("Fps", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Fps",0);
        }
    }
    #endregion

}

