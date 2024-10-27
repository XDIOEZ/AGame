using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

  
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       //�����Ұ���Wasd,����run����
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetTrigger("run");
        }
        else
        {
            animator.SetTrigger("run");
        }
//�����Ұ��¿ո�,����jump����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
        }
        //�����Ұ���J��,����attack����
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("attack");
        }
        //�����Ұ�����Shift,����dash����
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetTrigger("dash");
        }
        //������ʲô��û����,����idle����
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 
            && Input.GetKey(KeyCode.Space) == false && Input.GetKey(KeyCode.J) == false 
            && Input.GetKey(KeyCode.LeftShift) == false)
        {
            animator.SetTrigger("Idle");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
