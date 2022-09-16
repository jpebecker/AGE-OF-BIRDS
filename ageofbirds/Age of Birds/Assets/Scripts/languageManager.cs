using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class languageManager : MonoBehaviour
{
    [Header("Painel Linguagem")]
    public Text title;
    public Text ingles, portugues, confirmar;
    [Header("Aba Principal Menu")]
    public Text play;
    public Text options, sair, online, offline;
    [Header("Aba Options")]
    public Text language;
    public Text voltar1, voltar2, voltar3, voltar4,voltar5, fullscreen, sfxvolume, musicvolume,music, languagetitle, english, portuguese;
    private int lingua;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("language"))
        {
            lingua = 1;
        }
        else
        {
            lingua = PlayerPrefs.GetInt("language");
            TranslateMenu();
        }
    }
    public void GameIngles()
    {
        title.text = "Game Language:";
        ingles.text = "English";
        portugues.text = "Portuguese";
        confirmar.text = "CONFIRM";
        lingua = 1;

    }
    public void GamePortugues()
    {
        title.text = "Linguagem do jogo:";
        ingles.text = "Ingl�s";
        portugues.text = "Portugu�s";
        confirmar.text = "CONFIRMAR";
        lingua = 2;
    }

    public void TranslateMenu()
    {
        if (PlayerPrefs.GetInt("language") == 1)
        {
            play.text = "Play";
            options.text = "Options";
            sair.text = "Quit";
            online.text = "Multiplayer";
            offline.text = "Practice";
            language.text = "Language";
            voltar1.text = "BACK";
            voltar2.text = "BACK";
            voltar3.text = "BACK";
            voltar4.text = "BACK";
            voltar5.text = "BACK";
            fullscreen.text = "fullscreen";
            sfxvolume.text = "Sound Effects Volume";
            musicvolume.text = "Music Volume";
            music.text = "Music";
            languagetitle.text = "Game Language:";
            english.text = "English";
            portuguese.text = "Portuguese";
        }
        else
        {
            play.text = "Jogar";
            options.text = "Op��es";
            sair.text = "Sair";
            online.text = "Multijogador";
            offline.text = "Treino";
            language.text = "Linguagem";
            voltar1.text = "Voltar";
            voltar2.text = "Voltar";
            voltar3.text = "Voltar";
            voltar4.text = "Voltar";
            voltar5.text = "Voltar";
            fullscreen.text = "Tela cheia";
            sfxvolume.text = "Volume de efeitos sonoros";
            musicvolume.text = "Volume da m�sica";
            music.text = "M�sica";
            languagetitle.text = "Linguagem de jogo:";
            english.text = "Ingl�s";
            portuguese.text = "Portugu�s";
        }
    }
    public void Confirm()
    {
        PlayerPrefs.SetInt("language", lingua);
        TranslateMenu();
    }
}