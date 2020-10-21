using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManaBarScript : MonoBehaviour
{
    private Transform fullBarTransform;
    private Transform emptyBarTransform;

    public PlayerStatBindings parentBindings;
    public GameObject fullBarAsset;
    public GameObject emptyBarAsset;
    public float barLength;
    public float barHeight;

    private float currentMana
    {
        get
        {
            return parentBindings.currentMana;
        }
    }
    private float maxMana;

    void Start()
    {
        maxMana = parentBindings.maxMana;

        fullBarTransform = fullBarAsset.GetComponent<Transform>();
        fullBarTransform.localScale = new Vector3(barLength, barHeight, 1);

        emptyBarTransform = emptyBarAsset.GetComponent<Transform>();
        emptyBarTransform.localScale = fullBarTransform.localScale;
    }

    void Update()
    {
        fullBarTransform.localScale = new Vector3(barLength * (currentMana / maxMana), barHeight, 1f);
        fullBarTransform.localPosition = new Vector3(emptyBarTransform.localPosition.x - (barLength / 2) + (fullBarTransform.localScale.x / 2), 0, -1);
    }
}
