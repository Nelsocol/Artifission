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
    private bool casting = false;
    public bool occupyingCaster;
    public float manaCost;
    public bool continuousCast;
    public float manaPerSecond;

    private float expendedTime = 0;
    private float recoveryTime;
    private bool finishedContinuousCast = true;

    void Start()
    {
        Initialize();
    }

    public void CastSpell()
    {
        if (expendedTime > recoveryTime)
        {
            casting = true;
            occupyingCaster = true;

            spellFormScript.InitializeSpell(spellEffectScript, transform);

            if (continuousCast)
            {
                finishedContinuousCast = false;
            }
        }
    }

    public void EndCast()
    {
        spellFormScript.EndTrigger(spellEffectScript, thisTransform, hitData);
        finishedContinuousCast = true;
        occupyingCaster = false;
    }

    private void Update()
    {
        if(casting)
        {
            if(spellFormScript.WindUp())
            {
                if(!continuousCast)
                {
                    occupyingCaster = false;
                }

                casting = false;
                playerBindings.DepleteMana(manaCost);
                spellFormScript.Trigger(spellEffectScript, transform, hitData);
                expendedTime = 0;
            }
        }
        else
        {
            if(!finishedContinuousCast)
            {
                playerBindings.DepleteMana(manaPerSecond * Time.deltaTime);
            }
            else
            {
                expendedTime += Time.deltaTime;
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
