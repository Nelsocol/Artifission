using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropScript : MonoBehaviour
{
    public bool receivesImpact;

    public void ReceiveImpact(Vector2 impactSource, float impactStrength)
    {
        if(receivesImpact)
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.AddForceAtPosition(((Vector2)transform.position - impactSource).normalized * impactStrength, impactSource, ForceMode2D.Impulse);
        }
    }
}
