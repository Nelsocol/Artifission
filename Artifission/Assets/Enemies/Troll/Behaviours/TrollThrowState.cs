using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollThrowState : MonoBehaviour, ICreatureState
{
    public float throwChargeTime;
    public GameObject rockObject;
    public float throwStrength;
    public MonoBehaviour outState_Alert;
    public float arcScalar;

    private float throwTimer;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if (throwTimer <= 0)
        {
            GameObject spear = Instantiate(rockObject, transform.position, Quaternion.identity);
            spear.GetComponent<Rigidbody2D>().velocity = (context.playerReference.transform.position - transform.position).normalized * throwStrength + new Vector3(0, arcScalar, 0);
            context.brainReference.ChangeState(outState_Alert as ICreatureState);
        }
        throwTimer -= Time.deltaTime;
    }

    public void InitateState(StateContext context)
    {
        throwTimer = throwChargeTime;
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
