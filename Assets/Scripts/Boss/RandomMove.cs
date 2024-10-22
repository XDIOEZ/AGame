using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : StateMachineBehaviour
{
    [Header("����ƶ�����")]
    [SerializeField]
    private Vector2 moveDurationRange = new Vector2(1f, 3f); // ����ƶ�ʱ�䷶Χ
    [SerializeField]
    private Vector2 moveSpeedRange = new Vector2(0.5f, 1.0f); // �ƶ��ٶȷ�Χ
    [SerializeField]
    private int turnCounter = 3; // �����ת�����
    [SerializeField]
    private float minTurnDuration = 0.5f; // ת�����ʱ�����Сֵ
    [SerializeField]
    private float maxTurnDuration = 1.5f; // ת�����ʱ������ֵ

    private float moveTimer = 0f; // ����ƶ���ʱ��
    private Vector2 moveDirection; // ����ƶ�����
    private AIData aiData;
    private int currentTurnCounter; // ��ǰת�������
    private float turnDuration; // ÿ��ת��ĳ���ʱ��

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ��ȡAI����
        aiData = animator.gameObject.transform.parent?.GetComponent<AIData>();

        // ���AI�Ƿ���Ŀ�꣬�����Ŀ��������Update��ִ��
        if (aiData != null && aiData.enemyTargetPosition != null)
        {
            //Debug.Log("AI��Ŀ�꣬��������ƶ���");
            moveTimer = 0f;
            return;
        }

        // ��ʼ���ƶ���ʱ��
        moveTimer = Random.Range(moveDurationRange.x, moveDurationRange.y); // �����ʼ����ʱ��

        // ����ƶ�����
        ChangeMoveDirection();

        // ��ʼ��ת�������
        currentTurnCounter = turnCounter;
    }

    private void ChangeMoveDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
        turnDuration = Random.Range(minTurnDuration, maxTurnDuration); // ����ת�����ʱ��
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ����Ƿ���Ŀ�꣬�����Ŀ������������ƶ��߼�
        if (aiData == null || aiData.enemyTargetPosition != null)
        {
            return;
        }

        // ����ƶ�
        float randomSpeed = aiData.entity.Data.MoveSpeed * Random.Range(moveSpeedRange.x, moveSpeedRange.y);
        aiData.currentPosition.Translate(moveDirection * randomSpeed * Time.deltaTime);

        // ���¼�ʱ��
        moveTimer -= Time.deltaTime;
        turnDuration -= Time.deltaTime;

        // ���ת�����ʱ�䵽��0����ת�����������0����ı��ƶ�����
        if (turnDuration <= 0f && currentTurnCounter > 0)
        {
            ChangeMoveDirection();
            currentTurnCounter--; // �ݼ�ת�������
        }

        // ��� moveTimer <= 0������ת����������㣬�����Idle״̬
        if (moveTimer <= 0f && currentTurnCounter <= 0)
        {
            animator.SetTrigger("Idle");
            Debug.Log("����ƶ�ʱ�����������Idle״̬��!!!!!!!!!!!!!!!");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �����ʱ��
        moveTimer = 0f;
       // Debug.Log("�˳�Move״̬����������ƶ���ʱ����");
    }
}
