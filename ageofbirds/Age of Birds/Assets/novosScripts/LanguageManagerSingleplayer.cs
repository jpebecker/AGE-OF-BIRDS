using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManagerSingleplayer : MonoBehaviour
{
    [Header("Painel Times")]
    public Text timePassaros;
    public Text timeNatureza;

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
        }
        else
        {
            timePassaros.text = "Pássaros";
            timeNatureza.text = "Natureza";
        }
    }
}
