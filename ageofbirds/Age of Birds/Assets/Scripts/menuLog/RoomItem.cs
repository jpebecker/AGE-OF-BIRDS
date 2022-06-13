using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    [Header("ref")]
    [SerializeField] private Text txtNomeSala, txtPlayerSala;//textos do painel
    [SerializeField] private Image roomImage;
    [SerializeField] private Button JoinBtn;//botao de logar

    public void UpdateListaSalas(string nomesala, int playersMax, int playersAtual, UiManager uimanager)//atualiza a UI com base no que recebe do metodo
    {
        txtNomeSala.text = nomesala;
        txtPlayerSala.text = string.Format("{0} / {1}", playersAtual, playersMax);

        if (playersAtual == playersMax)//se a quantidade de players jogando for igual ao maximo ele desabilita o btn
        {
            JoinBtn.interactable = false;
            return;
        }
        JoinBtn.onClick.AddListener(delegate { uimanager.LogaEmSalaBtn(nomesala); });//adiciona ouvinte ao botao == quando clicar, chama o metodo
    }
}
