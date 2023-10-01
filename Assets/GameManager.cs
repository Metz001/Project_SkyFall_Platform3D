using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*El Game manager llevará el conteo de puntaje y tendrá los métodos de cambio de puntaje
     * y el cambio de las vidas del jugador. El jugador detectará sus muertes y tendrá conteo de ella. Pero
     * siempre que hay cabio se lo comunicará al Game Manager con un método que le permita cambiar su vida en el UI
     */

    private PlayerController player;
    private Transform checkPoint;
    private float totalScore;


    public static GameManager instance;


    private void Awake()
    {
        instance = this;
        
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        totalScore = 0;
        CheckPoint();

    }


    public void Score(float score)
    {
        totalScore += score;
        Debug.Log("Game manager Socre " + totalScore);
    }
    public void lifeChange(int hearth)
    {
        player.hearthCount += hearth;
        Debug.Log("Game manager hearth " + player.hearthCount);
    }

    public void BackToCheckpoit()
    {
        player.gameObject.transform = checkPoint;      
    }

    public void CheckPoint()
    {
        Debug.Log("Posicion de check point " + checkPoint);
        checkPoint = player.gameObject.transform;
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
