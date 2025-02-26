using System;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [Header("Essentials")]

    [Tooltip("The GameInput script that should be attached with some gameobject as this script should be reading input for look and movment from it.")]
    [SerializeField] GameInput gameInput;

    [SerializeField] CharacterController characterController;

    [Tooltip("This game object should have a collider with apprx 0.3 unit radius")]
    [SerializeField] GameObject groundCheck;

    [Tooltip("The gameobject which controlls the cinemachine position and raycasitg for interaction.")]
    [SerializeField] GameObject cameraRootGameobject;

    [Tooltip("The position where object will move for grab or interaction animation")]
    [SerializeField] Transform grabPoint;

    [Tooltip("The layer on which player can jump.")]
    [SerializeField] LayerMask groundLayer;

    [Space(20)]
    [Header("Prefrences")]
    [SerializeField] float playerSpeed = 5f;

    [SerializeField] float jumpForce = 8f;

    [SerializeField] float gravity = -9.18f;
    
    [Tooltip("How far a player can interact with Objects.")]
    [SerializeField] float distanceOfRaycast = 1.6f;

    private Vector3 velocity;
    private bool isGrounded;

    public bool canMove;
    private void Start()
    {
        canMove = true;
    }

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("there are more than one instance of player");
        Instance = this;
       
    }

    private void Update()
    {
        GroundCheck();
        if (canMove)
        {
            Movement();
            HandleJump();
        }
    }

    private void GroundCheck()
    {
        float groundCheckRadius = 0.25f;
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundCheckRadius, groundLayer);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep grounded check stable
        }
    }

    public void Movement()
    {
        Vector2 movInXandZ = gameInput.GetNormalizedMovementInXandZ();
        Vector3 movement = transform.right * movInXandZ.x + transform.forward * movInXandZ.y;
        movement *= playerSpeed;
        movement.y = velocity.y;
        characterController.Move(movement * Time.deltaTime);
    }


    private void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Code working");
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public Vector3 GetGrabPosition()
    {
        return grabPoint.position;
    }
}
