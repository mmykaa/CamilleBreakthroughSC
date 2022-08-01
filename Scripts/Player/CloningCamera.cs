using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloningCamera : MonoBehaviour
{
    //Receber objetos que possam ser clonados - Done
    //Com multiplos objetos para serem clonados, podemos dar cycle entre eles - Done
    //spawn dos objetos - Done
    //Reposição de objetos - Done
    //Verificar a zona onde o objeto vai ser colocado é valida
    //Ao criar uma nova cópia, apagar o antigo objeto clonado do mesmo tipo - Done
    //Apagar o objecto que estamos a tentar copiar no momento - Done

    private Collider2D[] groundDetectionColliders = new Collider2D[1];
    [SerializeField] private LayerMask groundLayerMask = 0;

    [SerializeField] private List<int> capturedObjectsID;
    [SerializeField] private List<int> clonedObjectsID;
    [SerializeField] private List<GameObject> capturedObjectsGO;

    [SerializeField] private GameObject spawnPosition;
    [SerializeField] private GameObject objectToSpawn;
    private GameObject currentCapturedObject;

    [SerializeField] private int currentObjectIDToClone;

    private bool canPlaceObject;

    private void Update()
    {
        canPlaceObject = CheckForFloor();
        if (Input.GetKeyDown(KeyCode.C) && capturedObjectsID.Count > 1)
        {
            Cycle();
        }

        if (Input.GetKeyDown(KeyCode.X) && capturedObjectsID.Count > 0 && objectToSpawn != null && PlayerController.Instance.CheckForFloor())
        {
            CheckForAlreadyClonedObjects();
        }
    }

    private void CheckForAlreadyClonedObjects()
    {
        if (clonedObjectsID.Contains(currentObjectIDToClone) && !canPlaceObject)
        {
            ObjectReposition();
        }
        else if (!canPlaceObject && PlayerController.Instance.CheckForFloor())
        {
            clonedObjectsID.Add(currentObjectIDToClone);
            SpawnObject();
        }
    }

    public void RemoveCloneID(int objectID)
    {
        clonedObjectsID.Remove(objectID);
    }

    public void SetObjectID(int objectID) //era chato fazer a reposição do objeto e não alterar para o mesmo :)
    {
        currentObjectIDToClone = objectID;
        objectToSpawn = capturedObjectsGO[currentObjectIDToClone];
        UpdateUIIndicators();
    }

    private void SpawnObject()
    {
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = spawnPosition.transform.position;
        objectToSpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        objectToSpawn.GetComponent<CapturableObject>().SetObjectToClone();
    }

    private void ObjectReposition()
    {
        objectToSpawn.transform.position = spawnPosition.transform.position;
        objectToSpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    private void Cycle()
    {
        currentObjectIDToClone++;

        if (currentObjectIDToClone > capturedObjectsID.Count - 1)
        {
            currentObjectIDToClone = 0;
        }

        objectToSpawn = capturedObjectsGO[currentObjectIDToClone];

        UpdateUIIndicators();
    }

    private void UpdateUIIndicators()
    {
        if (currentObjectIDToClone == 0)
        {
            UIManager.Instance.ChangeToBoxIndicator();
        }

        if (currentObjectIDToClone == 1)
        {
            UIManager.Instance.ChangeToRectangleIndicator();
        }
    }

    private void ReceiveCapturableObject(int objectID, GameObject objectGO)
    {
        capturedObjectsID.Add(objectID);
        capturedObjectsGO.Add(objectGO);
        currentObjectIDToClone = capturedObjectsID[objectID];
        objectToSpawn = capturedObjectsGO[currentObjectIDToClone];
        UpdateUIIndicators();
    }

    public void CheckIfObjectIsAlreadyCaptured(int objectID, GameObject objectGO)
    {
        if (capturedObjectsID.Contains(objectID))
        {
            return;
        }
        else
        {
            ReceiveCapturableObject(objectID, objectGO);
        }
    }

    public bool CheckForFloor()
    {
        int collidersFound = Physics2D.OverlapPointNonAlloc(spawnPosition.transform.position, groundDetectionColliders, groundLayerMask);

        if (collidersFound > 0)
        {
            return true;
        }

        return false;
    }
}
