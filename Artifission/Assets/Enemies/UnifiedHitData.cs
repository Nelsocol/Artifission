using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnifiedHitData: MonoBehaviour
{
    public float baseDamage;
    public float baseImpact;
    public Vector2 hitSource;
    public float statusChance;
    public List<GameObject> statusEffects = new List<GameObject>();
}
