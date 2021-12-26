using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerInput playerInput;


    private void OnValidate()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerInput = GetComponent<PlayerInput>();
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
        }
        else
        {
            playerMovement.OnJumpStatusChanged -= playerAnimator.PlayerMovement_OnJumpStatusChanged;
            playerMovement.OnPowerfulJump -= playerAnimator.PlayerMovement_PowerfulJump;
        }
    }

    private void Update()
    {
        playerInput.ReadMoveInput();
        playerMovement.Move(playerInput.GetInputNormalized(), playerInput.CheckJumpInput());
        playerAnimator.ChangeMoveAnimation(playerInput.GetInputMagnitude());
    }



}
