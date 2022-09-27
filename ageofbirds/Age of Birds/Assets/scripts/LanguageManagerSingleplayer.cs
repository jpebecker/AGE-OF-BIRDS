using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManagerSingleplayer : MonoBehaviour
{
    [Header("Painel Times")]
    public Text timePassaros;
    public Text timeNatureza,legendaSlider,legendaSlider2,venceu1,venceu2,derrota1,derrota2,sair1,sair2;

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
        }
    }
}
