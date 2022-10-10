using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private float DelayToSpawn=5f;
    private float timer;
    public Slider sliderDelay;
    [SerializeField]private Image fillSlider,tornadoimage,fireImage;
    public Color tornadoColor, fireColor;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        sliderDelay.maxValue = DelayToSpawn;
        timer = DelayToSpawn;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        sliderDelay.value = timer;

        if (timer < DelayToSpawn)
        {
            tornadoimage.color = Color.grey;
            fireImage.color = Color.grey;
        }
        else
        {
            tornadoimage.color = Color.white;
            fireImage.color = Color.white;
        }

        #region posicionaraoclique

        if (Input.GetMouseButtonDown(1) && isPlaying && view.IsMine && timer >= DelayToSpawn && PhotonNetwork.IsConnected)//rightclick connected
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

            timer = 0;
        }
        if (Input.GetMouseButtonDown(1) && isPlaying && view.IsMine && timer >= DelayToSpawn && !PhotonNetwork.IsConnected)//rightclick offline
        {
            switch (eventoPosicionar)
            {
                case TypeOfEvent.Tornado:
                    GameObject obj = Instantiate(tornadoPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                    obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0);
                    break;
                case TypeOfEvent.Firestorm:
                    GameObject objeto = Instantiate(fireStormPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                    objeto.transform.position = new Vector3(objeto.transform.position.x, objeto.transform.position.y, 0);
                    break;
            }

            timer = 0;
        }

        #endregion

        #region teclas de selecao

        if (Input.GetKeyDown(KeyCode.Alpha1) && view.IsMine && PhotonNetwork.IsConnected)
        {
            eventoPosicionar = TypeOfEvent.Tornado;
            fillSlider.color = tornadoColor;
            //tornadoimage.color = Color.yellow;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && view.IsMine && PhotonNetwork.IsConnected)
        {
            eventoPosicionar = TypeOfEvent.Firestorm;
            fillSlider.color = fireColor;
            //fireImage.color = Color.yellow;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !PhotonNetwork.IsConnected)
        {
            eventoPosicionar = TypeOfEvent.Tornado;
            fillSlider.color = tornadoColor;
            //tornadoimage.color = Color.yellow;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !PhotonNetwork.IsConnected)
        {
            eventoPosicionar = TypeOfEvent.Firestorm;
            fillSlider.color = fireColor;
            //fireImage.color = Color.yellow;
        }

        #endregion
    }

}
