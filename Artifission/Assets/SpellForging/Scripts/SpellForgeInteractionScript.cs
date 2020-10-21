using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellForgeInteractionScript : StandardInteractable
{
    public SpellForgeController spellForgeController;

    protected override void Interact(GameObject player)
    {
        player.GetComponent<PlayerDeathHandler>().SetRespawnPosition(transform.position);
        spellForgeController.SetShowState(true);
    }
}
