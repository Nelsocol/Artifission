using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.InputSystem.Controls;

public class SpellCaster : MonoBehaviour
{
    private PlayerStatBindings playerBindings;

    public SpellNodeScript[] spellNodes;

    private List<ButtonControl> inputList;
    public void Start()
    {
        inputList = new List<ButtonControl>()
        {
            Mouse.current.leftButton,
            Mouse.current.rightButton,
            Keyboard.current.digit1Key,
            Keyboard.current.digit2Key,
            Keyboard.current.digit3Key,
            Keyboard.current.digit4Key,
            Keyboard.current.digit5Key
        };

        playerBindings = GetComponent<PlayerStatBindings>();
    }

    public void Update()
    {
        if (spellNodes.Where(e => e.casting).Count() == 0 && playerBindings.inMenus == false)
        {
            foreach (ButtonControl input in inputList)
            {
                int index = inputList.IndexOf(input);
                if (input.IsPressed() && playerBindings.currentMana > spellNodes[index].manaCost)
                {
                    playerBindings.DepleteMana(spellNodes[index].manaCost);
                    spellNodes[index].CastSpell();
                }
            }
        }

        if(playerBindings.inMenus == false)
        {
            foreach (ButtonControl input in inputList)
            {
                int index = inputList.IndexOf(input);
                if (input.wasReleasedThisFrame && spellNodes[index].continuousCast)
                {
                    spellNodes[index].EndCast();
                }
            }


        }
    }
}
