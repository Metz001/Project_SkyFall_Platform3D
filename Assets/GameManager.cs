using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*El Game manager llevará el conteo de puntaje y tendrá los métodos de cambio de puntaje
     * y el cambio de las vidas del jugador. El jugador detectará sus muertes y tendrá conteo de ella. Pero
     * siempre que hay cabio se lo comunicará al Game Manager con un método que le permita cambiar su vida en el UI
     */

    public PlayerController player;

    public GameObject[] starsUI = new GameObject[9];
    public GameObject[] hearthsUI = new GameObject[2];
    private float totalScore;

    public int hearthCount;
    public int starCount;

    public Vector3 lastCheckpointPos;

    public static GameManager instance;

    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }


    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;
        player = GameObject.FindObjectOfType<PlayerController>();
        /*
        for (int i = 0; i > hearthsUI.Length; i++) {

            hearthsUI[i] = GameObject.FindGameObjectsWithTag("hearth")[i]; 

        }*/
        for (int i = 0; i < starsUI.Length; i++)
        {
            starsUI[i].gameObject.SetActive(false);
        }

        
        for (int i = 0;i < hearthsUI.Length; i++)
        {
            hearthCount = i;
        } 
        
        //CheckPoint();
        
    }

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.G))
        {
            BackToCheckpoit();
        }
    }

    public void Score(float score)
    {
        totalScore += score;

        Debug.Log("Game manager Socre " + totalScore);
    }
    public void HearthChange()
    {

    }
    public void helthChange(int health)
    {
        //player.hearthCount += hearth;
        player.health += health;
        Debug.Log("Game manager hearth " + player.health);
    }

    public void BackToCheckpoit()
    {
        player.gameObject.transform.position = lastCheckpointPos;
        Debug.Log(lastCheckpointPos);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           BackToCheckpoit();
           Score(-1);
        }
    }

    public enum GameState
    {
        Menu,
        Playing,
        Pause,
        Lose,
        Win
    }
}
