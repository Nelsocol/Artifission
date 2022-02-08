using Assets.CutsceneController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneControllerScript : MonoBehaviour
{
    public CutsceneStep StartStep;
    public GameObject CameraReference;

    public bool seen = false;
    public void StartCutscene() 
    {
        CutsceneContext newContext = new CutsceneContext();
        newContext.Camera = CameraReference;
        newContext.Camera.GetComponent<CameraPlayerTracking>().state = CameraState.CUTSCENE;

        StartStep.Execute(newContext);
    }

    public void CloseCutscene() 
    {
        CameraReference.GetComponent<CameraPlayerTracking>().state = CameraState.TRACKING;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!seen) {
            seen = true;
            StartCutscene();
        }     
    }
}
