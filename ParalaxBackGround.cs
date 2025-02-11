using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalexBackGround : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float xPosition;
    private float lengths;
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        lengths = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    void Update()
    {
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        transform.position = new Vector2(xPosition + distanceToMove, transform.position.y);

        if (distanceMoved > xPosition + lengths)
            xPosition += lengths;
        else if (distanceMoved < xPosition - lengths)
            xPosition -= lengths;
    }
}
