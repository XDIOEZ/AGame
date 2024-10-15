using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("����")]
    
    public float speed;

    //��ɫ�����ȡ___����

    AGameInputSystem inputActions;
    Vector2 inputDirection;
    //��ɫ���___���� 

    Rigidbody2D rb;
    SpriteRenderer sr;
    


    private void Awake()
    {
        inputActions = new AGameInputSystem();
    }
    private void Update()
    {
        inputDirection = inputActions.PlayerAction.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        //ͨ��������ƽ�ɫ�ƶ�
        rb.velocity = new Vector2(inputDirection.x * speed, inputDirection.y);

        //���ƽ�ɫת��
        if (inputDirection.x != 0 && Mathf.Sign(inputDirection.x) != Mathf.Sign(transform.localScale.x))
        { 
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

    }
   

        //Input System��������ر�
    private  void OnEnable()
    {
         inputActions.Enable();
    }
    private  void OnDisable()
    {
        inputActions.Disable();
    }
    
}
