using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturableObject : MonoBehaviour
{
    [SerializeField] private bool canCopyObject = false;
    private bool isClone = false;

    [SerializeField] private int objectID;

    [SerializeField] private GameObject whoIsOther;

    private void Update()
    {
        if (canCopyObject && Input.GetKeyDown(KeyCode.Z) && !isClone)
        {
            //play record animation
            //photo fx
            SendObjectID();
            //cameraTool.ReceiveObject(this.gameObject); //call this with record animation
            Dismiss();
        }

        if (canCopyObject && Input.GetKeyDown(KeyCode.Z) && isClone)
        {
            whoIsOther.GetComponent<CloningCamera>().RemoveCloneID(objectID);
            whoIsOther.GetComponent<CloningCamera>().SetObjectID(objectID);
            //delete fx
            Dismiss();
        }
    }

    public void SetObjectToClone()
    {
        isClone = true;
    }

    private void SendObjectID()
    {
        if (whoIsOther != null)
        {
            whoIsOther.GetComponent<CloningCamera>().CheckIfObjectIsAlreadyCaptured(objectID, gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Camera"))
        {
            canCopyObject = true;
            whoIsOther = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Camera"))
        {
            canCopyObject = false;
            whoIsOther = null;
        }
    }

    private void Dismiss()
    {
        gameObject.SetActive(false);
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
