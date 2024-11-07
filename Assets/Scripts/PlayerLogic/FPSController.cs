using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private ControlManager controlManager;
    [SerializeField] private float walkingSpeed = 7.5f;
    [SerializeField] private float runningSpeed = 11.5f;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float lookXLimit = 45.0f;
    [SerializeField] private float jumpDelay = 0.2f;
    private SoundFXManager soundManager;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        controlManager.AddPlayersAction(PlayersActionType.Jump, HandleJump);
        controlManager.AddPlayersAction(PlayersActionType.Jump, HandleJumpSound);

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundFXManager>();
    }

    private void HandleGravity()
    {
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }

    private void DelayedJump()
    {
        if (canMove && characterController.isGrounded)
            moveDirection.y = jumpSpeed;
    }
    private void HandleJump(InputAction.CallbackContext context)
    {
        if (canMove && characterController.isGrounded)
            Invoke("DelayedJump", jumpDelay);
    }

    private void HandleMovement()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Get player's input: 
        Vector2 inputVector = controlManager.GetInputDirectionNormalized();
        bool isRunning = controlManager.PlayerIsRunning();

        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * inputVector.y : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * inputVector.x : 0;

        Vector3 newMoveDirection = (forward * curSpeedX) + (right * curSpeedY);
        newMoveDirection.y = moveDirection.y;
        moveDirection = newMoveDirection;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleRotation()
    {
        // Get input data
        // Vector2 mouseMovement = controlManager.GetMouseMovement();
        // TODO: fix unity's new input system
        Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -mouseMovement.y * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, mouseMovement.x * lookSpeed, 0);
        }
    }

    private void HandleSound()
    {
        if (controlManager.GetInputDirectionNormalized().magnitude > 0)
        {
            soundManager.PlaySound(Sound.RobotMovement, true, transform);
        }
        else
        {
            soundManager.StopSound(Sound.RobotMovement, transform);
        }
    }

    private void HandleJumpSound(InputAction.CallbackContext context)
    {
        if (characterController.isGrounded)
        {
            soundManager.PlaySound(Sound.RobotJump, false, transform);
        }
    }

    void Update()
    {
        if (PauseMenu.isPaused == false)
        {
            HandleMovement();
            HandleGravity();
            HandleRotation();
            HandleSound();
        }
    }
}