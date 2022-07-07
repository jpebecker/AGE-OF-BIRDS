using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class UiManager : MonoBehaviour
{
    [Header("multiplayerLobby")]
    [SerializeField] private Text txtStatus, txtNickLobby;//txt de status 
    [SerializeField] private GameObject painelNick, painelTutorial;//painel que faz o nick do jogador
    [SerializeField] private InputField InputNick;//input que le o nick que o jogador digita
    [SerializeField] private GameObject PainelLobby;//painel do lobby que se exibe apos logar
    [SerializeField] private Text TxtServerData;//exibe as configuracoes do servidor como a region/versao/max players
    [Header("salas")]
    [SerializeField] private InputField inputNomeSala;//input que le o nome da sala que o jogador digita
    [SerializeField] private GameObject PrefabSalaLista;//prefab exibido quando ha uma sala criada mostrando o nome e qtd de players
    [SerializeField] private Transform parenteListaSala;//posicao para spawnar o prefab da sala
    [SerializeField] private byte playersSala;
    [Header("mapa")]
    [SerializeField] private Image mapImage;
    [SerializeField] private Sprite[] images;
    [SerializeField] private Text  maptxt;


    private LogConsole LogConsole;//referencia ao script da UI
    private menuController menuController;//referencia ao gamecontroller
    private int tamanhominimonick = 3;//quantidade minima de letras pro nick do jogador
    [HideInInspector]public int mapaNum = 1;

    void Start()
    {
        LogConsole = GetComponent<LogConsole>();
        menuController = GetComponent<menuController>();

        CarregarNick();//carrega o nickname salvo
        txtStatus.text = string.Empty;//zera o status 
    }

    #region Nick e NomedaSala -- Criar e Entrar
    public void NicknameBtn()//chamado pelo CONNECT e o INPUTNICK
    {
        if (PlayerPrefs.HasKey("nickname"))
        {
            painelNick.SetActive(false);//fecha o painel de nick
        }
        if (PlayerPrefs.HasKey("T") == false)
        {
            painelTutorial.SetActive(true);
            return;
        }
        if (InputNick.text.Length < tamanhominimonick)//se o nick digitado no input field tiver menos letras que o minimo
        {
            LogConsole.DefinirTexto("Nickname inválido, minimo de " + tamanhominimonick);
            return;
        }

            SalvarNick();//salva o nickname
            painelNick.SetActive(false);//fecha o painel de nick
            LogConsole.DefinirTexto(string.Empty);
            txtStatus.text = "CARREGANDO...";//ilustra o status
            menuController.StartConnection(InputNick.text);//tenta conectar no servidor com o nick salvo
            txtNickLobby.text = PlayerPrefs.GetString("nickname");
    }

    public void CriarSalaBtn()//chamado pelo botao de criar sala
    {
        if (inputNomeSala.text.Length < 3)//se o nick for menor que o minimo
        {
            LogConsole.DefinirTexto("Nome de sala inválido, minimo de 3 letras");
            inputNomeSala.textComponent.GetComponent<Text>().text = "Nome inválido";
            return;
        }
        menuController.NewRoomDefinitions(playersSala);//define a nova qtd de players por sala

        menuController.CreateRoom(inputNomeSala.text, true);//chama o metodo de criar sala do gamecontroller junto com o nome atribuido
    }

    public void TooglePlayersMax(int playerMax)
    {
        playersSala = (byte)playerMax;
    }//chamado pelos toggles

    public void ChangeMap(int button)
    {
        if (button == 1)//esquerda
        {
            mapaNum--;
            if(mapaNum < 1)
            {
                mapaNum = 3;
            }
        }
        else//direita
        {
            mapaNum++;
            if (mapaNum > 3)
            {
                mapaNum = 1;
            }
        }

        if (mapaNum == 1)
        {
            print(mapaNum);
            mapImage.sprite = images[0];
            maptxt.text = "florestas";
        }
        else if (mapaNum == 2)
        {
            print(mapaNum);
            mapImage.sprite = images[1];
            maptxt.text = "ilhas";
        }
        else if (mapaNum == 3)
        {
            print(mapaNum);
            mapImage.sprite = images[2];
            maptxt.text = "montanhas";
        }
    }//chamado pela selecao de fase

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
            InputNick.text = PlayerPrefs.GetString("nickname");//puxa do player prefs
        }
    }

    private void SalvarNick()//salva o nick
    {
        PlayerPrefs.SetString("nickname", InputNick.text);//manda pro player prefs
    }

    public void Tutorial()
    {
        PlayerPrefs.SetInt("T", 1);
    }

    #endregion

    #region feedback de UI
    public void ToggleLobbyPanel(bool mostrarpainel = true)//chamado para exibir o painel do lobby
    {
        if (mostrarpainel == true)
        {
            PainelLobby.SetActive(true);
            txtStatus.text = string.Empty;
        }
        else
        {
            PainelLobby.SetActive(false);
        }
    }

    public void PrintLog(string consoleMessage)//determina o txt do console
    {
        LogConsole.DefinirTexto(consoleMessage);
    }

    public void ShowMessage(string message)//determina o txt do painel
    {
        
        txtStatus.text = message;
    }

    public void ShowServerData(string data)//determina o txt de propriedades do servidor
    {
        TxtServerData.text = data;
    }

    public void AtualizarListaSalas(List<RoomInfo> listaSalas)//atualiza a lista de salas quando for chamado
    {
        
        foreach (RoomInfo room in listaSalas)
        {
            RoomItem roomItem = Instantiate(PrefabSalaLista, parenteListaSala.position, parenteListaSala.rotation, parenteListaSala).GetComponent<RoomItem>();
            roomItem.UpdateListaSalas(room.Name, room.MaxPlayers, room.PlayerCount, this);
        }
    }

    #endregion

}
