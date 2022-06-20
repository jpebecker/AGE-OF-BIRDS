using UnityEngine;
using Photon.Pun;
public enum ObjectType
{
    terreno, passaros
}
public class objectController : MonoBehaviour
{
    public ObjectType tipodeobjeto;
    [HideInInspector]public bool BirdOptions;
    private Vector3 clickPOS;
    private PhotonView phV;
    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("[PlayerController]Jogador desconectado");
            return;
        }

        if (phV.IsMine)//se o jogador controla  o object
        {
            //_txtNickName.text = PhotonNetwork.LocalPlayer.NickName;
            //_team_ = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        }
        else//se o jogador Nao controla  o object
        {
            Debug.Log("[PlayerController] outro jogador atribuido");
            //_txtNickName.text = _pv_.Owner.NickName;
            //_team_ = (int)_pv_.Owner.CustomProperties["Team"];
        }
    }
    private void OnMouseDown()
    {
        //coleta o ponto que foi clicado
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            clickPOS = hit.point;
        }


        if (tipodeobjeto != ObjectType.terreno)//se clicar nos passaros
        {
            cameraController.instance.followTransform = transform.parent;
        }

        if (tipodeobjeto != ObjectType.passaros && FindObjectOfType<birdCollection>().isSelected == true)//mover os passaros ate ponto X
        {
            print("move");
            FindObjectOfType<birdCollection>().isSelected = false;
            FindObjectOfType<birdCollection>().MoveBirds(clickPOS);
        }
        if (cameraController.instance.followTransform)
        {
            if (tipodeobjeto != ObjectType.passaros && cameraController.instance.followTransform.GetComponent<birdCollection>().isSelected == false)//desativa a ui
            {
                cameraController.instance.followTransform = null;
                BirdOptions = false;
                FindObjectOfType<birdCollection>().birdOptions.SetActive(BirdOptions);


            }
        }
       
    }
    private void OnMouseOver()
    {
        //right click
        if(Input.GetMouseButtonDown(1) && tipodeobjeto == ObjectType.terreno)
        {
            BirdOptions = false;
            FindObjectOfType<birdCollection>().birdOptions.SetActive(false);
            cameraController.instance.followTransform = null;
        }


        if(Input.GetMouseButtonDown(1) && tipodeobjeto != ObjectType.terreno)//right click
        {
            if (phV.IsMine)
            {
                BirdOptions = !BirdOptions;
                //abrir painel
                cameraController.instance.followTransform = null;
                print("open Options");
                GetComponent<birdCollection>().birdOptions.SetActive(BirdOptions);
                GetComponent<birdCollection>().isSelected = false;
                GetComponent<birdCollection>().moving = false;
            }
           
        }
    }
}
