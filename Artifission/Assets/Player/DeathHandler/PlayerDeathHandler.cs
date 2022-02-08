using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    private PlayerStatBindings playerBindings;

    public DeathScreenBehavior deathScreen;
    public EnemyDirectorScript enemyDirector;

    public Vector3 respawnPosition;

    private void Start()
    {
        playerBindings = GetComponent<PlayerStatBindings>();
    }

    public void Respawn()
    {
        enemyDirector.DespawnAll(true);
        playerBindings.inMenus = false;
        playerBindings.ClearAllStatus(true);
        playerBindings.ResetStats();
        gameObject.transform.position = respawnPosition;
    }

    public void SetRespawnPosition(Vector3 newPosition)
    {
        respawnPosition = newPosition;
    }

    public void Kill()
    {
        playerBindings.inMenus = true;
        deathScreen.fadeIn = true;
    }
}
