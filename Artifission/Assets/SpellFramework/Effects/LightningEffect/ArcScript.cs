using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcScript : MonoBehaviour
{
    public void Arc(Vector2 startPos, Vector2 endPos)
    {
        foreach (ParticleSystem arcParticles in GetComponentsInChildren<ParticleSystem>())
        {
            ParticleSystem.ShapeModule beamShape = arcParticles.shape;
            arcParticles.transform.position = (startPos + endPos) / 2;
            beamShape.radius = (endPos - startPos).magnitude / 2;
            arcParticles.gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg);
            arcParticles.Play();
        }
        transform.DetachChildren();
        Destroy(gameObject);
    }
}
