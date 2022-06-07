using UnityEngine;
using UnityEngine.UI;

public class LogConsole : MonoBehaviour
{
    [Header("referencias")]
    [SerializeField] private Text textLog;//texto do console
    [SerializeField] private GameObject consoleLog;//painel do console
    [HideInInspector] public bool mostrarLog = true;//estado do painel

    void Start()//verifica se o painel esta ativo ou inativo
    {
        if (!mostrarLog)
        {
            consoleLog.SetActive(false);
        }
    }

    public void DefinirTexto(string message)//define o text do console
    {
        textLog.text = message;
    }
}
