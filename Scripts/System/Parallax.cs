using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
    }

    void FixedUpdate()
    {
        float dist = (mainCamera.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
    }
}
