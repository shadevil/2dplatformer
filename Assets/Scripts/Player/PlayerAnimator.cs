using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    private PlayerMovement mov;
    private PlayerCombat playerCombat;

    #region ANIMATIONS

    #endregion

    public bool StartedJumping { private get; set; }
    public bool justLanded { private get; set; }

    public bool IsSquating;

    private bool isJumping;




    private void Start()
    {
        mov = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        animator = GetComponent<Animator>();
        IsSquating = false;
        isJumping = false;
    }

    private void LateUpdate()
    {

        //if (mov.IsSliding)
        //{
        //    Debug.Log("PLAYER_RUN");
        //    
        //}


        if (!Input.GetKey(KeyCode.LeftControl))
        {
            IsSquating = false;
        }

        if (mov.LastOnGroundTime == 0.2f)
            isJumping = false;

        CheckAnimationState();
    }

    private void CheckAnimationState()
    {
        if (mov.IsSquating || IsSquating && Input.GetAxisRaw("Horizontal") != 0 && mov.LastOnGroundTime == 0.2f)
        {
            ChangeAnimationState(Names.SquatRun);
            return;
        }

        if (isJumping == false)
        {
            if (Input.GetKey(KeyCode.LeftControl) || mov.IsSquating)
            {
                IsSquating = true;
                ChangeAnimationState(Names.Squat);
                return;
            }
        }


        if (playerCombat.IsAttacking && playerCombat.Attacks == 1)
        {
            ChangeAnimationState(Names.Attack);
            return;
        }

        else if (playerCombat.IsAttacking && playerCombat.Attacks == 2)
        {
            ChangeAnimationState(Names.Attack2);
            return;
        }

        else if (playerCombat.IsAttacking && playerCombat.Attacks == 3)
        {
            ChangeAnimationState(Names.Attack3);
            return;
        }

        if (Input.GetKey(KeyCode.C) && Input.GetAxisRaw("Horizontal") != 0 && mov.LastOnGroundTime == 0.2f)
            return;

        if (StartedJumping)
        {
            ChangeAnimationState(Names.JumpUp);
            StartedJumping = false;
            isJumping = true;
            return;
        }

        if (justLanded)
        {
            ChangeAnimationState(Names.Idle);
            justLanded = false;
            return;
        }

        if (Input.GetAxisRaw("Horizontal") != 0 && mov.LastOnGroundTime == 0.2f)
        {
            ChangeAnimationState(Names.Run);
            return;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && mov.LastOnGroundTime == 0.2f)
        {
            ChangeAnimationState(Names.Idle);
            return;
        }

        if (mov.RB.velocity.y < -10 && StartedJumping == false)
        {
            ChangeAnimationState(Names.JumpDown);
            isJumping = true; // в бущем заменить на isFalling
            return;
        }
    }


    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
