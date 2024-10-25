using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}

/// <summary>
/// LittleStateMovement�����С��״̬����ҵ��ƶ���Ϊ��
/// <para>ʹ��˵����</para>
/// <para>1. ���˽ű����ص���ҵ�С��״̬��Ϸ�����ϡ�</para>
/// <para>2. ��Unity�༭���У�ȷ����PlayerLittleState��PlayerMove�����������ȷ���á�</para>
/// <para>3. ����moveSpeed�����Ը����ƶ��ٶȡ�</para>
/// <para>4. ͨ�������������ҵĲ���ֵ��isUp, isDown, isLeft, isRight���������ƶ�����</para>
/// <para>5. ȷ������Ҫ�ƶ�������µ���ProcessInput()�������������롣</para>
/// <para>6. ����Ҵ�����̻���Ծʱ������Զ��л��ش���״̬��</para>
/// </summary>
public class LittleStateMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f; // �ƶ��ٶ�

    [SerializeField]
    private bool canMove = true; // ��ʾ����Ƿ�����ƶ�

    [SerializeField]
    private Vector2 moveDirection; // �ƶ�����

    [SerializeField]
    private PlayerLittleState playerLittleState; // ���� PlayerLittleState ���

    [SerializeField]
    private Rigidbody2D rb2D; // ������� Rigidbody2D ���
    public PlayerMove playerMove;

    [Header("�����ƶ�����")]
    public bool isUp = false; // ��
    public bool isDown = false; // ��
    public bool isLeft = false; // ��
    public bool isRight = false; // ��

    void Start()
    {
        playerLittleState = GetComponent<PlayerLittleState>(); // ��ȡ PlayerLittleState ���
        // ��ȡ������� PlayerMove ���
        playerMove = GetComponentInParent<PlayerMove>();

        EventCenter.Instance.AddEventListener<Direction>("�����ƶ�", OnLightBallMove);
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
        if (playerMove.isDashing == true)
        {
            playerMove.dashCount++;
            playerLittleState.ChangeToBig();
        }
        if (playerMove.isJumping == true)
        {
            playerMove.jumpCount++;
            playerLittleState.ChangeToBig();
        }
        ResetDirection(); // ��λ
    }

    private void ResetDirection()
    {
        isUp = false;
        isDown = false;
        isLeft = false;
        isRight = false;
    }

    private float DirectionTransform(bool positive, bool negative)
    {
        if (positive && !negative)
            return -1;
        if (negative && !positive)
            return 1;
        return 0;
    }

    private void ProcessInput()
    {
        if (canMove)
        {
            // ��ȡ WASD ����
            float horizontalInput = DirectionTransform(isLeft, isRight);
            float verticalInput = DirectionTransform(isDown, isUp);

            // ������һ�������ƶ�
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

    private void OnLightBallMove(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                isUp = true;
                break;
            case Direction.Down:
                isDown = true;
                break;
            case Direction.Left:
                isLeft = true;
                break;
            case Direction.Right:
                isRight = true;
                break;
        }
        canMove = true; // �����ƶ�
    }
}
