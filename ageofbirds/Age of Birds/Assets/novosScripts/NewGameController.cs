using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameController : MonoBehaviour
{
    [Header("paineis")]
    [SerializeField] private GameObject painelTimes;
    [SerializeField] private GameObject controlBirds,controlNature,gameOverPanel,winPanel;
    [SerializeField] private Slider sliderBirds,sliderNature;

    [Header("Codes")]
    [SerializeField] private bird passaro;
    [SerializeField] private IncisivePlay nature;
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

        if (TimerIsActive)
        {
            sliderBirds.value -= Time.deltaTime;
        }

        if(sliderBirds.value <= 0)
        {
            
        }
    }

    public void ChooseTeamSinglePlayer(int team)
    {
        if(team == 0)//bird
        {
            controlBirds.SetActive(true);
            TimerIsActive = true;
            passaro.IsPlaying = true;
            StartSingleplayerMission(0);
        }
        else//nature
        {
            controlNature.SetActive(true);
            TimerIsActive = true;
            nature.isPlaying = true;
            StartSingleplayerMission(1);
        }
    }

    public void StartSingleplayerMission(int mission)
    {
        if (mission == 0)//bird
        {
            TimerIsActive = true;
        }
        else//nature
        {
          
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Win()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
