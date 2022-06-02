using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectType
{
    terreno, passaros
}
public class objectController : MonoBehaviour
{
    public ObjectType tipodeobjeto;
    [HideInInspector]public bool BirdOptions;
    private Vector3 clickPOS;
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
            if (tipodeobjeto != ObjectType.passaros && cameraController.instance.followTransform.GetComponent<birdCollection>().isSelected == false)
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
            BirdOptions = !BirdOptions;
            //abrir painel
            cameraController.instance.followTransform = null;
            print("open Options");
            FindObjectOfType<birdCollection>().birdOptions.SetActive(BirdOptions);
            FindObjectOfType<birdCollection>().isSelected = false;
            FindObjectOfType<birdCollection>().moving = false;
        }
    }
}
