using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGoblinStrikeState : MonoBehaviour, ICreatureState
{
    public MonoBehaviour outState_Position;
    public UnifiedHitData hitData;
    public float strikeDelay;
    public float strikeRange;

    private float strikeTimer;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if (strikeTimer <= 0)
        {
            hitData.hitSource = transform.position;
            context.playerReference.GetComponent<PlayerStatBindings>().TakeHit(hitData, 1);
            context.brainReference.ChangeState(outState_Position as ICreatureState);
        }

        if ((context.playerReference.transform.position - transform.position).magnitude > strikeRange)
        {
            context.brainReference.ChangeState(outState_Position as ICreatureState);
        }

        strikeTimer -= Time.deltaTime;
    }

    public void InitateState(StateContext context)
    {
        strikeTimer = strikeDelay;
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
