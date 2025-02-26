using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.Touch;


[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event Action OnPhysicalInteraction;

    public static event EventHandler<OnIntractEventArgs> OnIntract;

    public class OnIntractEventArgs : EventArgs
    {
        public bool intracting;
    }

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

    

    [SerializeField] float gravity = -1f;
    
    [Tooltip("How far a player can interact with Objects.")]
    [SerializeField] float distanceOfRaycast = 1.6f;
    
    [Tooltip("The layer of gameobjects with which player can interact.")]
    [SerializeField] LayerMask interactLayer;

    public bool canMove;
    public static bool inHome;

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

        if (canMove)
        {
            Movement();
        }
    }

    

    public void Movement()
    {
        float groundCheckRadius = 0.25f;
        bool isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundCheckRadius, groundLayer);  //ground check

        Vector2 movInXandZ = gameInput.GetNormalizedMovementInXandZ();

        Vector3 movement = transform.right * movInXandZ.x + transform.forward * movInXandZ.y;    // transform.right and transform.forward are the directions according to the player direction, not of world

        movement.y = isGrounded ? 0f : gravity;

        characterController.Move(playerSpeed * Time.deltaTime * movement);
    }

    public Vector3 GetGrabPosition()
    {
        return grabPoint.position;
    }
}
