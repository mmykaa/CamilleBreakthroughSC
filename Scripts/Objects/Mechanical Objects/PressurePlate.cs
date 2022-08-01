using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject platformToMove;
    [SerializeField] private Sprite disabledPressurePlate;
    [SerializeField] private Sprite activatedPressurePlate;
    private SpriteRenderer mySpriteRenderer;
    [SerializeField] private int platformToMoveSpringDistanceWithoutPressure;
    [SerializeField] private int platformToMoveSpringDistanceOnPressure;

    SpringJoint2D platformToMoveSpringJoint;

    private void Awake()
    {
        platformToMoveSpringJoint = platformToMove.GetComponent<SpringJoint2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void MakeSpringDistanceSmaller()
    {
        mySpriteRenderer.sprite = disabledPressurePlate;
        platformToMoveSpringJoint.distance = platformToMoveSpringDistanceWithoutPressure;
    }

    public void MakeSpringDistanceBigger()
    {
        mySpriteRenderer.sprite = activatedPressurePlate;
        platformToMoveSpringJoint.distance = platformToMoveSpringDistanceOnPressure;
    }
}
