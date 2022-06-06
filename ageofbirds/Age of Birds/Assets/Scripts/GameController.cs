using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject HUD_birds;

    [SerializeField] private Text specieNumberTXT, repTxt,LvlTxt,StrenghtTxt,SpecieNameTxt,SpecialHabilityTxt;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void SetHudValues()//valores da HUD
    {
        Habilities hab = FindObjectOfType<cameraController>().followTransform.GetComponent<birdCollection>().habilidadeEspecial;
        SpecieNameTxt.text = FindObjectOfType<cameraController>().followTransform.GetComponent<birdCollection>().Specie_Name.ToString();
        specieNumberTXT.text = FindObjectOfType<cameraController>().followTransform.GetComponent<birdCollection>().birdsCount.ToString() + " aves";
        LvlTxt.text = "Lvl " + FindObjectOfType<cameraController>().followTransform.GetComponent<birdCollection>().specieLevel.ToString();
        repTxt.text = "Reprodução: " + FindObjectOfType<cameraController>().followTransform.GetComponent<birdCollection>().specie_reprodution.ToString() + "/100";
        StrenghtTxt.text = "Força: " + FindObjectOfType<cameraController>().followTransform.GetComponent<birdCollection>().Specie_strenght.ToString() + "/100";
        SpecialHabilityTxt.text = "Habilidade: " + FindObjectOfType<cameraController>().followTransform.GetComponent<birdCollection>().specie_hability_force.ToString() + "/100";

        switch (hab)
        {
            case Habilities.Fire:

                break;
            case Habilities.Speed:

                break;
            case Habilities.Bomb:

                break;
        }
    }
}
