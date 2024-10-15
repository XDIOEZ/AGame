using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("属性")]

    [SerializeField] private  float speed=5f; //移动速度


    //角色方向获取___参数

    [HideInInspector] public  AGameInputSystem inputActions;
    Vector2 inputDirection;
   
     [SerializeField] private Rigidbody2D rb; // 玩家刚体组件
    //SpriteRenderer sr;
    //移动阻尼参数设置
    private float damping = 0.1f;

    //WS方向控制发射和冲刺
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
    /// 移动方法
    /// </summary>
    private void Move()
    {
        //通过组件控制角色移动
        float newspeed = Mathf.Lerp(rb.velocity.x, inputDirection.x * speed, 1f-damping);
        rb.velocity = new Vector2(newspeed, inputDirection.y);

        //控制角色转向
        if (inputDirection.x != 0 && Mathf.Sign(inputDirection.x) != Mathf.Sign(transform.localScale.x))
        { 
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

    }
   
    /// <summary>
    /// 长按和松开进行方向设置和取消方向设置(W)。 
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
   /// 长按和松开进行方向设置和取消方向设置(S)。
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
    /// InputSyste的启用与关闭。
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
