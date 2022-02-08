using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnNodeType
{
    Continuous,
    Respawning,
    Persistent,
    SingleSpawn
}


public class EnemyNodeScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public bool eligible = true;
    public bool instanceKilled = false;
    public SpawnNodeType nodeType;
    public float timeInterval;

    private GameObject enemyInstance;
    private bool spawned = false;
    private float timeElapsed;

    private void Update()
    {
        if(instanceKilled && nodeType == SpawnNodeType.Continuous)
        {
            if(timeElapsed < 0)
            {
                spawned = false;
                SpawnInstance();
                timeElapsed = timeInterval;
            }
            timeElapsed -= Time.deltaTime;
        }
    }

    public void SpawnInstance()
    {
        if (eligible && !spawned)
        {
            enemyInstance = Instantiate(enemyPrefab, transform);
            enemyInstance.transform.localPosition = Vector3.zero;
            instanceKilled = false;
            spawned = true;
        }
    }

    public void DespawnInstance()
    {
        if (spawned)
        {
            if (!instanceKilled)
            {
                Destroy(enemyInstance);
            }
            else
            {
                if (nodeType == SpawnNodeType.SingleSpawn || nodeType == SpawnNodeType.Persistent)
                {
                    eligible = false;
                }
            }
            spawned = false;
        }
    }

    public void PlayerDeath()
    {
        DespawnInstance();
        if(nodeType == SpawnNodeType.Persistent)
        {
            eligible = true;
        }
    }

    public void SendMesasgeToCreature(StateMessages message)
    {
        enemyInstance?.GetComponentInChildren<CreatureBrainCore>()?.SendMessage(message);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.8f);
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

    public GameObject GetCreature()
    {
        return enemyInstance;
    }
}
