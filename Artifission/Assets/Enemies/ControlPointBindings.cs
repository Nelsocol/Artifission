using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlPointBindings : MonoBehaviour
{
    List<Transform> controlPoints;

    public void Start()
    {
        controlPoints = GetComponentsInChildren<Transform>().OrderBy(e => e.position.x).ToList();
    }

    public Transform ClosestControlPoint(Vector3 targetPosition)
    {
        Transform closestPoint = controlPoints[0];
        foreach (Transform controlPoint in controlPoints)
        {
            if (Vector2.Distance(targetPosition, closestPoint.position) > Vector2.Distance(targetPosition, controlPoint.position))
            {
                closestPoint = controlPoint;
            }
        }
        return closestPoint;
    }

    public List<Transform> FarControlPoints(Vector3 targetPosition)
    {
        return controlPoints.Where(e => e != ClosestControlPoint(targetPosition)).ToList();
    }

    public List<Transform> ControlPoints()
    {
        return controlPoints;
    }
}
