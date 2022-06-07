using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class menuController : MonoBehaviourPunCallbacks
{
    [Header("Configs")]
    [SerializeField] private byte playerporsala = 4;//quantidade maxima de jogadores por sala
    private string versao_aplicativo = "0.1";//versao do aplicativo
    //private UiController _uiController;//controlador de bot�es e interface de jogo

    private void Start()
    {

    }
    public void StartConnection(string nickdejogador)//chamado para entrar no lobby dps de definir o nick
    {
        PhotonNetwork.GameVersion = versao_aplicativo;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = nickdejogador;
        //_uiController.ShowLog("Conectando...");
    }

    public void CreateRoom(string nomedasala, bool criarsala = true)//chamado quando o jogador quer criar uma sala
    {
        RoomOptions roomOpt = new RoomOptions();
        roomOpt.MaxPlayers = playerporsala;
        PhotonNetwork.JoinOrCreateRoom(nomedasala, roomOpt, TypedLobby.Default);//metodo chamado para criar ou dar join
        //_uiController.ShowLobbyPanel(false);
        if (criarsala)//ilustra no console que esta criando
        {
          //  _uiController.ShowMessage("Creating...");
        }
        else//ilustra no console que esta joinando
        {
          //  _uiController.ShowMessage("Joining...");
        }
    }

    #region PhotonProcedures

    public override void OnConnectedToMaster()
    {
        //_uiController.ShowServerData(string.Format("Conectado a regi�o: " + PhotonNetwork.CloudRegion + " | Vers�o : " + versao_aplicativo + " | N�mero m�ximo de jogadores: " + playerporsala));
        //_uiController.ShowLog("Conectado");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()//atualiza a UI do lobby e d� feedback ao jogador
    {
        //_uiController.ShowLobbyPanel();
        //_uiController.ShowLog("entrou no lobby");
    }

    public override void OnJoinedRoom()//chamado quando a sala ja foi criada e carrega a fase
    {
        base.OnJoinedRoom();
        {
            //_uiController.ShowMessage("Entrando...");
            PhotonNetwork.LoadLevel(1);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)//chamado quando a lista de salas tem alteracao
    {
        //_uiController.UpdateRoomList(roomList);
    }

    public override void OnDisconnected(DisconnectCause causa)
    {
        //_uiController.ShowLobbyPanel(false);
        //_uiController.ShowLog(causa.ToString());
    }

    public override void OnCreateRoomFailed(short codigoderetorno, string causa)//chamado quando o jogador n�o consegue criar uma sala
    {
        //_uiController.ShowLobbyPanel(false);
        //_uiController.ShowLog(causa);
        //_uiController.ShowLog(codigoderetorno.ToString());
    }
    #endregion

    //outras fun��es do menu inicial
    #region StandardFunctions
    public void SinglePlayer()
    {
        print("singleplayer");
        SceneManager.LoadScene(1);
    }
    public void QuitApp()
    {
        print("quit");
        PhotonNetwork.Disconnect();
        Application.Quit();
    }

    #endregion

}

