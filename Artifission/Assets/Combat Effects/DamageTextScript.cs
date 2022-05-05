using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextScript : MonoBehaviour
{
    private Rigidbody2D thisRigidbody;
    private TextMeshPro thisText;
    private float timeElapsed;

    public Vector2 impulseAmount;
    public float lifeTime;

    void Start()
    {
        thisText = GetComponent<TextMeshPro>();
        thisRigidbody = GetComponent<Rigidbody2D>();
        thisRigidbody.AddForce(impulseAmount * new Vector2(Random.Range(0.7f, 1), Random.Range(0.7f, 1)), ForceMode2D.Impulse);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        thisText.color = new Color(thisText.color.r, thisText.color.g, thisText.color.b, 1 - timeElapsed / lifeTime);
        if (timeElapsed > lifeTime) Destroy(gameObject);
    }
}
