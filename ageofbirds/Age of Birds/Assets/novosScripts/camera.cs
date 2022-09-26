using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{

    public Vector3 offSet;

    private Vector3 Change;

    public float Speed = 0.4f;
    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, transform.position, ref Change, Speed);
    }

}
