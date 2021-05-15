using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StormPaladinTeleportDamageScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Collider2D[] touchingColliders = new Collider2D[10];
        GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), touchingColliders);
        
        PlayerStatBindings playerBindings = null;
        if (touchingColliders.Any(e => e != null && e.TryGetComponent(out playerBindings)))
        {
            UnifiedHitData hitData = GetComponent<UnifiedHitData>();
            hitData.hitSource = transform.position;
            playerBindings.TakeHit(hitData, 1);
        }
    }
}
