using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour
{
    // 在状态进入时初始化（可以用于播放攻击音效等）
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 可以在这里添加攻击前的准备逻辑，比如播放音效
        //Debug.Log("Boss开始攻击");

        animator.SetTrigger("Move"); // 确保 "Move" 是你在 Animator 中定义的移动状态的触发器名称
       // Debug.Log("攻击完成，返回移动状态");
    }

    // 在状态更新时（每帧）
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 此处可以添加攻击逻辑，比如检测碰撞或造成伤害
    }

    // 在状态退出时切换回移动状态
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 切换回移动状态
       
    }
}
