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


    private float totalScore;   

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
        player = GameObject.FindObjectOfType<PlayerController>();

        totalScore = 0;
        //CheckPoint();
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("C is pressed");
        }
        if (Input.GetKey(KeyCode.F))
        {
            player.gameObject.transform.position = lastCheckpointPos;
            Debug.Log("F is pressed");
        }
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
    public void hearthChange(int hearth)
    {
        //player.hearthCount += hearth;
        player.hearthCount += hearth;
        Debug.Log("Game manager hearth " + player.hearthCount);
    }

    public void BackToCheckpoit()
    {
        player.gameObject.transform.position = lastCheckpointPos;
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
