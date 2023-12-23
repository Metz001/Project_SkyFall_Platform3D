using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    /*Solo detectará la colisión y llamará el 
     * método de suma de putaje o vida en el Game manager
     */

    // Start is called before the first frame update
    [SerializeField]
    private int scoreValeu;

    [SerializeField]
    PickUpType type;
    private void Start()
    {
        
        if(type == PickUpType.star)
        {
            scoreValeu = 10;
        }
        else if (type == PickUpType.hearth)
        {
            scoreValeu = 0;
        }
        else
        {
            type = PickUpType.reco;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && type == PickUpType.star)
        {
            GameManager.instance.Score(scoreValeu);
            //Debug.Log("score tag");         
            GameManager.instance.starsUI[GameManager.instance.starCount].SetActive(true);
            GameManager.instance.starCount++;
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            GameManager.instance.HearthChange(+1);
            Destroy(gameObject);
        }
    }


    public enum PickUpType
    {
        star,
        reco,
        hearth,
    }
}
