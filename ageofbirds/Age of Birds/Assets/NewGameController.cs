using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameController : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void ChooseTeamSinglePlayer(int team)
    {
        if(team == 0)//bird
        {
            print("bird");
        }
        else//nature
        {
            print("nature");
        }
    }
}
