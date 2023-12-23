using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone: MonoBehaviour
{
    public void BackToCheckpoit(GameObject player)
    {
        
        player.gameObject.transform.position = GameManager.instance.lastCheckpointPos;
        Debug.Log("Game Manager BTC " + GameManager.instance.lastCheckpointPos);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BackToCheckpoit(other.gameObject);
            GameManager.instance.Score(-1);
        }
    }

}
