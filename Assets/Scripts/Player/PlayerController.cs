using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("属性")]
    
    public float speed;
    //角色方向获取___参数
    InputActions inputActions;
    Vector2 inputDirection;
    //角色组件___参数 
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



    //Input System的启用与关闭
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
