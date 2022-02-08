using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearProjectileScript : MonoBehaviour
{
    public float selfDestructTimer;
    public UnifiedHitData hitData;

    private Rigidbody2D thisRigidbody;
    private Collider2D thisCollider;

    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        thisCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!thisRigidbody.isKinematic)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(thisRigidbody.velocity.y, thisRigidbody.velocity.x) * Mathf.Rad2Deg + -90));
        }
        else
        {
            if (selfDestructTimer <= 0)
            {
                Destroy(gameObject);
            }
            selfDestructTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStatBindings playerBindings;
        if (collision.tag == "Ground")
        {
            thisRigidbody.velocity = Vector2.zero;
            thisRigidbody.isKinematic = true;
            thisCollider.enabled = false;
        }
        else if (collision.gameObject.TryGetComponent(out playerBindings))
        {
            hitData.hitSource = transform.position;
            playerBindings.TakeHit(hitData, 1);
        }
    }
}
