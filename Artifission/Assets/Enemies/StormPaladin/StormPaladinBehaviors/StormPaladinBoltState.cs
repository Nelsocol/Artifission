using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Enemies.StormPaladin.StormPaladinBehaviors
{
    public class StormPaladinBoltState : MonoBehaviour, ICreatureState
    {
        public float boltDelay;
        private float boltTimer;

        public GameObject boltObject;

        public MonoBehaviour outState_Edge;

        public void ExecuteState(StateContext context, List<StateMessages> messages)
        {
            if (boltTimer > boltDelay)
            {
                boltTimer = 0;

                Vector2 boltDirection = new Vector2(1, 0);
                if (context.creatureReference.transform.position.x > context.playerReference.transform.position.x)
                {
                    boltDirection = new Vector2(-1, 0);
                }

                RaycastHit2D boltCast = Physics2D.Raycast(context.creatureReference.transform.position, boltDirection);
                PlayerStatBindings playerBindings = null;
                if (boltCast && boltCast.collider.gameObject.TryGetComponent(out playerBindings))
                {
                    // Deal damage to player
                }

                // Spawn and resize bolt object
                GameObject boltInstance = Instantiate(boltObject, context.creatureReference.transform);
                if (context.creatureReference.transform.position.x > context.playerReference.transform.position.x)
                {
                    boltInstance.transform.localScale = new Vector3(1, -1, 1);
                }

                context.brainReference.ChangeState(outState_Edge as ICreatureState);
            }

            boltTimer += Time.deltaTime;
        }

        public void InitateState(StateContext context)
        {
            boltTimer = 0;
        }

        public bool QueryValidity(StateContext context, List<StateMessages> messages)
        {
            return false;
        }
    }
}