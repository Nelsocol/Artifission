using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileScript : MonoBehaviour
{
    public RuneRecord boundRune;

    private bool tracking;
    private Vector3 startingPosition;
    private GameObject boundPoint;
    private void Start()
    {
        startingPosition = transform.localPosition;
    }

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                tracking = true;
            }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                Unbind();
            }
        }

        if (tracking && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            tracking = false;

            SlotScript bindingZone = null;
            foreach(Collider2D foundZone in Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), 1).Where(e => e.TryGetComponent(out bindingZone) && bindingZone.tileType == boundRune.type))
            {
                bindingZone.boundTile = gameObject;
                boundPoint = foundZone.gameObject;
                break;
            }
            if(bindingZone == null)
            {
                Unbind();
            }
        }

        if (tracking)
        {
            Vector2 trackingPosition = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position = new Vector3(trackingPosition.x, trackingPosition.y, transform.parent.transform.position.z - 1);
        }
        else 
        {
            if (boundPoint != null)
            {
                transform.localPosition = boundPoint.transform.localPosition + new Vector3(0,0,-0.1f);
            }
            else 
            {
                transform.localPosition = startingPosition;
            }
        }
    }

    private void Unbind()
    {
        boundPoint.GetComponent<SlotScript>().boundTile = null;
        boundPoint = null;
    }

    public GameObject QueryForNode()
    {
        return boundRune.spellComponent;
    }
}
