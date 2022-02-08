using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Enemies.StormPaladin.StormPaladinBehaviors
{
    public class StormPaladinOutOfPositionState : MonoBehaviour, ICreatureState
    {
        public float newMovePollTime;
        private float newMoveTimer;

        public float slashDistance;
        public float boltDistance;
        public float auraDistance;

        public MonoBehaviour outState_Teleport;
        public MonoBehaviour outState_Traverse;
        public MonoBehaviour outState_Slam;


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
                    //outState_Slam,
                };

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
}