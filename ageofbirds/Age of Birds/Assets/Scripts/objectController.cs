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
        phV = GetComponent<PhotonView>();
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("[PlayerController]Jogador desconectado");
            return;
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
            cameraController.instance.followTransform = transform.parent;//trava a camera neles
        }

        if (tipodeobjeto != ObjectType.passaros)//move o passaro selecionado até o ponto
        {
            birdCollection aveSelected = new birdCollection();
            print("terrain click");

            foreach(birdCollection birds in FindObjectsOfType<birdCollection>())//para os que estiverem selecionados
            {
                if (birds.isSelected && birds.GetComponent<PhotonView>().IsMine)
                {
                    aveSelected = birds;
                }
            }
            print("move");
            aveSelected.GetComponent<birdCollection>().isSelected = false;
            aveSelected.GetComponent<birdCollection>().MoveBirds(clickPOS);

        }

        if (cameraController.instance.followTransform)
        {
            if (tipodeobjeto != ObjectType.passaros && cameraController.instance.followTransform.GetComponent<birdCollection>().isSelected == false)//desativa a ui
            {
                cameraController.instance.followTransform.GetComponent<birdCollection>().birdOptions.SetActive(BirdOptions);
                cameraController.instance.followTransform = null;
                BirdOptions = false;
               


            }
        }
       
    }
    private void OnMouseOver()//mouse em cima
    {
        //right click
        if(Input.GetMouseButtonDown(1) && tipodeobjeto == ObjectType.terreno)//right click no terreno
        {
            BirdOptions = false;
            cameraController.instance.followTransform.GetComponent<birdCollection>().birdOptions.SetActive(false);
            cameraController.instance.followTransform = null;
        }


        if(Input.GetMouseButtonDown(1) && tipodeobjeto != ObjectType.terreno)//right click
        {
            if (phV.IsMine)//se for do jogador
            {
                BirdOptions = !BirdOptions;
                //abrir painel
                cameraController.instance.followTransform = null;
                print("open Options");
                GetComponent<birdCollection>().birdOptions.SetActive(BirdOptions);
                GetComponent<birdCollection>().isSelected = false;
                GetComponent<birdCollection>().moving = false;
            }
            if (!phV.IsMine)//se nao for do jogador
            {
                BirdOptions = !BirdOptions;
                //abrir painel
                cameraController.instance.followTransform = null;
                print("open enemie Options");
            }

        }
    }
}
