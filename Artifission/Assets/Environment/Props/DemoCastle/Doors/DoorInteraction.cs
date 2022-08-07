using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : StandardInteractable
{
    public GameObject pairedDoor;
    public MonoBehaviour mainCameraReference;

    protected override void Interact(GameObject player) {
        player.transform.position = pairedDoor.transform.position;
        (mainCameraReference as CameraPlayerTracking).RecenterCamera();
    }
}
