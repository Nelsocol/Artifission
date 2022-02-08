using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpellForm
{
    void Trigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData);
    void EndTrigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData);
    bool IsContinuous();
    void InitializeSpell(ISpellEffect spellEffect, Transform casterTransform);
    bool WindUp();
}
