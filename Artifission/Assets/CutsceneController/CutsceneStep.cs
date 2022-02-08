using Assets.CutsceneController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneStep : MonoBehaviour
{
    public float MoveTime;
    public float HoldTime;
    public ICutsceneBehavior CustomBehavior;
    public CutsceneStep NextStep;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 delta;
    private float currentTime = 0;
    private bool active = false;
    private CutsceneContext context;

    public void Execute(CutsceneContext newContext) 
    {
        context = newContext;
        startPosition = context.Camera.transform.position;
        endPosition = this.transform.position;
        delta = (endPosition - startPosition) / MoveTime;

        CustomBehavior?.Execute();
        active = true;
    }

    public void Update()
    {
        if (active) {
            if (currentTime < MoveTime) {
                context.Camera.transform.position += (Vector3)delta * Time.deltaTime;
            }

            if (currentTime > MoveTime + HoldTime) {
                active = false;

                if (NextStep == null)
                {
                    transform.parent.GetComponent<CutsceneControllerScript>().CloseCutscene();
                }
                else 
                {
                    NextStep?.Execute(context);
                }
            }
            currentTime += Time.deltaTime;
        }
    }
}
