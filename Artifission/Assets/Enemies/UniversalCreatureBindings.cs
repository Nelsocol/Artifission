using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UniversalCreatureBindings
{
    bool TakeHit(UnifiedHitData hitData, float hitScalar);
    void ReceiveImpact(UnifiedHitData hitData);
    void Kill();
    void ApplyStatus(GameObject statusEffect, float powerMultiplier = 1, float duration = 5);
    float GetParticleEffectScalar();
    float GetMaxHealth();
    float GetCurrentHealth();
    void ClearAllStatus(bool hardRemoval);
    void ResetStats();
}
