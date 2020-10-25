using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateContext : MonoBehaviour
{
    public GameObject playerReference;
    public GameObject creatureReference;
    public StandardEnemyBindings creatureBindings;
    public CreatureBrainCore brainReference;

    private void Start()
    {
        creatureReference = transform.parent.gameObject;
        creatureBindings = creatureReference.GetComponent<StandardEnemyBindings>();

        EnemyDirectorScript directorScript;
        Transform enemyDirector = transform.parent;
        while (!enemyDirector.gameObject.TryGetComponent(out directorScript))
        {
            enemyDirector = enemyDirector.parent;
        }

        playerReference = directorScript.playerReference;
        brainReference = GetComponent<CreatureBrainCore>();
    }

    public void UpdateContext()
    {

    }
}
