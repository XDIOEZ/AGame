using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ���������Ծ��Ϊ���࣬������Ծ�����ȡ����������Ծ����ʱ�䣨Coyote Time���߼���
/// </summary>
public class PlayerJump_Temp : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] private float jumpForce = 5f; // ��Ծ������
    [SerializeField] private LayerMask groundLayer; // ����㣬���ڼ������Ƿ��ڵ�����
    [SerializeField] private Transform groundCheck; // ���ڼ������Transform
    [SerializeField, Range(0.01f, 1.5f)] private float groundCheckDistance = 0.1f; // ���߼��ľ��룬�ɵ�������ֵ
    [SerializeField] private float jumpCoyoteTime = 0.05f; // ��Ծ����ʱ��

    [SerializeField] private Rigidbody2D rb; // ��Ҹ������
    [SerializeField] private bool isGrounded; // ����Ƿ��ڵ�����
    [SerializeField] private float coyoteTimeCounter; // ���ڼ�ʱ��Ծ����ʱ��
    public bool isJumping;

    /// <summary>
    /// ��ʼ����ҵĸ��������
    /// </summary>
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// ÿ֡������ҵ���Ծ�߼�������������ʹ������롣
    /// </summary>
    void Update()
    {
        CheckGroundStatus(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        playerController.inputActions.PlayerAction.Jump.started += JumpSetting;
    }

    private void JumpSetting(InputAction.CallbackContext context)
    {
        if ( coyoteTimeCounter > 0)
        {
            Jump(jumpForce);
        }
    }

    /// <summary>
    /// �������Ƿ��ڵ����ϣ������� Coyote Time ��ʱ����
    /// </summary>
    /// <param name="position">�����ʼλ��</param>
    /// <param name="direction">��ⷽ��</param>
    /// <param name="distance">������</param>
    /// <param name="layer">����</param>
    private void CheckGroundStatus(Vector3 position, Vector2 direction, float distance, LayerMask layer)
    {
        isGrounded = Physics2D.Raycast(position, direction, distance, layer);

        if (isGrounded)
        {
            coyoteTimeCounter = jumpCoyoteTime;
            isJumping = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

   
   

    /// <summary>
    /// ������Ծ�������޸ĸ���Ĵ�ֱ�ٶ���ʵ����Ծ��
    /// </summary>
    /// <param name="force">��Ծ����</param>
    private void Jump(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, force);
        isJumping = true;
        coyoteTimeCounter = 0;
    }

    /// <summary>
    /// ���Ƽ�����ĵ������������ڿ��ӻ���ⷶΧ��
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
    }
}
