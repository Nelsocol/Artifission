using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class SpellCaster : MonoBehaviour
{
    private PlayerStatBindings playerBindings;

    public SpellNodeScript[] spellNodes;

    public void Start()
    {
        playerBindings = GetComponent<PlayerStatBindings>();
    }

    public void Update()
    {
        if (spellNodes.Where(e => e.casting).Count() == 0 && playerBindings.inMenus == false)
        {
            if (Mouse.current.leftButton.isPressed && playerBindings.currentMana > spellNodes[0].manaCost)
            {
                playerBindings.DepleteMana(spellNodes[0].manaCost);
                spellNodes[0].CastSpell();
            }
            else if (Mouse.current.rightButton.isPressed && playerBindings.currentMana > spellNodes[1].manaCost)
            {
                playerBindings.DepleteMana(spellNodes[1].manaCost);
                spellNodes[1].CastSpell();
            }
        }
    }
}
