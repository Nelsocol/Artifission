using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ForgeButtonScript : MonoBehaviour
{
    private PlayerStatBindings playerBindings;

    public SlotScript primaryFormNode;
    public SlotScript primaryEffectNode;

    private void Start()
    {
        playerBindings = GetComponentInParent<SpellForgeController>().playerBindings;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                Debug.Log("Creating Node");
                GameObject formRune = primaryFormNode.QueryForRune();
                GameObject effectRune = primaryEffectNode.QueryForRune();

                if(formRune != null && effectRune != null)
                {
                    Debug.Log("Creating Node");
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
