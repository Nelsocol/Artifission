using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RuneType
{
    Form,
    Effect
}

public class RuneRecord: MonoBehaviour
{
    public RuneType type;
    public string runeName;
    public bool unlocked;
    public Sprite tileSprite;
    public GameObject spellComponent;
}
