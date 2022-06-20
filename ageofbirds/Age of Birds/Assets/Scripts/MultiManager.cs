using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MultiManager : MonoBehaviour
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
    [SerializeField] private GameObject aveC;//prefab jogador vermelho
    [SerializeField] private GameObject aveD;//prefab jogador vermelho
    [SerializeField] private GameObject aveE;//prefab jogador vermelho


    private PhotonView PhotonV;
    //private ChatController _chatController_;//referencia ao codigo de chat
    private bool isPaused;//bolha de controle de pause

    private void Start()//inicializa a UI e atribui os scripts
    {
        canvas.SetActive(true);
        PhotonV = GetComponent<PhotonView>();
        //_teamController_ = GetComponent<TeamController>();
        //_chatController_ = GetComponent<ChatController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//se apertar ESC chama o pause
        {
            isPaused = !isPaused;
            ToggleMenuPause();
        }
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

    #region Criar times
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
    public void UpdatePlayersList(List<string> nameList)//atualiza a lista de salas quando for chamado
    {
        foreach (string nick in nameList)
        {
            PlayerItem nickItem = Instantiate(prefabNick, transformParentNick.position, transformParentNick.rotation, transformParentNick).GetComponent<PlayerItem>();
            nickItem.UpdateListaSalas(nick);
        }
    }
    #endregion
}
