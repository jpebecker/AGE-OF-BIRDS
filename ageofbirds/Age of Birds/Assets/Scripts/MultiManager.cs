using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MultiManager : MonoBehaviourPunCallbacks
{
    [Header("referencias")]
    [SerializeField] private GameObject canvas;//canvas de jogo
    [SerializeField] private GameObject painelAves;//painel de escolha de time
    [SerializeField] private GameObject menuPausePainel;//painel de pause

    [SerializeField] private Transform[] spawnpoints;//array spawnpoints das aves
    [SerializeField] private Transform transformParentNick;//array spawnpoints das aves
    [SerializeField] private GameObject prefabNick;//array spawnpoints das aves
    [SerializeField] private GameObject aveA;//prefab jogador azul
    [SerializeField] private GameObject aveB;//prefab jogador vermelho
    [SerializeField] private GameObject aveC;//prefab jogador verde
    [SerializeField] private GameObject aveD;//prefab jogador amarelo
    [SerializeField] private GameObject aveE;//prefab jogador rosa
    private PhotonView PhotonV;

    //times
    private bool HasMaster;
    private bool IsMaster;

    //private ChatController _chatController_;//referencia ao codigo de chat
    private bool isPaused;//bolha de controle de pause

    [SerializeField] private List<PlayerItem> playerItemList = new List<PlayerItem>();
    [SerializeField] private PlayerItem playerItemPrefab;
    [SerializeField] private Transform playerItemParent1,playerItemParent2;


    //sala de espera
    [SerializeField] private GameObject waitingPanel, spectatorPanel, MasterPanel;
    [SerializeField] private Text roomNameTXT1,roomNameTXT2, FPStxt;
    private int PlayersProntos;
    private bool IsReady;

    //JogoEmAndamento
    private bool PartidaAtiva;
    private void Start()//inicializa a UI e atribui os scripts
    {
        canvas.SetActive(true);
        PhotonV = GetComponent<PhotonView>();
        UpdatePlayersList();
        //_chatController_ = GetComponent<ChatController>();
        roomNameTXT1.text = PhotonNetwork.CurrentRoom.Name;
        roomNameTXT2.text = PhotonNetwork.CurrentRoom.Name;

        if (PhotonNetwork.CurrentRoom.Players.Count < 2)
        {
            painelAves.SetActive(false);
            waitingPanel.SetActive(true);
           
        }

        if (PartidaAtiva)//se a partida ja esta em andamento ele nao deixa o jogador controlar nada
        {
            waitingPanel.SetActive(false);
            MasterPanel.SetActive(false);
            spectatorPanel.SetActive(true);

            objectController[] objs = FindObjectsOfType<objectController>();
            
            foreach(objectController controllers in objs)
            {
                controllers.CanControl = false;
            }
        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//se apertar ESC chama o pause
        {
            if (!waitingPanel)
            {
                isPaused = !isPaused;
                ToggleMenuPause();
            }
            else
            {
                VoltarProMenu();
            }
          
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        
    }
    #region pause e chat
    private void ToggleMenuPause()//ativa o painel de pause
    {
        menuPausePainel.SetActive(isPaused);
    }

    public void VoltarProMenu()//chamado pelo Btn do pause menu
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }

    public void DespauseBtn()//chamado pelo Btn de RESUME
    {
        isPaused = false;
        ToggleMenuPause();
    }


    #endregion

    #region Criar espécies
    public void CreateBirdA()//chamado pelo BTN de criar time
    {
        int indexSpawn = UnityEngine.Random.Range(0, (spawnpoints.Length - 1));//randomiza
        Transform transformPoint = spawnpoints[indexSpawn];
        GameObject BirdA = PhotonNetwork.Instantiate(aveA.name, transformPoint.position, transformPoint.rotation);
        //BirdA.GetComponent<PlayerController>()._teamController = _teamController_;
        //_chatController_._pc = lcBluePlayer.GetComponent<PlayerController>();
        painelAves.SetActive(false);
        SetCustomproperties(1);
    }

    public void CreateBirdB()//chamado pelo BTN de criar time
    {
        int indexSpawn = UnityEngine.Random.Range(0, (spawnpoints.Length - 1));
        Transform transformPoint = spawnpoints[indexSpawn];
        GameObject BirdB = PhotonNetwork.Instantiate(aveB.name, transformPoint.position, transformPoint.rotation);
        //BirdB.GetComponent<PlayerController>()._teamController = _teamController_;
        //_chatController_._pc = BirdB.GetComponent<PlayerController>();
        painelAves.SetActive(false);
        SetCustomproperties(2);
    }

    public void CreateBirdC()//chamado pelo BTN de criar time
    {
        int indexSpawn = UnityEngine.Random.Range(0, (spawnpoints.Length - 1));
        Transform transformPoint = spawnpoints[indexSpawn];
        GameObject BirdC = PhotonNetwork.Instantiate(aveB.name, transformPoint.position, transformPoint.rotation);
        //BirdB.GetComponent<PlayerController>()._teamController = _teamController_;
        //_chatController_._pc = BirdB.GetComponent<PlayerController>();
        painelAves.SetActive(false);
        SetCustomproperties(3);
    }
    public void CreateBirdD()//chamado pelo BTN de criar time
    {
        int indexSpawn = UnityEngine.Random.Range(0, (spawnpoints.Length - 1));
        Transform transformPoint = spawnpoints[indexSpawn];
        GameObject BirdC = PhotonNetwork.Instantiate(aveB.name, transformPoint.position, transformPoint.rotation);
        //BirdB.GetComponent<PlayerController>()._teamController = _teamController_;
        //_chatController_._pc = BirdB.GetComponent<PlayerController>();
        painelAves.SetActive(false);
        SetCustomproperties(4);
    }
    public void CreateBirdE()//chamado pelo BTN de criar time
    {
        int indexSpawn = UnityEngine.Random.Range(0, (spawnpoints.Length - 1));
        Transform transformPoint = spawnpoints[indexSpawn];
        GameObject BirdC = PhotonNetwork.Instantiate(aveB.name, transformPoint.position, transformPoint.rotation);
        //BirdB.GetComponent<PlayerController>()._teamController = _teamController_;
        //_chatController_._pc = BirdB.GetComponent<PlayerController>();
        painelAves.SetActive(false);
        SetCustomproperties(5);
    }

    private void SetCustomproperties(int aveEscolhida)
    {
        ExitGames.Client.Photon.Hashtable birdHashtable = new ExitGames.Client.Photon.Hashtable();
        birdHashtable.Add("Bird ", aveEscolhida);
        PhotonNetwork.SetPlayerCustomProperties(birdHashtable);
    }

    #endregion

    #region tabeladejogadores
    public override void OnPlayerEnteredRoom(Player Newplayer)
    {
        UpdatePlayersList();
    }
    public void UpdatePlayersList()//atualiza a lista de salas quando for chamado
    {
        foreach(PlayerItem prefab in playerItemList)
        {
            Destroy(prefab.gameObject);//apaga os itens que existem
        }

        playerItemList.Clear();//limpa a lista

        if(PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newItem1 = Instantiate(playerItemPrefab, playerItemParent1);
            newItem1.SetPlayerInfo(player.Value);

            PlayerItem newItem2 = Instantiate(playerItemPrefab, playerItemParent2);
            newItem2.SetPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newItem1.LocalPlayerSettings();
                newItem2.LocalPlayerSettings();
            }
            playerItemList.Add(newItem1);
            playerItemList.Add(newItem2);
        }
    }
    #endregion

    #region iniciodejogo

    public void PlayerReady(GameObject text)
    {
        IsReady = !IsReady;
        foreach (PlayerItem playerItem in GameObject.FindObjectsOfType<PlayerItem>())
        {
            if (playerItem._player == PhotonNetwork.LocalPlayer)
            {
                playerItem.SetReady(IsReady);
            }
            else
            {
                playerItem.SetReady((bool)playerItem._player.CustomProperties["isready"]);
            }
        }
        if (IsReady)
        {
            PlayersProntos++;
            text.GetComponent<Text>().color = Color.green;
        }
        else
        {
            text.GetComponent<Text>().color = Color.white;
            PlayersProntos--;
        }
        print(PlayersProntos + "ready");
        PhotonV.RPC("playersProntos", RpcTarget.AllBufferedViaServer, PlayersProntos);
    }


    [PunRPC]private void playersProntos(int playerProntos)
    {
        this.PlayersProntos = playerProntos;
    
        if (playerProntos >= 1)//Começa o jogo
        {
            PhotonV.RPC("GenerateTeams", RpcTarget.AllBufferedViaServer);
            waitingPanel.SetActive(false);
        }
    }

    [PunRPC] private void GenerateTeams()
    {
        print("sorteando Times");

        Player[] sortearLista = PhotonNetwork.PlayerList;
        if(this.PhotonV.Owner.ActorNumber == sortearLista[Random.Range(0, sortearLista.Length)].ActorNumber)
        {
            print("PlayerIsBirds");
            this.painelAves.SetActive(true);
           
        }
        else
        {
            print("PlayerIsMaster");
            this.MasterPanel.SetActive(true);
            this.IsMaster = true;
        }
        
        PartidaAtiva = true;
    }

    #endregion
}
