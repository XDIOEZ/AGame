using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // ���� DOTween �������ռ�

public class PlayerDash_ : MonoBehaviour
{
    [Header("�������")]
    [SerializeField]
    private float dashDistance = 5f; // ��̵ľ���
    [SerializeField]
    private float dashDuration = 0.2f; // ��̳�����ʱ��
    [SerializeField]
    private float dashCooldown = 1f; // ��̵���ȴʱ��

    private bool canDash = true; // �Ƿ���Գ��
    private Tweener dashTweener; // �洢��̵� Tweener

    // ���º����������������
    void Update()
    {
        // ����Ƿ���Գ��
        if (canDash)
        {
            Vector2 dashDirection = GetDashDirection();
            if (dashDirection != Vector2.zero)
            {
                Dash(dashDirection); // ִ�г��
            }
        }
    }

    // ��ȡ��̷���
    private Vector2 GetDashDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        return direction;
    }

    // ��̺���
    private void Dash(Vector2 dashDirection)
    {
        // �����̵�Ŀ��λ��
        Vector3 targetPosition = transform.position + (Vector3)dashDirection * dashDistance;

        // ��ʼ����ƶ�
        dashTweener = transform.DOMove(targetPosition, dashDuration)
            .SetEase(Ease.OutQuad) // ʹ�û���Ч����ʹ��̸�����
            .OnComplete(() =>
            {
                // �����ɺ������ȴ״̬
                StartCoroutine(DashCooldown());
            });

        // ��ֹ�������
        canDash = false;
    }

    // �����ȴЭ��
    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true; // ��ȴ��ɺ������ٴγ��
    }

    // ֹͣ��̵ĺ����������Ҫ��
    private void StopDash()
    {
        if (dashTweener != null && dashTweener.IsActive())
        {
            dashTweener.Kill(); // ֹͣ��ǰ�ĳ�̶���
        }
    }
}
