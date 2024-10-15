using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("����")]

    [SerializeField] private  float speed=5f; //�ƶ��ٶ�


    //��ɫ�����ȡ___����

    [HideInInspector] public  AGameInputSystem inputActions;
    Vector2 inputDirection;
   
     [SerializeField] private Rigidbody2D rb; // ��Ҹ������
    //SpriteRenderer sr;
    //�ƶ������������
    private float damping = 0.1f;

    //WS������Ʒ���ͳ��
     public  bool setDirectionUp;
     public  bool setDirectionDown;

    [HideInInspector] public Vector2 lastInput;


    private void Awake()
    {
        inputActions = new AGameInputSystem();
        

    }
    private void Update()
    {
       
        inputDirection = inputActions.PlayerAction.Move.ReadValue<Vector2>();
        
        inputActions.PlayerAction.UpDirection.performed += UpDirectionSetting;
        inputActions.PlayerAction.UpDirection.canceled += CancelUpDirectionSetting;
        inputActions.PlayerAction.DownDirection.performed += DownDirectionSetting;
        inputActions.PlayerAction.DownDirection.canceled += CancelDownDirectionSetting;
        

        
    }
   
   
    private void FixedUpdate()
    {
        Move();
    }
    /// <summary>
    /// �ƶ�����
    /// </summary>
    private void Move()
    {
        //ͨ��������ƽ�ɫ�ƶ�
        float newspeed = Mathf.Lerp(rb.velocity.x, inputDirection.x * speed, 1f-damping);
        rb.velocity = new Vector2(newspeed, inputDirection.y);

        //���ƽ�ɫת��
        if (inputDirection.x != 0 && Mathf.Sign(inputDirection.x) != Mathf.Sign(transform.localScale.x))
        { 
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

    }
   
    /// <summary>
    /// �������ɿ����з������ú�ȡ����������(W)�� 
    /// </summary>
    /// <param name="context"></param>
     private void CancelUpDirectionSetting(InputAction.CallbackContext context)
        {
            setDirectionUp = false;
        }

    private void UpDirectionSetting(InputAction.CallbackContext context)
        {
            setDirectionUp = true;
        }
   /// <summary>
   /// �������ɿ����з������ú�ȡ����������(S)��
   /// </summary>
   /// <param name="context"></param>
    private void CancelDownDirectionSetting(InputAction.CallbackContext context)
        {
            setDirectionDown = false;
        }

    private void DownDirectionSetting(InputAction.CallbackContext context)
        {
            setDirectionDown = true;
        }
    

    public void GetDirection()
    {
        if (inputDirection.x != 0)
        {
            lastInput = inputDirection;
        }
    }


    

    /// <summary>
    /// InputSyste��������رա�
    /// </summary>
    private void OnEnable()
    {
         inputActions.Enable();
    }
    private  void OnDisable()
    {
        inputActions.Disable();
    }
   
    
}
