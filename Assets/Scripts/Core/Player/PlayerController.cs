using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerInteractor playerInteractor;
    [SerializeField] private PlayerVfx playerVfx;


    private void OnValidate()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerInput = GetComponent<PlayerInput>();
        playerInteractor = GetComponent<PlayerInteractor>();
        playerVfx = GetComponent<PlayerVfx>();
    }

    private void Start()
    {
        Subscribe(true);
    }

    private void OnDestroy()
    {
        Subscribe(false);
    }

    private void Subscribe(bool subscribe)
    {
        if (subscribe)
        {
            playerMovement.OnJumpStatusChanged += playerAnimator.PlayerMovement_OnJumpStatusChanged;
            playerMovement.OnPowerfulJump += playerAnimator.PlayerMovement_PowerfulJump;
            playerMovement.OnJumpStatusChanged += playerVfx.PlayerMovement_OnJumpStatusChanged;
            playerMovement.OnPowerfulJump += playerVfx.PlayerMovement_PowerfulJump;
            playerMovement.OnPowerfulJump += playerInteractor.PlayerMovement_PowerfulJump;
            playerInteractor.OnCollisionHappend += playerAnimator.PlayerCollision_CollisionHappend;
        }
        else
        {
            playerMovement.OnJumpStatusChanged -= playerAnimator.PlayerMovement_OnJumpStatusChanged;
            playerMovement.OnPowerfulJump -= playerAnimator.PlayerMovement_PowerfulJump;
            playerMovement.OnJumpStatusChanged -= playerVfx.PlayerMovement_OnJumpStatusChanged;
            playerMovement.OnPowerfulJump -= playerVfx.PlayerMovement_PowerfulJump;
            playerInteractor.OnCollisionHappend -= playerAnimator.PlayerCollision_CollisionHappend;
        }
    }

    private void Update()
    {
        playerInput.ReadMoveInput();
        playerMovement.Move(playerInput.GetInputNormalized(), playerInput.CheckJumpInput());
        playerAnimator.ChangeMoveAnimation(playerInput.GetInputMagnitude());
    }



}
