using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coletaveis : MonoBehaviour
{
    public bool IsInactive = false;
    public int type;

    private Vector2 centro;
    private float angulo;
    private Transform player;
    public void Start()
    {
        if(type == 0)//bushes and water
        {
            Destroy(gameObject, 10f);
        }
        else
        {
            Destroy(gameObject, 20f);
        }
        if(type == 2)//tornado
        {
            centro = transform.position;
        }

        if(type == 3)//predator
        {
            player = FindObjectOfType<bird>().gameObject.transform;   
        }
    }
    private void Update()
    {
        if (IsInactive && gameObject.transform.localScale.x <= 5 && gameObject.transform.localScale.y <= 5 && gameObject.transform.localScale.z <=5 && type ==0)//coletaveis normais
        {
           gameObject.transform.localScale += new Vector3(0.5f * Time.deltaTime, 0.5f * Time.deltaTime, 0.5f * Time.deltaTime);
        }

        if(type == 1)//fire
        {
            gameObject.transform.localScale += new Vector3(0.25f * Time.deltaTime, 0.25f * Time.deltaTime, 0.25f * Time.deltaTime);
        }

        if(type == 2)//tornado
        {
            angulo += Random.Range(6,8) * Time.deltaTime;

            var borda = new Vector2(Mathf.Sin(angulo), Mathf.Cos(angulo)) * 1f;

            transform.position = centro + borda;

            gameObject.transform.localScale += new Vector3(0.2f * Time.deltaTime, 0.2f * Time.deltaTime, 0.2f * Time.deltaTime);
        }

        if(type == 3)//predador
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, Random.Range(3, 6) * Time.deltaTime);
        }
        
    }
}
