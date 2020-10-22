using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellCollection : MonoBehaviour
{
    public SpellNodeScript[] spellCollection;

    private void Start()
    {
        spellCollection = GetComponents<SpellNodeScript>();
    }
}
