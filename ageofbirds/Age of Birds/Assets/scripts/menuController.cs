using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private AudioMixer mix;
    [SerializeField] private Toggle toggleMusic,toggleFullscreen,toggleFps;
    [SerializeField] private Slider sliderMusic, sliderSfx;
    [SerializeField] private Text txtFPS;
    [SerializeField] private GameObject painelLinguagem;
    private float deltaTime;
    private void Start()
    {
        if (PlayerPrefs.HasKey("language"))
        {
            painelLinguagem.SetActive(false);
        }
        else
        {
            painelLinguagem.SetActive(true);
        }

        if (PlayerPrefs.HasKey("music"))
        {
            if (PlayerPrefs.GetInt("music") == 1)//som ativado
            {
                ToggleMusic(true);
                toggleMusic.isOn = true;
                sliderMusic.value = PlayerPrefs.GetInt("musicVolume");
                MusicVolume(sliderMusic.value);
            }
            else
            {
                ToggleMusic(false);
                toggleMusic.isOn = false;
                sliderMusic.value = PlayerPrefs.GetInt("musicVolume");
            }
          
        }
        else
        {
            toggleMusic.isOn = true;
            sliderMusic.value = 5;
            MusicVolume(sliderMusic.value);
        }
        if (!PlayerPrefs.HasKey("Fps"))
        {
            txtFPS.gameObject.SetActive(false);
            toggleFps.isOn = false;
        }
        if (!PlayerPrefs.HasKey("fullScreen"))
        {
            toggleFullscreen.isOn = true;
        }
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sliderSfx.value = PlayerPrefs.GetInt("sfxVolume");
            SoundEffectVolume(sliderSfx.value);
        }

        Photon.Pun.PhotonNetwork.Disconnect();
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("Fps") == 1)
        {
            txtFPS.gameObject.SetActive(true);
            toggleFps.isOn = true;
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            txtFPS.text = Mathf.Ceil(fps).ToString();
        }
        else
        {
            toggleFps.isOn = false;
            txtFPS.gameObject.SetActive(false);
        }
       
    }

    //outras funções do menu inicial
    #region StandardFunctions
    public void SinglePlayer()
    {
        print("singleplayer");
        SceneManager.LoadScene(1);
    }
    public void Disconnect()
    {
        Photon.Pun.PhotonNetwork.Disconnect();
    }
    public void QuitApp()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    #endregion

    #region Configs

    public void MusicVolume(float value)
    {
        if (PlayerPrefs.GetInt("music") == 1)
        {
            if (value == 5)
            {
                mix.SetFloat("music", -5);
                PlayerPrefs.SetInt("musicVolume", 5);
            }
            else if (value == 4)
            {
                mix.SetFloat("music", -20);
                PlayerPrefs.SetInt("musicVolume", 4);

            }
            else if (value == 3)
            {
                mix.SetFloat("music", -25);
                PlayerPrefs.SetInt("musicVolume", 3);
            }
            else if (value == 2)
            {
                mix.SetFloat("music", -35);
                PlayerPrefs.SetInt("musicVolume", 2);
            }
            else if (value == 1)
            {
                mix.SetFloat("music", -50);
                PlayerPrefs.SetInt("musicVolume", 1);
            }
            else
            {
                mix.SetFloat("music", -80);
                PlayerPrefs.SetInt("musicVolume", 0);
            }
        }
      
    }

    public void ToggleMusic(bool isOn)
    {
        if (!isOn)
        {
            mix.SetFloat("music", -80);
            PlayerPrefs.SetInt("music", 0);
        }
        else
        {
            mix.SetFloat("music", -5);
            PlayerPrefs.SetInt("music", 1);
        }
        
    }
    public void SoundEffectVolume(float value)
    {
            if (value == 5)
            {
                mix.SetFloat("sfx", -5);
                PlayerPrefs.SetInt("sfxVolume", 5);
            }
            else if (value == 4)
            {
                mix.SetFloat("sfx", -20);
                PlayerPrefs.SetInt("sfxVolume", 4);

            }
            else if (value == 3)
            {
                mix.SetFloat("sfx", -25);
                PlayerPrefs.SetInt("sfxVolume", 3);
            }
            else if (value == 2)
            {
                mix.SetFloat("sfx", -35);
                PlayerPrefs.SetInt("sfxVolume", 2);
            }
            else if (value == 1)
            {
                mix.SetFloat("sfx", -50);
                PlayerPrefs.SetInt("sfxVolume", 1);
            }
            else
            {
                mix.SetFloat("sfx", -80);
                PlayerPrefs.SetInt("sfxVolume", 0);
            }
    }

    public void Fullscreen(bool isFullscreen)
    {
        if (isFullscreen)
        {
            PlayerPrefs.SetInt("fullScreen", 1);
            Screen.fullScreen = true;
        }
        else
        {
            PlayerPrefs.SetInt("fullScreen", 0);
            Screen.fullScreen = false;
        }
    }

    public void Fps(bool isON)
    {
        if (isON)
        {
            PlayerPrefs.SetInt("Fps", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Fps",0);
        }
    }
    #endregion

}

