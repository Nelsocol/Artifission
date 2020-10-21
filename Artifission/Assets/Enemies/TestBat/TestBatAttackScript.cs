using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBatAttackScript : MonoBehaviour
{
    private UnifiedHitData hitData;

    public void Start()
    {
        hitData = GetComponent<UnifiedHitData>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerStatBindings playerBindings;
        if(collision.gameObject.TryGetComponent(out playerBindings))
        {
            playerBindings.TakeHit(hitData);
        }
    }
}
