using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellForgeController : MonoBehaviour
{
    public PlayerStatBindings playerBindings;
    public GameObject tileObject;
    private bool showing;

    public void Initialize()
    {
        foreach(TileScript tile in GetComponentsInChildren<TileScript>())
        {
            Destroy(tile.gameObject);
        }

        float currentHeight = 4;

        foreach(RuneRecord rune in playerBindings.GetComponentInChildren<PlayerRuneList>().runeCollection.Where(e => e.unlocked))
        {
            GameObject newTile = Instantiate(tileObject, transform);
            newTile.transform.localPosition = new Vector3(-8 + Random.Range(-0.1f, 0.1f),currentHeight,-1);
            newTile.GetComponent<TileScript>().boundRune = rune;
            newTile.GetComponent<SpriteRenderer>().sprite = rune.tileSprite;
            currentHeight -= 0.75f;
        }
    }
    
    public void SetShowState(bool showState)
    {
        showing = showState;
        if (showState)
        {
            Initialize();
            transform.localPosition = new Vector3(0, 0, 3.5f);
            playerBindings.inMenus = true;
        }
        else 
        {
            transform.position = new Vector3(0, 30, 3.5f);
            playerBindings.inMenus = false;
        }
    }

    private void Start()
    {
        playerBindings = GetComponentInParent<CameraPlayerTracking>().player.GetComponent<PlayerStatBindings>();
    }

    private void Update()
    {
        if(showing && Keyboard.current.escapeKey.isPressed)
        {
            SetShowState(false);
        }
    }
}
