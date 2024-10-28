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
        // 检测输入并控制动画状态
        bool isRunning = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isDashing = Input.GetKey(KeyCode.LeftShift);
        bool isJumping = Input.GetKeyDown(KeyCode.Space);
        bool isAttacking = Input.GetKeyDown(KeyCode.J);

        // 更新Animator参数
        animator.SetBool("Dash", isDashing);
        animator.SetBool("Attack", isAttacking);
        animator.SetBool("Jump", isJumping);
        
         if (isRunning)
        {
            // 如果没有按下空格键并且正在跑步
            animator.SetBool("Run", true);
        }
        else
        {
            // 没有其他动作时，播放idle动画
            animator.SetBool("Run", false);
            animator.SetBool("Jump", false);
        }

        // 控制Idle状态
        bool isIdle = !isRunning && !isDashing && !isJumping && !isAttacking;
        animator.SetBool("Idle", isIdle);
    }
}
