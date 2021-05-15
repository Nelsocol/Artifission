using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyClusterScript : MonoBehaviour
{
    private EnemyNodeScript[] childNodes;
    private Collider2D thisCollider;

    private void Start()
    {
        childNodes = GetComponentsInChildren<EnemyNodeScript>();
        thisCollider = GetComponent<Collider2D>();
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

        List<Collider2D> overlappingColliders = new List<Collider2D>();
        thisCollider.OverlapCollider(new ContactFilter2D(), overlappingColliders);
        if(overlappingColliders.Exists(e => e.tag == "Player"))
        {
            OnTriggerEnter2D(overlappingColliders.First(e => e.tag == "Player"));
        }
    }

    public void PropogateMessage(StateMessages message)
    {
        foreach (EnemyNodeScript node in childNodes)
        {
            node.SendMesasgeToCreature(message);
        }
    }

    public List<GameObject> GetAllLivingCreatures()
    {
        return childNodes.Select(e => e.GetCreature()).Where(e => e != null).ToList();
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
