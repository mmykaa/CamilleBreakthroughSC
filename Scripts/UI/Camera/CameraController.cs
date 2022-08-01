using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float speed;
    [SerializeField] private GameObject followObject;
    [SerializeField] private Vector3 followOffset;
    private Vector2 threshold;

    private Rigidbody2D playerRigidbody2D;

    public static CameraController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameObject.transform.position = new Vector3(followObject.transform.position.x, followObject.transform.position.y, -10);
        GetNewPlayerSpeed();
        threshold = CalculateTreshhold();
        playerRigidbody2D = followObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 follow = followObject.transform.position;

        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x); //x's only
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);//y's only


        Vector3 newPosition = transform.position;

        //conversão para abs
        if (Mathf.Abs(xDifference) >= threshold.x)
        {
            newPosition.x = follow.x;
        }

        if (Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }

        //I Tried this for days and i couldn't figure out why the camera movement was flickering if you readed this
        //please contact me on discord or by e-mail: micaelsilva.work@gmail.com
        //I would love to know why this is happening

        //float moveSpeed = playerRigidbody2D.velocity.magnitude > speed ? playerRigidbody2D.velocity.magnitude : speed;
        // //playerRigidbody2D.velocity.magnitude is a float value equals to the highest velocity value
        // //if lower than the default player speed than we will use our default speed value "speed"

        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

    }

    private Vector3 CalculateTreshhold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector3(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x; //calcular o treshhold
        t.y -= followOffset.y;
        return t;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 border = CalculateTreshhold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }

    public void GetNewPlayerSpeed()
    {
        speed = PlayerController.Instance.GetPlayerVelocity();
    }
}
