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
        
        if(scoreValeu == 0 && type == PickUpType.star)
        {
            scoreValeu = 1;
        }
        else
        {
            scoreValeu = 0;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && type == PickUpType.star)
        {
            GameManager.instance.Score(scoreValeu);
            //Debug.Log("score tag");
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            GameManager.instance.lifeChange(1);
            Destroy(gameObject);
        }
    }


    public enum PickUpType
    {
        star,
        hearth,
    }
}
