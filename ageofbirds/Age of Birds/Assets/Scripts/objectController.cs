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
    private bool BirdOptions;
    private void OnMouseDown()
    {
        if (tipodeobjeto != ObjectType.terreno)
        {
            cameraController.instance.followTransform = transform;
        }
        if(tipodeobjeto != ObjectType.passaros && cameraController.instance.followTransform.GetComponent<birdCollection>().isSelected == false)
        {
           cameraController.instance.followTransform = null;
           BirdOptions = false;
           FindObjectOfType<birdCollection>().birdOptions.SetActive(BirdOptions);

            
        }
        if (tipodeobjeto != ObjectType.passaros && cameraController.instance.followTransform.GetComponent<birdCollection>().isSelected == true)
        {
            cameraController.instance.followTransform.GetComponent<birdCollection>().MoveTo();
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1) && tipodeobjeto == ObjectType.terreno)
        {
            BirdOptions = false;
            FindObjectOfType<birdCollection>().birdOptions.SetActive(false);
            cameraController.instance.followTransform = null;
        }


        if(Input.GetMouseButtonDown(1) && tipodeobjeto != ObjectType.terreno)//right click
        {
            BirdOptions = !BirdOptions;
            cameraController.instance.followTransform = transform;
            //abrir painel
            //cameraController.instance.followTransform = null;
            print("open Options");
            FindObjectOfType<birdCollection>().birdOptions.SetActive(BirdOptions);
        }
    }
}
