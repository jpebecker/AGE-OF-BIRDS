using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManagerSingleplayer : MonoBehaviour
{
    [Header("Painel Times")]
    public Text timePassaros;
    public Text timeNatureza,subtitlePassaro,subtitleNature,objective1Passaro,objective2Passaro,objective1Nature,objective2Nature,legendaSlider,legendaSlider2,venceu1,venceu2,derrota1,derrota2,sair1,sair2, txtWait;

    private string nickName;

    private void Start()
    {
        Translate();

    }
    public void Translate()
    {
        if (PlayerPrefs.GetInt("language") == 1)
        {
            timePassaros.text = "BIRDS";
            timeNatureza.text = "NATURE";
            legendaSlider.text = "SURVIVE";
            legendaSlider2.text = "DEFEAT THE BIRDS";
            venceu1.text = "VICTORY";
            venceu2.text = "VICTORY";
            derrota1.text = "DEFEAT";
            derrota2.text = "DEFEAT";
            sair1.text = "EXIT";
            sair2.text = "EXIT";
            subtitlePassaro.text = "SURVIVE AGAINST NATURE";
            subtitleNature.text = "ELIMINATE THE BIRD SPECIE";
            objective1Passaro.text = "COLLECT:";
            objective2Passaro.text = "AVOID:";
            objective1Nature.text = "PLACE IT:";
            objective2Nature.text = "DESTROY:";
            txtWait.text = "Waiting for players...";
        }
        else
        {
            timePassaros.text = "Pássaros";
            timeNatureza.text = "Natureza";
            legendaSlider2.text = "Derrote os pássaros";
            legendaSlider.text = "sobreviva pela maior quantidade de tempo";
            venceu1.text = "Vitória";
            venceu2.text = "Vitória";
            derrota1.text = "Derrota";
            derrota2.text = "Derrota";
            sair1.text = "Sair";
            sair2.text = "Sair";
            subtitlePassaro.text = "Sobreviva contra a natureza";
            subtitleNature.text = "Elimine a espécie dos pássaros";
            objective1Passaro.text = "Colete:";
            objective2Passaro.text = "Evite:";
            objective1Nature.text = "Posicione:";
            objective2Nature.text = "Destrua:";
            txtWait.text = "Aguardando jogadores...";
        }
    }
}
