using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManagerSingleplayer : MonoBehaviour
{
    [Header("Painel Times")]
    public Text timePassaros;
    public Text timeNatureza,subtitlePassaro,subtitleNature,objective1Passaro,objective2Passaro,objective1Nature,objective2Nature,legendaSlider,legendaSlider2,venceu1,venceu2,derrota1,derrota2,sair1,sair2, txtWait;
    public Text sairMessage, sim, nao, practice1,practice2,practice3,practice4,exit1,exit2,frasepracticewin,frasepracticefail,aguardeJogador;

    private string nickName;

    private void Start()
    {
        Translate();

    }
    public void Translate()
    {
        if (PlayerPrefs.GetInt("language") == 1)
        {
            timePassaros.text = "BIRDS";
            timeNatureza.text = "NATURE";
            legendaSlider.text = "SURVIVE";
            legendaSlider2.text = "DEFEAT THE BIRDS";
            venceu1.text = "VICTORY";
            venceu2.text = "VICTORY";
            derrota1.text = "DEFEAT";
            derrota2.text = "DEFEAT";
            sair1.text = "EXIT";
            sair2.text = "EXIT";
            subtitlePassaro.text = "SURVIVE AGAINST NATURE";
            subtitleNature.text = "ELIMINATE THE BIRD SPECIE";
            objective1Passaro.text = "COLLECT:";
            objective2Passaro.text = "AVOID:";
            objective1Nature.text = "PLACE IT:";
            objective2Nature.text = "DESTROY:";
            txtWait.text = "Waiting for players...";
            sairMessage.text = "Are you sure you want to leave?";
            sim.text = "YES";
            nao.text = "NO";
            practice1.text = "PRACTICE FINISHED";
            practice2.text = "PRACTICE FINISHED";
            practice3.text = "PRACTICE FAILED";
            practice4.text = "PRACTICE FAILED";
            exit1.text = "EXIT";
            exit2.text = "EXIT";
            frasepracticefail.text = "You didn�t completed the objective";
            frasepracticewin.text = "You completed the objective";
            aguardeJogador.text = "WAIT FOR THE OTHER PLAYER";
        }
        else
        {
            timePassaros.text = "P�ssaros";
            timeNatureza.text = "Natureza";
            legendaSlider2.text = "Derrote os p�ssaros";
            legendaSlider.text = "sobreviva pela maior quantidade de tempo";
            venceu1.text = "Vit�ria";
            venceu2.text = "Vit�ria";
            derrota1.text = "Derrota";
            derrota2.text = "Derrota";
            sair1.text = "Sair";
            sair2.text = "Sair";
            subtitlePassaro.text = "Sobreviva contra a natureza";
            subtitleNature.text = "Elimine a esp�cie dos p�ssaros";
            objective1Passaro.text = "Colete:";
            objective2Passaro.text = "Evite:";
            objective1Nature.text = "Posicione:";
            objective2Nature.text = "Destrua:";
            txtWait.text = "Aguardando jogadores...";
            sairMessage.text = "Voc� tem certeza que deseja sair?";
            sim.text = "SIM";
            nao.text = "N�O";
            practice1.text = "TREINO CONCLU�DO";
            practice2.text = "TREINO CONCLU�DO";
            practice3.text = "FIM DE TREINO";
            practice4.text = "FIM DE TREINO";
            exit1.text = "SAIR";
            exit2.text = "SAIR";
            frasepracticefail.text = "Voc� falhou ao completar o objetivo";
            frasepracticewin.text = "Voc� completou o objetivo";
            aguardeJogador.text = "Espere o outro jogador";
        }
    }
}
