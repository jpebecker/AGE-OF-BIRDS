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
    public GameObject painelLoading;
    public void Connect()
    {
      PhotonNetwork.GameVersion = "0.1";
      PhotonNetwork.ConnectUsingSettings();
      painelLoading.SetActive(true);
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
                
        }
        else
        {
            if(nickInput.text.Length < 3 || nickInput.text.Length > 12)
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
        painelLoading.SetActive(false);
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
