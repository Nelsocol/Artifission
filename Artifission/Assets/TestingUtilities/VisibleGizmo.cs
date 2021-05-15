using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.8f);
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
