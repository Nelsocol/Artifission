using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormPaladinSlashState : MonoBehaviour, ICreatureState
{
    public MonoBehaviour outState_OutOfPosition;
    public float initialDelay;
    public float secondaryDelay;
    public float tertiaryDelay;
    public float punchForce;

    private bool firstPunch;
    private bool secondPunch;
    private bool thirdPunch;
    private float attackTimer;
    private Rigidbody2D thisRigidbody;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if (!firstPunch && attackTimer > initialDelay)
        {
            Punch(context);
            firstPunch = true;
        }
        else if (!secondPunch && attackTimer > initialDelay + secondaryDelay)
        {
            Punch(context);
            secondPunch = true;
        }
        else if (!thirdPunch && attackTimer > initialDelay + secondaryDelay + tertiaryDelay)
        {
            Punch(context);
            thirdPunch = true;
            context.brainReference.ChangeState(outState_OutOfPosition as ICreatureState);
        }

        attackTimer += Time.deltaTime;
    }

    public void InitateState(StateContext context)
    {
        firstPunch = secondPunch = thirdPunch = false;
        attackTimer = 0;
        thisRigidbody = context.creatureReference.GetComponent<Rigidbody2D>();
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }

    private void Punch(StateContext context)
    {
        int directionMod = context.playerReference.transform.position.x < context.creatureReference.transform.position.x ? -1 : 1;
        this.thisRigidbody.AddForce(new Vector2(directionMod, 0) * punchForce, ForceMode2D.Impulse);
    }
}
