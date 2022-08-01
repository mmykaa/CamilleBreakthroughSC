using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private bool canInteract = false;

    private GameObject whoIsOther;

    private void Update()
    {
        if (whoIsOther != null)
        {
            if (whoIsOther.CompareTag("Lever"))
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    whoIsOther.GetComponent<Lever>().ActivateLeverFunction();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        canInteract = true;
        whoIsOther = other.gameObject;

        if (other.CompareTag("PressurePlate"))
        {
            other.GetComponent<PressurePlate>().MakeSpringDistanceBigger();
        }

        if (other.CompareTag("Collectible"))
        {
            GameManager.Instance.CollectibleFound();
            Destroy(other.gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        canInteract = false;
        whoIsOther = null;

        if (other.CompareTag("PressurePlate"))
        {
            other.GetComponent<PressurePlate>().MakeSpringDistanceSmaller();
        }
    }
}
