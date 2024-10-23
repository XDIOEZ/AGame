using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : StateMachineBehaviour
{
    public AudioClip audioClip;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ��ȡ�������������Ӷ����е����� AudioSource ���
        AudioSource[] audioSources = animator.GetComponentsInParent<AudioSource>();

        // ���� loop ����Ϊ true �� AudioSource
        foreach (AudioSource source in audioSources)
        {
            if (source.loop)
            {
                // ��������������ڲ��žͲ���ִ�� Play
                if (!source.isPlaying)
                {
                    // ������ƵƬ�β�����
                    source.clip = audioClip;
                    source.Play();
                }
                break; // ֻ��Ҫ����һ������������ AudioSource
            }
        }
    }

    // OnStateExit ���Ը�����Ҫֹͣ���Ż�ִ����������
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ��������߼���״̬�˳�ʱֹͣ�������֣�����ȡ��������
    }
}
