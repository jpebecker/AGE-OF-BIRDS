using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class UiManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text _txtMenssage;//txt de status 
    [SerializeField] private GameObject _panelNickname;//painel que faz o nick do jogador
    [SerializeField] private InputField _txtNickName;//input que le o nick que o jogador digita
    [SerializeField] private GameObject _panelLobby;//painel do lobby que se exibe apos logar
    [SerializeField] private Text _txtServerData;//exibe as configuracoes do servidor como a region/versao/max players
    [SerializeField] private InputField _txtRoomName;//input que le o nome da sala que o jogador digita
    [SerializeField] private GameObject _prefabRoomItem;//prefab exibido quando ha uma sala criada mostrando o nome e qtd de players
    [SerializeField] private Transform _parentRoomItem;//posicao para spawnar o prefab da sala

    //private UiLog _uiLog;//referencia ao script da UI
    private menuController menuController;//referencia ao gamecontroller
    private int _minNameLenght = 3;//quantidade minima de letras pro nick do jogador

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("[Ui_Controller inicializando]");
        //_uiLog = GetComponent<UiLog>();
        menuController = GetComponent<menuController>();

        //GetNickName();//carrega o nickname salvo
        _txtMenssage.text = string.Empty;//zera o status 
        _panelNickname.SetActive(true);//ativa o painel de nickname
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Nick e NomedaSala -- Criar e Entrar
    public void NicknameBtn()//chamado pelo btn q confirma o nick
    {
        if (_txtNickName.text.Length < _minNameLenght)//se o nick digitado no input field tiver menos letras que o minimo
        {
            //_uiLog.SetText("Nickname inválido, minimo de " + _minNameLenght);
            return;
        }

        SalvarNick();//salva o nickname
        _panelNickname.SetActive(false);//fecha o painel de nick
        //_uiLog.SetText(string.Empty);
        _txtMenssage.text = "LOADING...";//ilustra o status
        menuController.StartConnection(_txtNickName.text);//tenta conectar no servidor com o nick salvo
    }

    public void CriarSalaBtn()//chamado pelo botao de criar sala
    {

        if (_txtRoomName.text.Length < 3)//se o nick for menor que o minimo
        {
            //_uiLog.SetText("Nome de sala inválido, minimo de 3 letras");
            return;
        }

        menuController.CreateRoom(_txtRoomName.text, true);//chama o metodo de criar sala do gamecontroller junto com o nome atribuido
    }

    public void LogaEmSalaBtn(string nomedasala)//chamado pelo botao do prefab de sala ja criada
    {
        if (nomedasala.Length < 3)
        {
            return;
        }
        menuController.CreateRoom(nomedasala, false);//da join na sala ja criada
    }

    private void CarregarNick()//carrega o nick salvo
    {
        if (PlayerPrefs.HasKey("nickname"))
        {
            _txtNickName.text = PlayerPrefs.GetString("nickname");//puxa do player prefs
        }
    }

    private void SalvarNick()//salva o nick
    {
        PlayerPrefs.SetString("nickname", _txtNickName.text);//manda pro player prefs
    }

    #endregion

    #region feedback de UI
    public void ToggleLobbyPanel(bool mostrarpainel = true)//chamado para exibir o painel do lobby
    {
        Debug.Log("[UiController] Painel do Lobby: " + mostrarpainel);
        if (mostrarpainel == true)
        {
            _panelLobby.SetActive(true);
            _txtMenssage.text = string.Empty;
        }
        else
        {
            _panelLobby.SetActive(false);
        }
    }

    public void PrintLog(string consoleMessage)//determina o txt do console
    {
        //_uiLog.SetText(consoleMessage);
    }

    public void ShowMessage(string s)//determina o txt do painel
    {
        Debug.Log("[UiController]txt do lobby atualizado");
        _txtMenssage.text = s;
    }

    public void ShowServerData(string data)//determina o txt de propriedades do servidor
    {
        _txtServerData.text = data;
    }

    public void AtualizarListaSalas(List<RoomInfo> listaSalas)//atualiza a lista de salas quando for chamado
    {
        
        foreach (RoomInfo room in listaSalas)
        {
            //RoomItem roomItem = Instantiate(_prefabRoomItem, _parentRoomItem.position, _parentRoomItem.rotation, _parentRoomItem).GetComponent<RoomItem>();
            //roomItem.UpdateRoom(_ri.Name, _ri.MaxPlayers, _ri.PlayerCount, this);
        }
    }

    #endregion

}
