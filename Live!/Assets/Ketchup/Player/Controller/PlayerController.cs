using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [TextArea]
    [SerializeField ] private string helper = "This should be on your player GameObject.";

    #region References
    private Rigidbody Rb;
    public Transform orientation;
    public Transform GroundCheck;
    #endregion

    #region Getters & Setters
    public float Speed { get { return _speed; } set { _speed = value; } }

    //Grounded
    public float GroundDrag { get { return _groundDrag; } set { _groundDrag = value; } }
    public bool isGrounded { get { return _isGrounded; } set { _isGrounded = value; } }
    #endregion

    #region Variables
    [Header("Movement")]
    [Tooltip("The speed in which the player moves.")]
    [SerializeField] private float _speed = 8f;
    private float horizontal;
    private float vertical;

    [Header("Grounded")]
    public LayerMask Ground;
    public float _groundDrag = 5f;
    [SerializeField] private bool _isGrounded;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private float _jumpCoolDown = 0.25f;
    [SerializeField] private float _airMultiplier = 0.4f;
    [SerializeField] private bool _isReadyToJump;

    [Header("Keybinds")]
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    #endregion

    #region Functions

    #region Unity Functions
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _isReadyToJump = true;
    }

    private void Update()
    {
        //Ground check
        _isGrounded = Physics.CheckSphere(GroundCheck.position, 0.1f, Ground);

        if (isGrounded) Rb.drag = _groundDrag;
        else Rb.drag = 0;

        PlayerInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    #endregion

    private void Init()
    {
        Rb = GetComponent<Rigidbody>();
        Rb.freezeRotation = true;
    }

    private void PlayerInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(_jumpKey) && _isReadyToJump && _isGrounded)
        {
            _isReadyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), _jumpCoolDown);
        }
    }

    private void MovePlayer()
    {
        //Calculate movement direction
        Vector3 moveDir = orientation.forward * vertical + orientation.right * horizontal;

        //On ground
        if (isGrounded)
        {
            Rb.AddForce(moveDir.normalized * _speed * 10f, ForceMode.Force);
        }
        else if (!isGrounded)
        {
            Rb.AddForce(moveDir.normalized * _speed * 10f * _airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(Rb.velocity.x, 0f, Rb.velocity.z);

        //Limit velocity
        if(flatVel.magnitude > _speed)
        {
            Vector3 limitedVel = flatVel.normalized * _speed;
            Rb.velocity = new Vector3(limitedVel.x, Rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //Reset Y velocity
        Rb.velocity = new Vector3(Rb.velocity.x, 0f, Rb.velocity.z);

        Rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        _isReadyToJump = true;
    }
    #endregion
}
