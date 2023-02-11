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

    public bool startedJumping { private get; set; }
    public bool justLanded { private get; set; }

    public bool IsSquating;

    public float currentVelY;


    private void Start()
    {
        mov = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        animator = GetComponent<Animator>();
        IsSquating = false;
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

        CheckAnimationState();
    }

    private void CheckAnimationState()
    {
        if (IsSquating && Input.GetAxisRaw("Horizontal") != 0 && mov.LastOnGroundTime == 0.2f)
        {
            ChangeAnimationState(Names.SquatRun);
            return;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            IsSquating = true;
            ChangeAnimationState(Names.Squat);
            return;
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

        if (startedJumping)
        {
            ChangeAnimationState(Names.JumpUp);
            startedJumping = false;
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

        if (mov.RB.velocity.y < -10 && startedJumping == false)
        {
            ChangeAnimationState(Names.JumpDown);
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
