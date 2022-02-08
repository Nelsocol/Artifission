using System.Linq;
using UnityEngine;

public class PlayerStatBindings : MonoBehaviour, UniversalCreatureBindings
{
    private Transform thisTransform;
    private SpellCaster casterScript;
    private PlayerDeathHandler deathHandler;
    private Rigidbody2D thisRigidbody;

    public CameraShakeBindings cameraShakeBindings;

    public float maxMana;
    public float constantManaRegeneration;
    public float currentMana;

    public float maxHealth;
    private float currentHealth;
    public bool inMenus;
    public float iFrameDuration;

    public bool stunned 
    {
        get
        {
            return stunDuration > 0;
        }
    }

    private float stunDuration;
    private float iFrameTimer;

    void Start()
    {
        thisTransform = GetComponent<Transform>();
        casterScript = GetComponent<SpellCaster>();
        deathHandler = GetComponent<PlayerDeathHandler>();
        thisRigidbody = GetComponent<Rigidbody2D>();

        currentMana = maxMana;
        currentHealth = maxHealth;
    }

    void Update()
    {
        currentMana = Mathf.Clamp(currentMana + (constantManaRegeneration*Time.deltaTime), 0, maxMana);

        stunDuration = stunDuration >= 0 ? stunDuration - Time.deltaTime : 0;
        iFrameTimer = iFrameTimer >= 0 ? iFrameTimer - Time.deltaTime : 0;
    }

    public void DepleteMana(float depletionAmount)
    {
        currentMana -= depletionAmount;
    }

    public bool TakeHit(UnifiedHitData hitData, float hitScalar)
    {
        if (iFrameTimer <= 0)
        {
            cameraShakeBindings.AddTremor(0.1f, 0.1f);
            currentHealth = Mathf.Clamp(currentHealth - hitData.baseDamage, 0, maxHealth);

            if (currentHealth <= 0)
            {
                Kill();
                return true;
            }

            ReceiveImpact(hitData);

            foreach (GameObject statusEffect in hitData.statusEffects)
            {
                float statusRoll = (Random.Range(0, 100) / 100f);
                if (statusRoll < hitData.statusChance)
                {
                    ApplyStatus(statusEffect);
                }
            }

            iFrameTimer = iFrameDuration;
        }
        return false;
    }

    public void ReceiveImpact(UnifiedHitData hitData)
    {
        thisRigidbody.velocity *= 0.1f;
        thisRigidbody.AddForce(((Vector2)transform.position - hitData.hitSource).normalized * hitData.baseImpact, ForceMode2D.Impulse);
        stunDuration = 0.05f * hitData.baseImpact;
    }

    public void Kill()
    {
        deathHandler.Kill();
    }

    public void ApplyStatus(GameObject statusEffect, float powerMultiplier = 1, float duration = 5)
    {
        IStatusEffect newStatus;
        if (!GetComponentsInChildren<IStatusEffect>().Any(e => e.GetType() == statusEffect.GetComponent<IStatusEffect>().GetType()))
        {
            newStatus = Instantiate(statusEffect, thisTransform).GetComponent<IStatusEffect>();
            newStatus.SetParameters(powerMultiplier, duration);
        }
        else
        {
            GetComponentsInChildren<IStatusEffect>().First(e => e.GetType() == statusEffect.GetComponent<IStatusEffect>().GetType()).ResetEffect();
        }
    }

    public float GetParticleEffectScalar()
    {
        return 0.3f;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SwapSpell(SpellNodeScript newSpell, int spellIndex)
    {
        Destroy(casterScript.spellNodes[spellIndex].gameObject.GetComponent<UnifiedHitData>());
        casterScript.spellNodes[spellIndex].formPrefab = newSpell.formPrefab;
        casterScript.spellNodes[spellIndex].effectObject = newSpell.effectObject;
        casterScript.spellNodes[spellIndex].Initialize();
    }
    public void ClearAllStatus(bool hardRemoval)
    {
        foreach (IStatusEffect effect in GetComponentsInChildren<IStatusEffect>())
        {
            effect.RemoveEffect(true, hardRemoval);
        }
    }

    public void ResetStats()
    {
        currentMana = maxMana;
        currentHealth = maxHealth;
    }
}
