using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameController : MonoBehaviour
{
    [Header("paineis")]
    [SerializeField] private GameObject painelTimes;
    [SerializeField] private GameObject controlBirds,controlNature;
    
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
        }
        else//nature
        {
            controlNature.SetActive(true);
        }
    }
}
