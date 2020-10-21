using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ForgeButtonScript : MonoBehaviour
{
    private PlayerStatBindings playerBindings;

    public SlotScript primaryFormNode;
    public SlotScript primaryEffectNode;

    void Update()
    {
        playerBindings = GetComponentInParent<SpellForgeController>().playerBindings;
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                GameObject formRune = primaryFormNode.QueryForRune();
                GameObject effectRune = primaryEffectNode.QueryForRune();

                if(formRune != null && effectRune != null)
                {
                    SpellNodeScript newSpellNode = gameObject.AddComponent<SpellNodeScript>();
                    newSpellNode.formPrefab = formRune;
                    newSpellNode.effectObject = effectRune;
                    playerBindings.SwapSpell(newSpellNode, 0);
                    Destroy(newSpellNode);
                }
            }
        }
    }
}
