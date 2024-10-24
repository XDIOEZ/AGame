using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : StateMachineBehaviour
{
    public AudioClip[] audioClip;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            AudioSource audioSource = animator.GetComponent<AudioSource>();
        //随机播放集合中的音频
            int index = Random.Range(0, audioClip.Length);
            audioSource.clip = audioClip[index];
            audioSource.Play();
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

   
    
}
