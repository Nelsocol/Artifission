using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellForgeInteractionScript : StandardInteractable
{
    public SpellForgeController spellForgeController;
    public PlayerDeathHandler playerDeathHandler;

    protected override void Interact()
    {
        playerDeathHandler.SetRespawnPosition(transform.position);
        spellForgeController.SetShowState(true);
    }
}
