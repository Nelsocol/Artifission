using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffect
{
    void RemoveEffect(bool targetDead, bool hardRemoval);
    void ResetEffect();
}
