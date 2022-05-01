using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool move;
    #region Hash id for animations
    private int Velocity;
    private int IsMove;
    private int Land;
    private int HardLand;
    private int Jump;
    private int Kick;
    #endregion

    private void Start()
    {
        Velocity = Animator.StringToHash("Velocity");
        IsMove = Animator.StringToHash("IsMove");
        Land = Animator.StringToHash("Land");
        HardLand = Animator.StringToHash("HardLand");
        Jump = Animator.StringToHash("Jump");
        Kick = Animator.StringToHash("Kick");
    }

    public void ChangeMoveAnimation(float normalizedVelocity)
    {
        if (normalizedVelocity <= 0) move = false;
        else { move = true; }
        animator.SetBool("IsMove", move);
        animator.SetFloat("Velocity", normalizedVelocity, 0.1f, Time.deltaTime);
    }

    public void PlayerMovement_PowerfulJump()
    {
        animator.SetTrigger("HardLand");
    }

    public void PlayerMovement_OnJumpStatusChanged(bool jump)
    {
        ChangeJumpAnimation(jump);
    }

    public void PlayerCollision_CollisionHappend()
    {
        animator.SetTrigger("Kick");
    }

    public void ChangeJumpAnimation(bool jump)
    {
        if (jump)
        {
            animator.SetTrigger("Jump");
        }
        else
        {
            animator.SetTrigger("Land");
        }
    }


}
