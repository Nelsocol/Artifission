using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdraftScript : MonoBehaviour
{
    public float updraftStrength;
    public float draftTime;
    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rigidBody;
        if(collision.TryGetComponent(out rigidBody))
        {
            rigidBody.AddForce(new Vector2(0, updraftStrength));
        }
    }

    private void Update()
    {
        if(draftTime < 0)
        {
            GetComponent<ParticleSystem>().Stop();
            GetComponent<BoxCollider2D>().enabled = false;
        }
        draftTime -= Time.deltaTime;
    }
}
