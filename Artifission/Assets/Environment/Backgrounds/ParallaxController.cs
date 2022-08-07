using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public GameObject cameraReference;
    public float parallaxFactor;
    public bool isolateYAxis;

    private Vector3 startPosition;
    private Vector3 cameraStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        cameraStartPosition = cameraReference.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float camDeltaX = cameraReference.transform.position.x - cameraStartPosition.x;
        float camDeltaY = cameraReference.transform.position.y - cameraStartPosition.y;
        float yPosition = isolateYAxis ? cameraReference.transform.position.y : startPosition.y + camDeltaY * parallaxFactor;
        transform.position = (new Vector3(startPosition.x + camDeltaX * parallaxFactor,yPosition,startPosition.z));
    }
}
