using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfEvent
{
    Tornado,Firestorm,
}
public class IncisivePlay : MonoBehaviour
{
    [Header("Configs")]
    public bool isPlaying = false;
    public TypeOfEvent eventoPosicionar;
    public GameObject tornadoPrefab, fireStormPrefab;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && isPlaying)//rightclick
        {
            switch (eventoPosicionar)
            {
                case TypeOfEvent.Tornado:
                    print("positionTOrnado");
              
                    break;
                case TypeOfEvent.Firestorm:
                    print("positionFirestorm");
                    break;
            }
        }
    }

}
