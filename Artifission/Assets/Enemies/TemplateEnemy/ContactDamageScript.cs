using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamageScript : MonoBehaviour
{
    private UnifiedHitData hitData;
    private CreatureBrainCore creatureBrain;

    public void Start()
    {
        hitData = GetComponent<UnifiedHitData>();
        creatureBrain = GetComponentInChildren<CreatureBrainCore>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerStatBindings playerBindings;
        if(collision.gameObject.TryGetComponent(out playerBindings))
        {
            hitData.hitSource = transform.position;
            playerBindings.TakeHit(hitData, 1);
            creatureBrain.SendMessage(StateMessages.HitPlayer);
        }
    }
}
