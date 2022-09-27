using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum TypeOfEvent
{
    Tornado,Firestorm,
}
public class IncisivePlay : MonoBehaviour
{
    [Header("Configs")]
    public bool isPlaying = false;
    public TypeOfEvent eventoPosicionar;
    public GameObject tornadoPrefab, fireStormPrefab;
    private PhotonView view;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && isPlaying && view.IsMine)//rightclick
        {
            switch (eventoPosicionar)
            {
                case TypeOfEvent.Tornado:
                    GameObject obj = PhotonNetwork.Instantiate(tornadoPrefab.name,Camera.main.ScreenToWorldPoint(Input.mousePosition),Quaternion.identity);
                    obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0);
                    break;
                case TypeOfEvent.Firestorm:
                    GameObject objeto = PhotonNetwork.Instantiate(fireStormPrefab.name, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                    objeto.transform.position = new Vector3(objeto.transform.position.x, objeto.transform.position.y, 0);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && view.IsMine)
        {
            eventoPosicionar = TypeOfEvent.Tornado;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && view.IsMine)
        {
            eventoPosicionar = TypeOfEvent.Firestorm;
        }
    }

}
