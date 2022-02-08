using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormPaladinCenteredState : MonoBehaviour, ICreatureState
{
    public float newMovePollTime;
    private float newMoveTimer;

    public float slashDistance;
    public float auraDistance;

    public MonoBehaviour outState_Teleport;
    public MonoBehaviour outState_Traverse;
    public MonoBehaviour outState_SkyStrike;
    public MonoBehaviour outState_Slash;
    public MonoBehaviour outState_Aura;


    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if (newMoveTimer > newMovePollTime)
        {
            newMoveTimer = 0;

            // Assemble move options
            List<MonoBehaviour> moveOptions = new List<MonoBehaviour>()
            {
                outState_Teleport,
                outState_Traverse,
                //outState_SkyStrike,
            };

            float distanceFromPlayer = Vector2.Distance(context.creatureReference.transform.position, context.playerReference.transform.position);

            if (distanceFromPlayer < slashDistance)
            {
                moveOptions.Add(outState_Slash);
            }

            if (distanceFromPlayer < auraDistance)
            {
                //moveOptions.Add(outState_Aura);
            }

            // Randomly pick the next state
            int moveSelection = Random.Range(0, moveOptions.Count);
            context.brainReference.ChangeState(moveOptions[moveSelection] as ICreatureState);
        }
        
        newMoveTimer += Time.deltaTime;
    }

    public void InitateState(StateContext context)
    {
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
