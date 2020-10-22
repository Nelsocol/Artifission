using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathScreenBehavior : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private PlayerDeathHandler playerReference;
    public bool fadeIn;
    public float fadeTime;

    void Start()
    {
        playerReference = GetComponentInParent<CameraPlayerTracking>().player.GetComponent<PlayerDeathHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1,1,1,0);
        transform.localPosition = new Vector3(0,0,1);
    }

    void Update()
    {
        if(fadeIn)
        {
            if(spriteRenderer.color.a < 1)
            {
                spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a + (1/fadeTime) * Time.deltaTime);
            }
            else
            {
                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    playerReference.Respawn();
                    fadeIn = false;
                }
            }
        }
        else
        {
            if (spriteRenderer.color.a > 0)
            {
                spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a - (1 / fadeTime) * Time.deltaTime);
            }
        }
    }
}
