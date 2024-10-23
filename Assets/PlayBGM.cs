using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : StateMachineBehaviour
{
    public AudioClip audioClip;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 获取父对象及其所有子对象中的所有 AudioSource 组件
        AudioSource[] audioSources = animator.GetComponentsInParent<AudioSource>();

        // 查找 loop 属性为 true 的 AudioSource
        foreach (AudioSource source in audioSources)
        {
            if (source.loop)
            {
                // 如果背景音乐正在播放就不再执行 Play
                if (!source.isPlaying)
                {
                    // 设置音频片段并播放
                    source.clip = audioClip;
                    source.Play();
                }
                break; // 只需要播放一个符合条件的 AudioSource
            }
        }
    }

    // OnStateExit 可以根据需要停止播放或执行其他操作
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 可以添加逻辑在状态退出时停止背景音乐，具体取决于需求
    }
}
