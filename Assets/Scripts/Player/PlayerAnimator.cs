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
    private const string PLAYER_IDLE = "Idle";
    private const string PLAYER_RUN = "Run";
    private const string PLAYER_JUMP_UP = "JumpUp";
    private const string PLAYER_JUMP_DOWN = "JumpDown";
    private const string PLAYER_DAMAGE = "Damage";
    private const string PLAYER_FIRST_ATTACK = "Attack";
    private const string PLAYER_SECOND_ATTACK = "Attack2";
    private const string PLAYER_THIRD_ATTACK = "Attack3";
    #endregion

    public bool startedJumping {  private get; set; }
    public bool justLanded { private get; set; }

    public float currentVelY;


    private void Start()
    {
        mov = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {

        //if (mov.IsSliding)
        //{
        //    Debug.Log("PLAYER_RUN");
        //    
        //}


 

        CheckAnimationState();
    }

    private void CheckAnimationState()
    {

        if (playerCombat.IsAttacking && playerCombat.Attacks == 1)
        {
            ChangeAnimationState(PLAYER_FIRST_ATTACK);
            return;
        }

        else if (playerCombat.IsAttacking && playerCombat.Attacks == 2)
        {
            ChangeAnimationState(PLAYER_SECOND_ATTACK);
            return;
        }

        else if (playerCombat.IsAttacking && playerCombat.Attacks == 3)
        {
            ChangeAnimationState(PLAYER_THIRD_ATTACK);
            return;
        }

        if (startedJumping)
        {            
            ChangeAnimationState(PLAYER_JUMP_UP);
            startedJumping = false;
            return;
        }

        if (justLanded)
        {
            ChangeAnimationState(PLAYER_IDLE);
            justLanded = false;
            return;
        }

        if (Input.GetAxisRaw("Horizontal") != 0 && mov.LastOnGroundTime==0.2f)
        {
            ChangeAnimationState(PLAYER_RUN);
            return;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && mov.LastOnGroundTime == 0.2f)
        {
            ChangeAnimationState(PLAYER_IDLE);
            return;
        }



        if (mov.RB.velocity.y < -10 && startedJumping == false)
        {
            ChangeAnimationState(PLAYER_JUMP_DOWN);
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
