using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
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

    //El machetazo
    private bool allowHearthChange = true;
    private System.Threading.Timer hearthChangeTimer;


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
            hearthCount++;
            hearthsUI[i].SetActive(true);
        }

        hearthChangeTimer = new System.Threading.Timer(TemporizadorCallback, null, Timeout.Infinite, Timeout.Infinite);
    }

    private void Update()
    {
              
        //Si el jugador cae, llama chekpoint
        if(player.gameObject.transform.position.y < -50)
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

    private void TemporizadorCallback(object state)
    {
        // Habilita la posibilidad de llamar a HearthChange después de que ha pasado 1 segundo
        allowHearthChange = true;
    }
    //Cambio de Conteo CORAZONES
    public void HearthChange(int hearth)
    {
        //Solución machetera
        if (!allowHearthChange)
        {
            Debug.Log("Espera 1 segundo entre llamadas a HearthChange.");
            return;
        }

        // Desactiva el temporizador y establece el retraso de 1 segundo
        hearthChangeTimer.Change(1000, Timeout.Infinite);

        allowHearthChange = false;
        //fin solución
        hearthCount += hearth;
        HearthUiCahnge(hearthCount);
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

    //Ui CORAZONES
    private void HearthUiCahnge(int hearthCountUi)
    {
        switch (hearthCountUi)
        {
            case 0:
                hearthsUI[0].SetActive(false);
                hearthsUI[1].SetActive(false);
                hearthsUI[2].SetActive(false);            
                break;

            case 1:
                hearthsUI[0].SetActive(false);
                hearthsUI[1].SetActive(false);
                hearthsUI[2].SetActive(true);
                break;

            case 2:
                hearthsUI[0].SetActive(false);
                hearthsUI[1].SetActive(true);
                hearthsUI[2].SetActive(true);
                break;

            case 3:
                hearthsUI[0].SetActive(true);
                hearthsUI[1].SetActive(true);
                hearthsUI[2].SetActive(true);
                break;

            default:
                Console.WriteLine("El valor no coincide con ningún caso");
                break;
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
