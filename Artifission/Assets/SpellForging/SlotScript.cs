using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotScript : MonoBehaviour
{
    public GameObject boundTile;
    public RuneType tileType;

    public GameObject QueryForRune()
    {
        if(boundTile != null)
        {
            return boundTile.GetComponent<TileScript>().QueryForNode();
        }
        return null;
    }
}
