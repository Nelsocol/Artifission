using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AuraBehaviorScript : MonoBehaviour
{
    public ISpellEffect spellEffect;
    public Transform casterTransform;
    public UnifiedHitData hitData;
    public float auraTime;
    public float onHitTick;
    public float onHitChance;

    private float elapsedTime = 0;
    private float onHitTimeElapsed;

    void Start()
    {
        casterTransform.parent.gameObject.GetComponent<PlayerStatBindings>().ApplyStatus(spellEffect.RetrievePositiveEffect(), 0.75f, 10);
    }
    
    void Update()
    {
        if(onHitTimeElapsed > onHitTick)
        {
            StandardEnemyBindings potentialEnemy;
            foreach(Collider2D collider in Physics2D.OverlapCircleAll(transform.position, 10))
            {
                if(collider.TryGetComponent(out potentialEnemy) && Random.Range(0f,1f) < onHitChance)
                {
                    potentialEnemy.TakeHit(hitData, 1);
                    spellEffect.SpecialOnHitAction(potentialEnemy.gameObject, hitData);
                }  

            }
            onHitTimeElapsed = 0;
        }
        onHitTimeElapsed += Time.deltaTime;

        if(elapsedTime > auraTime)
        {
            foreach(ParticleSystem particleSystem in GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Stop();
            }
            transform.DetachChildren();
            Destroy(gameObject);
        }
        elapsedTime += Time.deltaTime;
    }
}
