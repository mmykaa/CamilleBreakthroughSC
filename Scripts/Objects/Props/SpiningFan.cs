using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiningFan : MonoBehaviour
{
    [SerializeField] private float fanSpeed;
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, fanSpeed) * Time.deltaTime);
    }

    private void OnBecameVisible()
    {
        this.enabled = true;
    }

    private void OnBecameInvisible()
    {
        this.enabled = false;
    }
}
