using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRuneList : MonoBehaviour
{
    public RuneRecord[] runeCollection;

    private void Start()
    {
        runeCollection = GetComponents<RuneRecord>();
    }
}
