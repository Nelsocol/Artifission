using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBarScript : MonoBehaviour
{
    private Transform fullBarTransform;
    private Transform emptyBarTransform;

    private PlayerStatBindings parentBindings;
    public GameObject fullBarAsset;
    public GameObject emptyBarAsset;
    public float barLength;
    public float barHeight;

    private float currentHealth
    {
        get
        {
            return parentBindings.GetCurrentHealth();
        }
    }
    private float maxHealth;

    void Start()
    {
        parentBindings = GetComponentInParent<CameraPlayerTracking>().player.GetComponent<PlayerStatBindings>();
        maxHealth = parentBindings.GetMaxHealth();

        fullBarTransform = fullBarAsset.GetComponent<Transform>();
        fullBarTransform.localScale = new Vector3(barLength, barHeight, 1);

        emptyBarTransform = emptyBarAsset.GetComponent<Transform>();
        emptyBarTransform.localScale = fullBarTransform.localScale;
    }

    void Update()
    {
        fullBarTransform.localScale = new Vector3(barLength * (currentHealth / maxHealth), barHeight, 1f);
        fullBarTransform.localPosition = new Vector3(emptyBarTransform.localPosition.x - (barLength / 2) + (fullBarTransform.localScale.x / 2), 0, -1);
    }
}
