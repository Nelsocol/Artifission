using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirectorScript : MonoBehaviour
{
    private EnemyClusterScript[] childClusters;

    void Start()
    {
        childClusters = GetComponentsInChildren<EnemyClusterScript>();
    }

    public void DespawnAll(bool playerDeath)
    {
        foreach(EnemyClusterScript enemyCluster in childClusters)
        {
            enemyCluster.DespawnAll(playerDeath);
        }
    }
}
