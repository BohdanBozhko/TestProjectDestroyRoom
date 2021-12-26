using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private int Velocity;
    private int IsMove;
    private int Land;
    private int HardLand;
    private int Jump;
    private bool move;

    private void OnValidate()
    {
        animator = GetComponent<Animator>();
        if (animator == null) print("<color=red>Error:</color> animator was not found");
    }

    private void Start()
    {
        Velocity = Animator.StringToHash("Velocity");
        IsMove = Animator.StringToHash("IsMove");
        Land = Animator.StringToHash("Land");
        HardLand = Animator.StringToHash("HardLand");
        Jump = Animator.StringToHash("Jump");
    }

    public void ChangeMoveAnimation(float normalizedVelocity)
    {
        if (normalizedVelocity <= 0) move = false;
        else { move = true; }
        animator.SetBool(IsMove, move);
        animator.SetFloat(Velocity, normalizedVelocity, 0.1f, Time.deltaTime);
    }

    public void PlayerMovement_PowerfulJump()
    {
        PlayHardLandingAnimation();
    }

    public void PlayerMovement_OnJumpStatusChanged(bool jump)
    {
        ChangeJumpAnimation(jump);
    }

    public void ChangeJumpAnimation(bool jump)
    {
        if (jump)
        {
            animator.SetTrigger(Jump);
        }
        else
        {
            animator.SetTrigger(Land);
        }
    }

    private void PlayHardLandingAnimation()
    {
        animator.SetTrigger(HardLand);
    }

}
