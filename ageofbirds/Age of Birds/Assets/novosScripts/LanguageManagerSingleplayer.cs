using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManagerSingleplayer : MonoBehaviour
{
    [Header("Painel Times")]
    public Text timePassaros;
    public Text timeNatureza,legendaSlider;

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
        }
        else
        {
            timePassaros.text = "Pássaros";
            timeNatureza.text = "Natureza";
            legendaSlider.text = "sobreviva pela maior quantidade de tempo";
        }
    }
}
