using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameController : MonoBehaviour
{
    [Header("paineis")]
    [SerializeField] private GameObject painelTimes;
    [SerializeField] private GameObject controlBirds,controlNature;
    [SerializeField] private Slider sliderBirds,sliderNature;

    private bool TimerIsActive;
    
    void Start()
    {
        painelTimes.SetActive(true);
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void ChooseTeamSinglePlayer(int team)
    {
        if(team == 0)//bird
        {
            controlBirds.SetActive(true);
            TimerIsActive = true;
            StartSingleplayerMission(0);
        }
        else//nature
        {
            controlNature.SetActive(true);
            TimerIsActive = true;
            StartSingleplayerMission(1);
        }
    }

    public void StartSingleplayerMission(int mission)
    {
        if (mission == 0)//bird
        {
          
        }
        else//nature
        {
          
        }
    }
}
