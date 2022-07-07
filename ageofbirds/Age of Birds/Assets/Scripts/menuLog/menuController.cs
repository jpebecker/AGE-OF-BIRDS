using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class menuController : MonoBehaviourPunCallbacks
{
    [Header("Configs")]
    [SerializeField] private byte playersporsala = 4;//quantidade maxima de jogadores por sala
    private string versao_aplicativo = "0.1";//versao do aplicativo
    private UiManager uiController;//controlador de botões e interface de jogo

    private void Start()
    {
        uiController = GetComponent<UiManager>();
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
        Application.Quit();
    }

    #endregion

}

