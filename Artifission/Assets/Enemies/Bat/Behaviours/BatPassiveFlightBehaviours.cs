using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BatPassiveFlightBehaviours : MonoBehaviour
{
    public float seperationForce;
    public float attractionForce;
    public float terrainAvoidanceRange;
    public LayerMask terrainMask;
    public float terrainAvoidanceStrength;

    private StateContext context;
    private Rigidbody2D thisRigidbody;

    void Start()
    {
        context = GetComponent<StateContext>();
        thisRigidbody = context.creatureReference.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ApplySeperationForce();
        ApplyAttractionForce();
        AvoidTerrain();
    }

    private void ApplySeperationForce ()
    {
        foreach (GameObject ally in context.clusterReference.GetAllLivingCreatures().Where(e => e != gameObject))
        {
            thisRigidbody.AddForce((context.creatureReference.transform.position - ally.transform.position).normalized * seperationForce);
        }
    }

    private void ApplyAttractionForce()
    {
        foreach (GameObject ally in context.clusterReference.GetAllLivingCreatures().Where(e => e != gameObject))
        {
            thisRigidbody.AddForce((ally.transform.position - context.creatureReference.transform.position) * attractionForce);
        }
    }

    private void AvoidTerrain ()
    {
        RaycastHit2D terrainCast = Physics2D.Raycast(context.creatureReference.transform.position, Vector2.down, terrainAvoidanceRange, terrainMask);

        if (terrainCast)
        {
            thisRigidbody.AddForce(Vector2.up * terrainAvoidanceStrength);
        }
    }
}
