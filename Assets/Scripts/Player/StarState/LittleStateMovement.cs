using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// С��״̬����ҵ��ƶ�������.
/// </summary>
public class LittleStateMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // �ƶ��ٶ�
    [SerializeField] private bool canMove = true; // ��ʾ����Ƿ�����ƶ�
    [SerializeField] private Vector2 moveDirection; // �ƶ�����
    [SerializeField] private PlayerLittleState playerLittleState; // ���� PlayerLittleState ���
    [SerializeField] private Rigidbody2D rb2D; // ������� Rigidbody2D ���

    void Start()
    {
        playerLittleState = GetComponent<PlayerLittleState>(); // ��ȡ PlayerLittleState ���
    }

    void OnEnable()
    {
        canMove = true; // �������ʱ�����ƶ�
    }

    void OnDisable()
    {
        canMove = false; // ʧ�����ʱ��ֹ�ƶ�
        moveDirection = Vector2.zero; // �����ƶ�����
    }

    void Update()
    {
        ProcessInput(); // ��������
    }

    private void ProcessInput()
    {
        if (canMove)
        {
            // ��ȡ WASD ����
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            // ��������һ�������ƶ�
            if (horizontalInput != 0)
            {
                moveDirection = new Vector2(horizontalInput, 0).normalized; // ˮƽ�ƶ�
                canMove = false; // ��Ϊ�����Լ����ƶ�
            }
            else if (verticalInput != 0)
            {
                moveDirection = new Vector2(0, verticalInput).normalized; // ��ֱ�ƶ�
                canMove = false; // ��Ϊ�����Լ����ƶ�
            }

            // ������ƶ�����ִ���ƶ�
            if (moveDirection != Vector2.zero)
            {
                Move();
            }
        }
    }

    private void Move()
    {
        Rigidbody2D rb2D = GetComponentInParent<Rigidbody2D>(); // ��ȡ������� Rigidbody2D ���
        // ʹ�� Rigidbody2D ����ƽ�ƣ������ƶ�������ٶ�
        rb2D.velocity = moveDirection * moveSpeed;
    }
}