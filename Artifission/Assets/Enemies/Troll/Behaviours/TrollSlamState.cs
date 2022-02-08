using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollSlamState : MonoBehaviour, ICreatureState
{
    public MonoBehaviour outState_Alert;
    public UnifiedHitData slamHitData;
    public float slamTime;
    public float slamRange;
    public Vector2 slamOffset;
    public ParticleSystem slamParticles;

    private float slamTimer;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if (slamTimer <= 0)
        {
            slamParticles.Play();
            Collider2D[] slamTargets = Physics2D.OverlapCircleAll((Vector2)context.creatureReference.transform.position + slamOffset, slamRange);
            foreach(Collider2D target in slamTargets)
            {
                PlayerStatBindings playerBindings;
                if (target.gameObject.TryGetComponent(out playerBindings))
                {
                    slamHitData.hitSource = context.creatureReference.transform.position;
                    playerBindings.TakeHit(slamHitData, 1);
                }

                PropScript propBindings;
                if(target.gameObject.TryGetComponent(out propBindings))
                {
                    propBindings.ReceiveImpact(context.creatureReference.transform.position, 10);
                }
            }
            context.brainReference.ChangeState(outState_Alert as ICreatureState);
        }

        slamTimer -= Time.deltaTime;
    }

    public void InitateState(StateContext context)
    {
        slamTimer = slamTime;
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
