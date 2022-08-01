using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 4f;
    private float defaultPlayerSpeed = 0f;
    [SerializeField] private float jumpForce = 16f;

    private float moveX;
    private float moveY;

    private bool isGrounded = false;
    private bool canJump = false;

    private bool canPlayLandingDust = false;

    [SerializeField] private LayerMask groundLayerMask = 0;

    [SerializeField] private Transform leftFeetTransform = null;
    [SerializeField] private Transform rightFeetTransform = null;
    [SerializeField] private Transform layerChecker = null;

    [SerializeField] private ParticleSystem flipDirectionDust;
    [SerializeField] private ParticleSystem jumpLandingDust;

    private Rigidbody2D myRigidbody2D = null;
    private Animator myAnimator = null;
    private SpriteRenderer mySpriteRenderer = null;
    private Camera mainCamera;
    private Collider2D[] groundDetectionColliders = new Collider2D[1];
    private Collider2D[] colliderFound;

    [SerializeField] private AudioClip[] stepsMetal;
    [SerializeField] private AudioClip[] stepsWood;
    [SerializeField] private AudioSource audioSource;

    public static PlayerController Instance;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = FindObjectOfType<Camera>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();

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
        defaultPlayerSpeed = playerSpeed;
    }

    private void Update()
    {
        isGrounded = CheckForFloor();
        CheckForInputs();
        GetAxis();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void GetAxis()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
    }

    private void CheckForInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            CheckIfCanJump();
        }
    }

    private void CheckIfCanJump()
    {
        if (moveY == 0f && isGrounded)
        {
            canJump = true;
            myAnimator.SetTrigger("Jump");
            Jump();
        }
    }

    private void Jump()
    {
        if (isGrounded && canJump)
        {
            canJump = false;
            isGrounded = false;

            myRigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }


    private void Movement()
    {

        myRigidbody2D.velocity = new Vector2(moveX * playerSpeed, myRigidbody2D.velocity.y);

        myAnimator.SetBool("isRunning", true);


        if (moveX == 0)
        {
            myAnimator.SetBool("isRunning", false);
        }

        if (moveX > 0)
        {
            if (transform.eulerAngles.y != 0)
            {
                transform.eulerAngles = Vector3.up * 0;
                CreateFlipDust();
            }
        }

        if (moveX < 0)
        {
            if (transform.eulerAngles.y != 180)
            {
                transform.eulerAngles = Vector3.up * 180;
                CreateFlipDust();
            }
        }
    }

    public bool CheckForFloor()
    {
        int collidersFoundL = Physics2D.OverlapPointNonAlloc(leftFeetTransform.position, groundDetectionColliders, groundLayerMask);
        int collidersFoundR = Physics2D.OverlapPointNonAlloc(rightFeetTransform.position, groundDetectionColliders, groundLayerMask);

        if (collidersFoundL > 0 || collidersFoundR > 0)
        {
            if (canPlayLandingDust)
            {
                myAnimator.SetBool("isGrounded", true);
                CheckForGroundType();
                CreateLandingDust();
                canPlayLandingDust = false;
            }

            return true;
        }
        canPlayLandingDust = true;
        myAnimator.SetBool("isGrounded", false);
        return false;
    }

    public void CheckForGroundType() //called by animation events
    {
        colliderFound = Physics2D.OverlapCircleAll(layerChecker.position, 0.5f, groundLayerMask.value);

        for (int i = 0; i < colliderFound.Length; i++)
        {
            if (colliderFound[i].transform.gameObject.layer == 8)
            {
                PlayMetalStep();
                break;
            }

            if (colliderFound[i].transform.gameObject.layer == 10)
            {
                PlayWoodStep();
                break;
            }
        }
    }

    private void PlayMetalStep()
    {
        audioSource.clip = stepsMetal[Random.Range(0, stepsMetal.Length)];
        audioSource.Play();
    }

    private void PlayWoodStep()
    {
        audioSource.clip = stepsWood[Random.Range(0, stepsWood.Length)];
        audioSource.Play();
    }

    private void CreateFlipDust()
    {
        flipDirectionDust.Play();
    }

    private void CreateLandingDust()
    {
        jumpLandingDust.Play();
        mainCamera.GetComponent<Animator>().SetTrigger("CameraShake");
    }

    public float GetPlayerVelocity()
    {
        return playerSpeed;
    }

    public void EnableSpeedCheat()
    {
        playerSpeed = playerSpeed * playerSpeed;
        CameraController.Instance.GetNewPlayerSpeed();
    }

    public void DisableSpeedCheat()
    {
        playerSpeed = defaultPlayerSpeed;
        CameraController.Instance.GetNewPlayerSpeed();
    }

    public void SetScript(bool value)
    {
        this.enabled = value;
    }
}