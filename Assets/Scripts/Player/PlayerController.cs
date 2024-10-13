using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("����")]
    
    public float speed;
    //��ɫ�����ȡ___����
    InputActions inputActions;
    Vector2 inputDirection;
    //��ɫ���___���� 
    Rigidbody2D rb;
    SpriteRenderer sr;
    


    private void Awake()
    {
        inputActions = new InputActions();
    }
    private void Update()
    {
        inputDirection = inputActions.PlayerControl.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        
        rb.velocity = new Vector2(inputDirection.x*speed, inputDirection.y);
    }



    //Input System��������ر�
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
