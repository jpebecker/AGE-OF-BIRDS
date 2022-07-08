using UnityEngine;
using Photon.Pun;
public enum ObjectType
{
    terreno, GROUP, BIRDSPREFAB
}
public class objectController : MonoBehaviour
{
    public ObjectType tipodeobjeto;
    [HideInInspector]public bool BirdOptions;
    private Vector3 clickPOS;
    private PhotonView phV;
    [HideInInspector] public bool CanControl = true;
    private void Start()
    {
        //atribui o PHOTONVIEW
        if(tipodeobjeto == ObjectType.BIRDSPREFAB)
        {
            phV = GetComponentInParent<PhotonView>();
        }
        else
        {
            phV = GetComponentInParent<PhotonView>();
        }
      

        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("[PlayerController]Jogador desconectado");
            return;
        }
    }
    private void OnMouseDown()
    {
        if (CanControl)
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

            if (tipodeobjeto == ObjectType.terreno)//move o passaro selecionado até o ponto
            {
                print("terrain click");

                foreach (birdCollection birds in FindObjectsOfType<birdCollection>())//para os que estiverem selecionados
                {
                    if (birds.selectedToMove && birds.GetComponent<PhotonView>().IsMine)
                    {
                        print("move");
                        birds.GetComponent<birdCollection>().selectedToMove = false;
                        birds.GetComponent<birdCollection>().MoveBirds(clickPOS);
                    }
                    else if (birds.selectedToAttack && birds.GetComponent<PhotonView>().IsMine)
                    {
                        print("attackPoint");
                        birds.GetComponent<birdCollection>().selectedToAttack = false;
                        birds.GetComponent<birdCollection>().AttackEnemie(clickPOS);
                    }
                }
            }
        

        }

        if (cameraController.instance.followTransform)
        {
            if (tipodeobjeto == ObjectType.terreno && cameraController.instance.followTransform.GetComponent<birdCollection>().selectedToMove == false)//desativa a ui
            {
                cameraController.instance.followTransform.GetComponent<birdCollection>().birdOptions.SetActive(BirdOptions);
                cameraController.instance.followTransform = null;
                BirdOptions = false;

            }
        }
       
    }
    private void OnMouseOver()//mouse em cima
    {
        if (CanControl)
        {
            //right click
            if (Input.GetMouseButtonDown(1) && tipodeobjeto == ObjectType.terreno)//right click no terreno
            {
                BirdOptions = false;
                cameraController.instance.followTransform.GetComponent<birdCollection>().birdOptions.SetActive(false);
                cameraController.instance.followTransform = null;
            }


            if (Input.GetMouseButtonDown(1) && tipodeobjeto != ObjectType.terreno)//right click
            {
                if (phV.IsMine)//se for do jogador
                {
                    BirdOptions = !BirdOptions;
                    cameraController.instance.followTransform = null;
                    print("open Options");
                    if (tipodeobjeto == ObjectType.BIRDSPREFAB)
                    {
                        GetComponentInParent<birdCollection>().birdOptions.SetActive(BirdOptions);
                        GetComponentInParent<birdCollection>().selectedToMove = false;
                        GetComponentInParent<birdCollection>().moving = false;
                    }
                    else
                    {
                        GetComponent<birdCollection>().birdOptions.SetActive(BirdOptions);
                        GetComponent<birdCollection>().selectedToMove = false;
                        GetComponent<birdCollection>().moving = false;
                    }


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
}
