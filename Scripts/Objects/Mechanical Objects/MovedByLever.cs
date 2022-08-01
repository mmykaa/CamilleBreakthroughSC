using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedByLever : MonoBehaviour
{
    [SerializeField] private Transform whereToGo;
    [SerializeField] private float speed = 10f;

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, whereToGo.position, step);

        if (transform.position == whereToGo.position)
        {
            this.enabled = false;
        }
    }
}
