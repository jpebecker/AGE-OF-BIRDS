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
    private void OnMouseDown()
    {
        if(tipodeobjeto != ObjectType.terreno)
        {
            cameraController.instance.followTransform = transform;
        }
        else
        {
            cameraController.instance.followTransform = null;
        }
        
    }
}
