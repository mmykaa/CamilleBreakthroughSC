using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractions : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PressurePlate"))
        {
            other.GetComponent<PressurePlate>().MakeSpringDistanceBigger();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PressurePlate"))
        {
            other.GetComponent<PressurePlate>().MakeSpringDistanceSmaller();
        }
    }
}

