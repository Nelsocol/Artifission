using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StormPaladinTraverseState : MonoBehaviour, ICreatureState
{
    public float traverseSpeed;
    public float traverseDelay;
    private float traverseTimer;
    private Transform controlPointSelection;

    public ControlPointBindings controlPoints;

    public MonoBehaviour outState_Center;
    public MonoBehaviour outState_Edge;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if (traverseTimer > traverseDelay)
        {
            if (Mathf.Abs(controlPointSelection.position.x - context.creatureReference.transform.position.x) < (traverseSpeed * Time.deltaTime))
            {
                context.creatureReference.transform.position = new Vector3(controlPointSelection.position.x, context.creatureReference.transform.position.y, context.creatureReference.transform.position.z);

                if (controlPoints.ControlPoints().IndexOf(controlPointSelection) == 1)
                {
                    context.brainReference.ChangeState(outState_Center as ICreatureState);
                }
                else
                {
                    context.brainReference.ChangeState(outState_Edge as ICreatureState);
                }
            }
            else
            {
                if (context.creatureReference.transform.position.x > controlPointSelection.position.x)
                {
                    context.creatureReference.transform.position += new Vector3(-traverseSpeed * Time.deltaTime, 0, 0);
                }
                else 
                {
                    context.creatureReference.transform.position += new Vector3(traverseSpeed * Time.deltaTime, 0, 0);
                }
            }
        }
        else
        {
            traverseTimer += Time.deltaTime;
        }
    }

    public void InitateState(StateContext context)
    {
        controlPointSelection = controlPoints.FarControlPoints(context.creatureReference.transform.position)[Random.Range(0, controlPoints.FarControlPoints(context.creatureReference.transform.position).Count)];
        traverseTimer = 0;
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
