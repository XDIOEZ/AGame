using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("属性")]
    
    public float speed;

    //角色方向获取___参数

    AGameInputSystem inputActions;
    Vector2 inputDirection;
    //角色组件___参数 

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
        //通过组件控制角色移动
        rb.velocity = new Vector2(inputDirection.x * speed, inputDirection.y);

        //控制角色转向
        if (inputDirection.x != 0 && Mathf.Sign(inputDirection.x) != Mathf.Sign(transform.localScale.x))
        { 
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

    }
   

        //Input System的启用与关闭
    private  void OnEnable()
    {
         inputActions.Enable();
    }
    private  void OnDisable()
    {
        inputActions.Disable();
    }
    
}
