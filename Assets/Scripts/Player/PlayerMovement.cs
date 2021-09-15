/*****************************************************************************
// File Name :         PlayerMovement.cs
// Author :            Jacob Welch
// Creation Date :     28 August 2021
//
// Brief Description : Handles the movement of the player.
*****************************************************************************/
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Handles gravity and 
    ///                             // TODO smooth player velocity
    /// </summary>
    private Vector3 velocity = Vector3.zero;

    #region Movement
    [Header("Move Speed")]
    [SerializeField]
    [Tooltip("The speed the player moves while standing")]
    [Range(0, 40)]
    private float walkSpeed = 10;

    [SerializeField]
    [Tooltip("The speed the player moves while crouching")]
    [Range(0, 40)]
    private float crouchWalkSpeed = 6;

    /// <summary>
    /// Holds the current movement speed of the player.
    /// </summary>
    private float currentSpeed = 10;

    /// <summary>
    /// Holds the input player movement.
    /// </summary>
    private Vector3 move = Vector3.zero;

    /// <summary>
    /// The Character Controller of the player. (used to move the player)
    /// </summary>
    private CharacterController controller;
    #endregion

    #region Air Movement
    [Header("Jump")]
    [SerializeField]
    [Tooltip("How much velocity the player has when jumping")]
    [Range(0, 5)]
    private float jumpHeight = 3;

    [SerializeField]
    [Tooltip("The rate at which gravity scales")]
    [Range(-20, 0)]
    private float gravity = -9.8f;

    [Space]
    [SerializeField]
    [Tooltip("How far away the player must be from the ground to be grounded")]
    [Range(-30, -5)]
    private float groundCheckDist = 0.5f;

    [SerializeField]
    [Tooltip("Position on the player to check ground from")]
    private GameObject groundCheckPos;

    [SerializeField]
    [Tooltip("The layer mask of the ground")]
    private LayerMask groundMask;

    /// <summary>
    /// Holds true if the player is on the ground.
    /// </summary>
    private bool isGrounded = false;
    #endregion

    #region Cameras
    /// <summary>
    /// The transform of the camera used for movement direction calculations.
    /// </summary>
    private Transform cameraTransform;

    /// <summary>
    /// The virtual camera for when the player is standing.
    /// </summary>
    private CinemachineVirtualCamera walkCam;

    /// <summary>
    /// The CinemachinePOV of the walk camera.
    /// </summary>
    private CinemachinePOV walkCamPOV;

    /// <summary>
    /// The virtual camera for when the player crouching.
    /// </summary>
    private CinemachineVirtualCamera crouchCam;

    /// <summary>
    /// The CinemachinePOV of the crouch camera.
    /// </summary>
    private CinemachinePOV crouchCamPOV;
    #endregion
    #endregion

    #region Functions
    #region Initilization
    /// <summary>
    /// Initializes the player movement components.
    /// </summary>
    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        GetCameras();
    }

    /// <summary>
    /// Gets all player cameras and components.
    /// </summary>
    private void GetCameras()
    {
        cameraTransform = Camera.main.transform;
        walkCam = GameObject.Find("Walk vcam").GetComponent<CinemachineVirtualCamera>();
        walkCamPOV = walkCam.GetCinemachineComponent<CinemachinePOV>();
        crouchCam = GameObject.Find("Crouch vcam").GetComponent<CinemachineVirtualCamera>();
        crouchCamPOV = crouchCam.GetCinemachineComponent<CinemachinePOV>();
    }
    #endregion

    #region Input Calls
    /// <summary>
    /// Recieves player input as a Vector2.
    /// </summary>
    /// <param name="move">The Vector2 movement for the player.</param>
    public void MovePlayer(Vector2 move)
    {
        this.move = new Vector3(move.x, 0, move.y);
    }

    /// <summary>
    /// The player jumps if they are on the ground.
    /// </summary>
    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    /// <summary>
    /// Crouches or uncrouches the player.
    /// </summary>
    public void Crouch()
    {
        if(crouchCam.m_Priority != 2 && isGrounded)
        {
            CrouchHelper(2, ref crouchCamPOV, ref walkCamPOV, crouchWalkSpeed);     // Crouches
        }
        else
        {
            CrouchHelper(0, ref walkCamPOV, ref crouchCamPOV, walkSpeed);           // Uncrouches
        }
    }

    /// <summary>
    /// Sets the current camera and changes the speed.
    /// </summary>
    /// <param name="camPriority">Sets the cam priority of the crouch cam to be higher or lower than the walk cam.</param>
    /// <param name="setTo">The cam that is having its value changed.</param>
    /// <param name="setFrom">The cam that is passing along its values.</param>
    /// <param name="speed">The new speed of the player.</param>
    private void CrouchHelper(int camPriority, ref CinemachinePOV setTo, ref CinemachinePOV setFrom, float speed)
    {
        // Keeps same look angle of the camera
        setTo.m_VerticalAxis.Value = setFrom.m_VerticalAxis.Value;
        setTo.m_HorizontalAxis.Value = setFrom.m_HorizontalAxis.Value;

        // Changes the camera
        crouchCam.m_Priority = camPriority;

        // Sets the player move speed of the player
        currentSpeed = speed;
    }
    #endregion

    #region Calculations
    /// <summary>
    /// Calls for movement calculations and to check if the player is on the ground.
    /// </summary>
    private void Update()
    {
        MoveCalculation();

        GravityCalculation();

        IsGrounded();
    }

    /// <summary>
    /// Checks if the player is on the ground.
    /// </summary>
    private void IsGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheckPos.transform.position, groundCheckDist, groundMask);
    }

    /// <summary>
    /// Moves the player.
    /// </summary>
    private void MoveCalculation()
    {
        Vector3 currentMove = cameraTransform.right * move.x + cameraTransform.forward * move.z;
        currentMove.y = 0f;

        controller.Move(currentMove * currentSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Applyes gravity to the player.
    /// </summary>
    private void GravityCalculation()
    {
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            velocity.y = 0;
        }
    }
    #endregion
    #endregion
}
