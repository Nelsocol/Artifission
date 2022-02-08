using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHealthBarScript : MonoBehaviour
{
    private Transform fullBarTransform;
    private UniversalCreatureBindings parentBindings;
    private SpriteRenderer healthBarSprite;
    private SpriteRenderer emptyBarSprite;
    private Transform emptyBarTransform;

    public GameObject fullBarAsset;
    public GameObject emptyBarAsset;
    public float barLength;
    public float barHeight;

    private float currentHealth { 
        get 
        {
            return parentBindings.GetCurrentHealth();
        } 
    }
    private float maxHealth;

    void Start()
    {
        parentBindings = GetComponentInParent<UniversalCreatureBindings>();
        maxHealth = parentBindings.GetMaxHealth();

        fullBarTransform = fullBarAsset.GetComponent<Transform>();
        fullBarTransform.localScale = new Vector3(barLength, barHeight, 1);

        emptyBarTransform = emptyBarAsset.GetComponent<Transform>();
        emptyBarTransform.localScale = fullBarTransform.localScale;

        healthBarSprite = fullBarAsset.GetComponent<SpriteRenderer>();
        emptyBarSprite = emptyBarAsset.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (maxHealth == currentHealth)
        {
            healthBarSprite.enabled = false;
            emptyBarSprite.enabled = false;
        }
        else 
        {
            healthBarSprite.enabled = true;
            emptyBarSprite.enabled = true;

            fullBarTransform.localScale = new Vector3(barLength * (currentHealth / maxHealth), barHeight, 1f);
            fullBarTransform.localPosition = new Vector3(emptyBarTransform.localPosition.x - (barLength / 2) + (fullBarTransform.localScale.x / 2), 0, 0);
        }
    }
}
