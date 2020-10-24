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
    private PlayerStatBindings playerBindings;

    public GameObject formPrefab;
    public GameObject effectObject;
    public bool casting = false;
    public float manaCost;
    public bool continuousCast;
    public float manaPerSecond;

    private float expendedTime = 0;
    private bool spellTriggered;
    private float recoveryTime;
    private bool finishedContinuousCast = true;

    void Start()
    {
        Initialize();
    }

    public void CastSpell()
    {
        casting = true;
        spellTriggered = false;
        expendedTime = 0;

        if(continuousCast)
        {
            finishedContinuousCast = false;
        }
    }

    public void EndCast()
    {
        spellFormScript.EndTrigger(spellEffectScript, thisTransform, hitData);
        finishedContinuousCast = true;
    }

    private void Update()
    {
        if(casting)
        {
            if(!spellTriggered)
            {
                spellFormScript.Trigger(spellEffectScript, thisTransform, hitData);
                spellTriggered = true;
            }

            if(expendedTime > recoveryTime)
            {
                casting = false;
            }

            if (finishedContinuousCast)
            {
                expendedTime += Time.deltaTime;
            }
        }

        if(!finishedContinuousCast)
        {
            playerBindings.DepleteMana((manaPerSecond + playerBindings.constantManaRegeneration) * Time.deltaTime);
            if(playerBindings.currentMana <= 0)
            {
                EndCast();
            }
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
        playerBindings = GetComponentInParent<PlayerStatBindings>();

        hitData = GenerateHitData();
        manaCost = effectCostComponent.manaCostComponent + formCostComponent.manaCostComponent;
        recoveryTime = effectCostComponent.recoveryTimeComponent + formCostComponent.recoveryTimeComponent;
        continuousCast = spellFormScript.IsContinuous();
        manaPerSecond = effectCostComponent.manaPerSecond + formCostComponent.manaPerSecond;
    }
}
