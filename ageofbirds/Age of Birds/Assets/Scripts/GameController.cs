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

    public void SetHudValues(string SpecieName,int SpeciePopulation,int SpecieLevel, int Strenght, int Reprodution, int habilityLevel, Habilities hab)
    {
        SpecieNameTxt.text = SpecieName;
        specieNumberTXT.text = SpeciePopulation.ToString() + " aves";
        LvlTxt.text = "Lvl " + SpecieLevel.ToString();
        repTxt.text = Reprodution.ToString() + "/100";
        StrenghtTxt.text = Strenght.ToString() + "/100";
        SpecialHabilityTxt.text = habilityLevel.ToString() + "/100";

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
