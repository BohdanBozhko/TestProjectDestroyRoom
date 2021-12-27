using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public event Action<bool> OnJumpStatusChanged = delegate { };
    public event Action OnPowerfulJump = delegate { };

    [Header("Movement Settings")]
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private float jumpSpeed = 10;
    [SerializeField] private float downJumpSpeed = -50;

    private CharacterController characterController;
    private Animator animator;
    private float heightSpeed;
    private float inAirTimer;
    private bool canMove;
    private bool inAir;

    public bool CanMove { get => canMove; set => canMove = value; }

    private void OnValidate()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (animator == null) print("<color=red>Error:</color> animator was not found");
    }

    public void Move(Vector3 input, bool jump = false)
    {
        CanMove = true;
        ApplyRotating(input);
        ApplyGravity();
        ApplyJumping(jump);
    }

    //Move character by Root Motion
    private void OnAnimatorMove()
    {
        if (CanMove == false) return;
        Vector3 velocity = animator.deltaPosition;
        velocity.y = heightSpeed * Time.deltaTime;
        characterController.Move(velocity);
    }

    public void ApplyGravity()
    {
        heightSpeed += Physics.gravity.y * Time.deltaTime;
    }

    private void ApplyJumping(bool jump)
    {
        if (characterController.isGrounded)
        {
            //Make sure the player's collider stay on the ground
            heightSpeed = -0.5f;

            if (inAir)
            {
                inAir = false;
                OnJumpStatusChanged.Invoke(inAir);
            }

            if (jump) Jump();
        }
        else
        {
            inAirTimer -= Time.deltaTime;
            PowerfulJump();
        }
    }

    private void Jump()
    {
        inAirTimer = 0.4f;
        inAir = true;
        heightSpeed = jumpSpeed;
        OnJumpStatusChanged.Invoke(inAir);
    }

    private void PowerfulJump()
    {
        if (Input.GetMouseButtonDown(0) && inAirTimer <= 0)
        {
            inAir = false;
            heightSpeed = downJumpSpeed;
            OnPowerfulJump.Invoke();
            CinemachineScreenShaker.Instance.ScreenShake(1.2f, 0.3f);
        }
    }

    private void ApplyRotating(Vector3 input)
    {
        float rotationSpeedMultiplier = 100f;

        if (input != Vector3.zero && inAir == false)
        {
            Quaternion targetRotation = Quaternion.LookRotation(input, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * rotationSpeedMultiplier * Time.deltaTime);
        }
    }


}
