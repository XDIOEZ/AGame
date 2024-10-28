using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ������벢���ƶ���״̬
        bool isRunning = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isDashing = Input.GetKey(KeyCode.LeftShift);
        bool isJumping = Input.GetKeyDown(KeyCode.Space);
        bool isAttacking = Input.GetKeyDown(KeyCode.J);

        // ����Animator����
        animator.SetBool("Dash", isDashing);
        animator.SetBool("Attack", isAttacking);
        animator.SetBool("Jump", isJumping);
        
         if (isRunning)
        {
            // ���û�а��¿ո�����������ܲ�
            animator.SetBool("Run", true);
        }
        else
        {
            // û����������ʱ������idle����
            animator.SetBool("Run", false);
            animator.SetBool("Jump", false);
        }

        // ����Idle״̬
        bool isIdle = !isRunning && !isDashing && !isJumping && !isAttacking;
        animator.SetBool("Idle", isIdle);
    }
}
