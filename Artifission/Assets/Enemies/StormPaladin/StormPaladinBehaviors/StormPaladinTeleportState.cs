using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Enemies.StormPaladin.StormPaladinBehaviors
{
    public class StormPaladinTeleportState : MonoBehaviour, ICreatureState
    {
        public float teleportDelay;
        private float teleportTimer;

        public GameObject teleportAOEObject;
        public GameObject destinationAOEObject;
        public ControlPointBindings controlPoints;

        public MonoBehaviour outState_Center;
        public MonoBehaviour outState_Edge;

        private Transform controlPointSelection;

        public void ExecuteState(StateContext context, List<StateMessages> messages)
        {
            if (teleportTimer > teleportDelay)
            {
                teleportTimer = 0;
                Instantiate(teleportAOEObject, context.creatureReference.transform.position, Quaternion.identity);

                
                context.creatureReference.transform.SetPositionAndRotation(controlPointSelection.position, Quaternion.identity);
                
                // Switch back to centered or edge state.
                if (controlPoints.ControlPoints().IndexOf(controlPointSelection) == 1)
                {
                    context.brainReference.ChangeState(outState_Center as ICreatureState);
                }
                else
                {
                    context.brainReference.ChangeState(outState_Edge as ICreatureState);
                }    
            }

            teleportTimer += Time.deltaTime;
        }

        public void InitateState(StateContext context)
        {
            controlPointSelection = controlPoints.FarControlPoints(context.creatureReference.transform.position)[Random.Range(0, controlPoints.ControlPoints().Count-1)];
            Instantiate(destinationAOEObject, controlPointSelection, false);
        }

        public bool QueryValidity(StateContext context, List<StateMessages> messages)
        {
            return false;
        }
    }
}