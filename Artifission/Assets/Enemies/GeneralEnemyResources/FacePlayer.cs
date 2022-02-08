using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public SpriteRenderer spriteReference;
    StateContext context;

    private void Start()
    {
        context = GetComponent<StateContext>();
    }

    void Update()
    {
        if (context.playerReference.transform.position.x > transform.position.x)
        {
            spriteReference.flipX = true;
        }
        else 
        {
            spriteReference.flipX = false;
        }
    }
}
