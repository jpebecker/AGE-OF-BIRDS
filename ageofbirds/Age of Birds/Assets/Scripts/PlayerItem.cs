using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerItem : MonoBehaviour
{
    [Header("refs")]
    [SerializeField] private Text nick, ping;//textos do painel

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    Player _player;

   public void SetPlayerInfo(Player player)
    {
        nick.text = player.NickName;
        _player = player;
    }

    public void LocalPlayerSettings()
    {

    }
}
