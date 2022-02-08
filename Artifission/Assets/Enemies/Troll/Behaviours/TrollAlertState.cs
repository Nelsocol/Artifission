using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollAlertState : MonoBehaviour, ICreatureState
{
    public MonoBehaviour outState_Throw;
    public MonoBehaviour outState_Punch;
    public MonoBehaviour outState_Slam;
    public MonoBehaviour outState_Leap;
    public MonoBehaviour outState_Walk;

    public float slamRange;
    public float punchRange;
    public float farRange;
    public float abilityPollRate;
    public float abilityTriggerChance;
    public float abilityCooldown;

    private float abilityTimer;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        Vector2 playerDelta = context.playerReference.transform.position - context.creatureReference.transform.position;
        float distanceToPlayer = playerDelta.magnitude;

        List<MonoBehaviour> validAbilities = new List<MonoBehaviour>() { outState_Walk };

        if (distanceToPlayer < slamRange)
        {
            validAbilities.Add(outState_Slam);
        }

        if (distanceToPlayer < punchRange)
        {
            validAbilities.Add(outState_Punch);
        }

        if (distanceToPlayer < farRange)
        {
            validAbilities.Add(outState_Leap);
        }

        if (distanceToPlayer > punchRange && distanceToPlayer < farRange)
        {
            validAbilities.Add(outState_Throw);
        }

        if(abilityTimer <= 0)
        {
            abilityTimer = abilityPollRate;
            if (Random.value < abilityTriggerChance)
            {
                MonoBehaviour chosenAbility = validAbilities[Random.Range(0, validAbilities.Count)];
                context.brainReference.ChangeState(chosenAbility as ICreatureState); 

                if (chosenAbility == outState_Slam)
                {
                    context.creatureAnimator.SetTrigger("SlamTrigger");
                }
                else if (chosenAbility == outState_Punch)
                {
                    context.creatureAnimator.SetTrigger("PunchTrigger");
                }
            }
        }

        abilityTimer -= Time.deltaTime;
    }

    public void InitateState(StateContext context)
    {
        abilityTimer = abilityCooldown;
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
