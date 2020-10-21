using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpellForm
{
    void Trigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData);
}
