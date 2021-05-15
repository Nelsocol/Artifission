using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    public ParticleSystem destructionParticles;
    public UnifiedHitData hitData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStatBindings playerBindings;
        if (collision.tag == "Ground")
        {
            Break();
        }
        else if (collision.TryGetComponent(out playerBindings))
        {
            hitData.hitSource = transform.position;
            playerBindings.TakeHit(hitData, 1);
            Break();
        }
    }

    private void Break()
    {
        destructionParticles.Play();
        destructionParticles.gameObject.transform.parent = null;
        Destroy(gameObject);
    }
}
