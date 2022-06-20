using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    [Header("refs")]
    [SerializeField] private Text nick, ping;//textos do painel
    [SerializeField] private Image birdImage;

    public void UpdateListaSalas(string nickPlayer)//atualiza a UI com base no que recebe do metodo
    {
        nick.text = nickPlayer;
    }
}
