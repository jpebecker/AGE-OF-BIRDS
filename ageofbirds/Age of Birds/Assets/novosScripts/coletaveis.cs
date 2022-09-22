using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coletaveis : MonoBehaviour
{
    public bool IsInactive = false;

    private void Update()
    {
        if (IsInactive && gameObject.transform.localScale.x <= 5 && gameObject.transform.localScale.y <= 5 && gameObject.transform.localScale.z <=5)
        {
           gameObject.transform.localScale += new Vector3(0.5f * Time.deltaTime, 0.5f * Time.deltaTime, 0.5f * Time.deltaTime);
        }
    }
}
