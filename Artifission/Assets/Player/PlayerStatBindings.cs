using System.Linq;
using UnityEngine;

public class PlayerStatBindings : MonoBehaviour, UniversalCreatureBindings
{
    private Transform thisTransform;
    private SpellCaster casterScript;
    private PlayerDeathHandler deathHandler;

    public float maxMana;
    public float constantManaRegeneration;
    public float currentMana;

    public float maxHealth;
    private float currentHealth;
    public bool inMenus;

    void Start()
    {
        thisTransform = GetComponent<Transform>();
        casterScript = GetComponent<SpellCaster>();
        deathHandler = GetComponent<PlayerDeathHandler>();

        currentMana = maxMana;
        currentHealth = maxHealth;
    }

    void Update()
    {
        currentMana = Mathf.Clamp(currentMana + (constantManaRegeneration*Time.deltaTime), 0, maxMana);
    }

    public void DepleteMana(float depletionAmount)
    {
        currentMana -= depletionAmount;
    }

    public bool TakeHit(UnifiedHitData hitData, float hitScalar)
    {
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
        return false;
    }

    public void ReceiveImpact(UnifiedHitData hitData)
    {
        
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
