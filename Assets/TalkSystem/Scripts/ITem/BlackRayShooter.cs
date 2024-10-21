using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackRayShooter : MonoBehaviour
{
    public float maxLength = 10.0f; // ��󳤶�
    public float lengthIncreaseSpeed = 1.0f; // ���������ٶ�
    public float damage = 10.0f; // ��ײ�������ɵ��˺�
    public LayerMask playerLayer; // ��ҵ���ײ��


    public ParticleSystem effct;
    private LineRenderer lineRenderer;
    private float currentLength = 0.0f;

    void Start()
    {
        // ��ȡLineRenderer���
        lineRenderer = GetComponent<LineRenderer>();

        // �����ߵ���ʼ�ͽ���λ��
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.startWidth = 0.2f;
    }

    void Update()
    {
        // �����ߵĳ���
        currentLength += lengthIncreaseSpeed * Time.deltaTime;
        Vector3 endPosition = transform.position + transform.right * currentLength;
        lineRenderer.SetPosition(1, endPosition);


        effct.transform.position = endPosition;
        // ������ȳ�����󳤶ȣ�ֹͣ��
        if (currentLength > maxLength)
        {
            Destroy(gameObject);
        }

        Ray ray = new Ray(transform.position, transform.right); // �������λ�����ҷ�������
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, currentLength, playerLayer); ;

        // �����ײ
        if (hit.collider != null)
        {
            // ���������������ײ����ӡ��ײ��Ϣ
            Debug.Log("Hit " + hit.collider.name);

            // �����ײ����ң�����˺�
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                // hit.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
                Destroy(hit.collider.gameObject);
                Destroy(gameObject);
            }
        }
    }
}