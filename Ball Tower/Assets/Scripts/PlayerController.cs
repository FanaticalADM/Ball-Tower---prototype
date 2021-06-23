using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public event Action onJumpEvent;
    void JumpEvent()
    {
        Jump();
        if (onJumpEvent != null)
        {
            onJumpEvent();
        }
    }

    public event Action onDoubleJumpRestoration;
    void DoubleJumpRestoration()
    {
        if (!doubleJump)
        {
            if (onDoubleJumpRestoration != null)
            {
                onDoubleJumpRestoration();
            }

            doubleJump = true;
            meshRenderer.material = doubleJumpBoost;
        }
    }

    public event Action onFloatEvent;
    public void FloatEvent()
    {
        if (onFloatEvent != null)
        {
            onFloatEvent();
            
        }
        jumpSpeed = jumpSpeed * jumpSpeedBoostValue;
        StartCoroutine(FloatCorutine());
    }

    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private JoysticButton joystickButton;

    private float direction;
    private Rigidbody playerRb;
    private float nextPowerUpTreshold = 100; //?
    [SerializeField]
    private float speed; 
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float jumpSpeedBoostValue; // boost prefab
    [SerializeField]
    private float startingJumpSpeed; 
    [SerializeField]
    private float fallMultiplayerOne =3.0f;
    [SerializeField]
    private float fallMultiplayerTwo =2.5f;

    [SerializeField]
    private Material normal;
    [SerializeField]
    private Material doubleJumpBoost;
    private MeshRenderer meshRenderer;

    [SerializeField]
    private bool grounded;
    [SerializeField]
    private bool doubleJump;
    private bool canJump;

    [SerializeField]
    private Transform startPosition;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onStartGame += ResetPlayer;

        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        playerRb = gameObject.GetComponent<Rigidbody>();
        grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        if (ScoreManager.Instance.Score > nextPowerUpTreshold)
        {
            DoubleJumpRestoration();
            nextPowerUpTreshold += 100;
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (canJump)
            JumpEvent();

        BetterJump();
    }

    void GetInput()
    {
        direction = Input.GetAxis("Horizontal");

        if ((Input.GetButtonDown("Jump") || joystickButton.isPressed) && (grounded || doubleJump))
        {
            joystickButton.isPressed = false;
            if (!grounded)
            {
                doubleJump = false;
                meshRenderer.material = normal;
            }
            canJump = true;
        }
    }

    void Move()
    {
        playerRb.velocity = new Vector3((direction + joystick.Horizontal) * speed, playerRb.velocity.y);
    }

    void Jump()
    {
        canJump = false;
        playerRb.velocity = new Vector3 (playerRb.velocity.x,jumpSpeed);
        grounded = false;
    }

    void BetterJump()
    {
        if (playerRb.velocity.y < 0)
        {
            playerRb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplayerOne - 1) * Time.deltaTime;
        }
        else if (playerRb.velocity.y > 0 && (!Input.GetKey(KeyCode.Space) && !joystickButton.pressed))
        {
            playerRb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplayerTwo - 1) * Time.deltaTime;
        }
    }

    IEnumerator FloatCorutine()
    {
        yield return new WaitForSeconds(5);
        jumpSpeed = jumpSpeed / jumpSpeedBoostValue;
    }

    public void ResetPlayer()
    {
        transform.position = startPosition.position;
        doubleJump = false;
        meshRenderer.material = normal;
        StopCoroutine(FloatCorutine());
        playerRb.useGravity = true;
        jumpSpeed = startingJumpSpeed;
        nextPowerUpTreshold = 100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
