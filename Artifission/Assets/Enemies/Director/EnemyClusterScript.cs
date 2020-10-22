using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyClusterScript : MonoBehaviour
{
    private EnemyNodeScript[] childNodes;

    private void Start()
    {
        childNodes = GetComponentsInChildren<EnemyNodeScript>();
    }

    public void DespawnAll(bool playerDeath)
    {
        foreach (EnemyNodeScript enemyNode in childNodes)
        {
            if (playerDeath)
            {
                enemyNode.PlayerDeath();
            }
            else
            {
                enemyNode.DespawnInstance();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            foreach (EnemyNodeScript enemyNode in childNodes)
            {
                enemyNode.SpawnInstance();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DespawnAll(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.8f);
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
