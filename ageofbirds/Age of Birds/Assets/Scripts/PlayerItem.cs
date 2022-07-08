using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerItem : MonoBehaviour
{
    [Header("refs")]
    [SerializeField] public Text nick, text2;//textos do painel

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Player _player;

    private void Start()
    {
        _player.CustomProperties = playerProperties;
    }
    public void SetPlayerInfo(Player player)
    {
        nick.text = player.NickName;
        _player = player;
    }

    public void SetReady(bool playerIsReady)
    {
        if (playerIsReady)
        {
            text2.gameObject.SetActive(true);
            playerProperties["isready"] = true;
            text2.text = "READY";
        }
        else
        {
            text2.gameObject.SetActive(false);
            playerProperties["isready"] = false;
            text2.text = "";
        }
    }
    public void LocalPlayerSettings()
    {

    }
}
