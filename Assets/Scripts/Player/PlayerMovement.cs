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

    private void OnValidate()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (animator == null) print("<color=red>Error:</color> animator was not found");
    }

    public void Move(Vector3 input, bool jump = false)
    {
        canMove = true;
        ApplyRotating(input);
        ApplyGravity();
        ApplyJumping(jump);
    }

    //Move character by Root Motion
    private void OnAnimatorMove()
    {
        if (canMove == false) return;
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
        Debug.LogError(characterController.isGrounded);
        if (characterController.isGrounded)
        {
            //Make sure the player's collider stay on the ground
            heightSpeed = -0.5f;

            if (inAir)
            {
                inAir = false;
                OnJumpStatusChanged.Invoke(inAir);
            }

            if (jump)
            {
                inAirTimer = 0.4f;
                inAir = true;
                OnJumpStatusChanged.Invoke(inAir);
                heightSpeed = jumpSpeed;
            }
        }
        else
        {
            inAirTimer -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && inAirTimer <= 0)
            {
                inAir = false;
                heightSpeed = downJumpSpeed;
                OnPowerfulJump.Invoke();
            }
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
