using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class birdOptionsController : MonoBehaviour
{
    [SerializeField]private GameObject PlayerOptionsPrefab,EnemieOptionsPrefab;
    private PhotonView pv;
    private GameObject birdOptions;
    private Button[] options;

    void Awake()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            birdOptions = Instantiate(EnemieOptionsPrefab, gameObject.transform.position,gameObject.transform.rotation);
            GetComponent<birdCollection>().birdOptions = birdOptions;
            print("bot");
        }
        else
        {
            birdOptions = Instantiate(PlayerOptionsPrefab, gameObject.transform.position,gameObject.transform.rotation);
            GetComponent<birdCollection>().birdOptions = birdOptions;
            birdOptions.transform.SetParent(gameObject.transform);
            print("player");
        }
     
    }

    private void Start()
    {
        options = birdOptions.GetComponentsInChildren<Button>();
        if (!pv.IsMine)//funcoes do botao A do inimigo
        {
            options[0].onClick.AddListener(delegate { GetComponent<birdCollection>().AttackEnemie(birdOptions.transform.parent.position); });
        }
        else//funcoes do botao A do player
        {
            options[0].onClick.AddListener(GetComponent<birdCollection>().ToggleMove);
            options[1].onClick.AddListener(GetComponent<birdCollection>().ToggleAttack);
        }
    
    }
}
