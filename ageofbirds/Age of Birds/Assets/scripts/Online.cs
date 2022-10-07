using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Online : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    public InputField nickInput;
    public GameObject painelLoading,painelCarregando;
    public void Connect()
    {
      PhotonNetwork.GameVersion = "0.1";
      PhotonNetwork.ConnectUsingSettings();
        if (PlayerPrefs.GetInt("language") == 1)
        {
            painelLoading.SetActive(true);
        }
        else
        {
            painelCarregando.SetActive(true);
        }
    
    }
    public void CreateSala()
    {
        if(createInput.text.Length < 3 || createInput.text.Length > 12)
        {
            if (PlayerPrefs.GetInt("language") == 1)
            {
                createInput.text = "incorrect";
            }
            else
            {
                createInput.text = "incorreto";
            }
                
        }//nome de sala menor ou maior que o permitido
        else
        {
            if(nickInput.text.Length < 3 || nickInput.text.Length > 12)//nickname menor ou maior que o permitido
            {
                if (PlayerPrefs.GetInt("language") == 1)
                {
                    nickInput.text = "incorrect";
                }
                else
                {
                    nickInput.text = "incorreto";
                }
            }
            else//cria a sala
            {
                PlayerPrefs.SetString("nickPlayer", nickInput.text);
                PhotonNetwork.NickName = nickInput.text;
                PhotonNetwork.CreateRoom(createInput.text);
            }

        }
      
    }
    public void JoinRoom()
    {
        if (joinInput.text.Length < 3 || joinInput.text.Length >12)
        {
            if (PlayerPrefs.GetInt("language") == 1)
            {
                createInput.text = "incorrect";
            }
            else
            {
                createInput.text = "incorreto";
            }
        }
        else
        {
            if (nickInput.text.Length < 3 || nickInput.text.Length > 12)
            {
                if (PlayerPrefs.GetInt("language") == 1)
                {
                    createInput.text = "incorrect";
                }
                else
                {
                    createInput.text = "incorreto";
                }
            }
            else
            {
                PhotonNetwork.NickName = nickInput.text;
                PhotonNetwork.JoinRoom(joinInput.text);
            }
        }
      
    }
    public override void OnConnectedToMaster()
    {
        print("connected");
        if (PlayerPrefs.HasKey("nickPlayer"))
        {
            nickInput.text = PlayerPrefs.GetString("nickPlayer");
        }

        if (PlayerPrefs.GetInt("language") == 1)
        {
            painelLoading.SetActive(false);
        }
        else
        {
            painelCarregando.SetActive(false);
        }
            
        //PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
      
        print("Onlobby");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
