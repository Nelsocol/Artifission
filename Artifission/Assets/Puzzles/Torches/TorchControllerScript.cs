using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchControllerScript : MonoBehaviour
{
    public MonoBehaviour successScript;
    public bool allOnForSuccess = true;

    private List<TorchScript> torchSet = new List<TorchScript>();
    private bool succeeded = false;

    void Start()
    {
        foreach (TorchScript torch in GetComponentsInChildren<TorchScript>())
        {
            torchSet.Add(torch);
        }
    }

    void Update()
    {
        if(!succeeded && !torchSet.Exists(e => (e.lit && !e.mustLight) || (!e.lit && e.mustLight)) && successScript != null)
        {
            successScript.enabled = true;
            succeeded = true;

            if (allOnForSuccess)
            {
                foreach(TorchScript torch in torchSet)
                {
                    torch.mustNotLight = false;
                    torch.dependsOn = null;
                    torch.sustainTime = 0;
                    torch.Light();
                }
            }
        }
    }

    public void ExtinguishAll()
    {
        foreach(TorchScript torch in torchSet)
        {
            torch.Extinguish();
        }
    }
}
