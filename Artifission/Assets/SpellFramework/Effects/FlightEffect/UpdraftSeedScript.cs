using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdraftSeedScript : MonoBehaviour
{
    public GameObject updraftObject;
    private bool jobDone = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" && !jobDone)
        {
            Instantiate(updraftObject, transform.position, Quaternion.identity);
            GetComponent<ParticleSystem>().Stop();
            GetComponent<SpriteRenderer>().enabled = false;
            jobDone = true;
        }
    }
}
