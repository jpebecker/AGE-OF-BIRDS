using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectController : MonoBehaviour
{
    private void Start()
    {
        cameraController.instance.followTransform = transform;
    }
}
