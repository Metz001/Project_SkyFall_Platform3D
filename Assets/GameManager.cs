using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float totalScore;

    public int hearthCount; //Conteo de corazones
    public int starCount; //Conteo de estrellas

    public Vector3 lastCheckpointPos; //almacena posicion de checkpoint

    public static GameManager instance; //Singleton instacnia

    
    //Singleton
    
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
        print(instance);
       
    }
    

    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;
        player = FindObjectOfType<PlayerController>();
       
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
              
        //Si el jugador cae, llama chekpoint
        if(player.gameObject.transform.position.y < -50f)
        {
            BackToCheckpoit();
            HearthChange(-1);
        }
    }

    public void Score(float score)
    {
        totalScore += score;

        Debug.Log("Game manager Socre " + totalScore);
    }

    //Cambio de CORAZONES
    public void HearthChange(int hearth)
    {
        hearthCount+=hearth;
        Debug.Log("quedan " + hearthCount + " corazones");
        //de no quedar  corazones, llama Game Over
        if (hearthCount < 0){
            GameOver();
        }
    }   

    ////Cambio de VIDA
    public void HealthChange(int health)
    {
        
        player.health += health; //cambio
        Debug.Log("Game manager hearth " + player.health);
        //de no quedar  vida, resta un corazón
        if (health < 0) {
            HearthChange(-1);
        }
    }

    //FIN DEL JUEGO
    private void GameOver()
    {
        throw new NotImplementedException();
    }

    public void BackToCheckpoit()
    {
        //Devuelve al jugadro al ultimo checkpoint
        instance.player.gameObject.transform.position = instance.lastCheckpointPos;
        Debug.Log("Game Manager BTC "+ instance.lastCheckpointPos);
        
    }

    /*
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           BackToCheckpoit();
           Score(-1);
        }
    }*/
    
    public enum GameState
    {
        Menu,
        Playing,
        Pause,
        Lose,
        Win
    }
}
