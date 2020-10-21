using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpellNodeScript : MonoBehaviour
{
    private Transform thisTransform;
    private ISpellForm spellFormScript;
    private ISpellEffect spellEffectScript;
    private EffectHitData effectHitData;
    private FormHitData formHitData;
    private UnifiedHitData hitData;
    private SpellCostComponents effectCostComponent;
    private SpellCostComponents formCostComponent;

    public GameObject formPrefab;
    public GameObject effectObject;
    public bool casting = false;
    public float manaCost;

    private float expendedTime = 0;
    private bool spellTriggered;
    private float castTime;
    private float recoveryTime;

    void Start()
    {
        Initialize();
    }

    public void CastSpell()
    {
        casting = true;
        spellTriggered = false;
        expendedTime = 0;
    }

    private void Update()
    {
        if(casting)
        {
            if(expendedTime > castTime && !spellTriggered)
            {
                spellFormScript.Trigger(spellEffectScript, thisTransform, hitData);
                spellTriggered = true;
            }

            if(expendedTime > castTime + recoveryTime)
            {
                casting = false;
            }
            expendedTime += Time.deltaTime;
        }
    }

    private UnifiedHitData GenerateHitData()
    {
        UnifiedHitData returnData = gameObject.AddComponent<UnifiedHitData>();

        returnData.baseDamage = formHitData.baseDamage * effectHitData.baseDamageMultiplier;
        returnData.baseImpact = formHitData.baseImpact * effectHitData.baseImpactMultiplier;
        returnData.hitSource = formHitData.hitSource;
        returnData.statusChance = formHitData.baseStatusChance;

        foreach(GameObject statusEffect in effectHitData.statusEffects)
        {
            returnData.statusEffects.Add(statusEffect);
        }

        return returnData;
    }

    public void Initialize()
    {
        thisTransform = GetComponent<Transform>();
        spellFormScript = formPrefab.GetComponent<ISpellForm>();
        spellEffectScript = effectObject.GetComponentInChildren<ISpellEffect>();
        effectHitData = effectObject.GetComponent<EffectHitData>();
        formHitData = formPrefab.GetComponent<FormHitData>();
        effectCostComponent = formPrefab.GetComponent<SpellCostComponents>();
        formCostComponent = effectObject.GetComponent<SpellCostComponents>();

        hitData = GenerateHitData();
        castTime = effectCostComponent.castingTimeComponent + formCostComponent.castingTimeComponent;
        manaCost = effectCostComponent.manaCostComponent + formCostComponent.manaCostComponent;
        recoveryTime = effectCostComponent.recoveryTimeComponent + formCostComponent.recoveryTimeComponent;
    }
}
