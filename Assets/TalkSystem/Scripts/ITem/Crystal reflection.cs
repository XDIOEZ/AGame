using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Crystalreflection: MonoBehaviour
{
    private PlayerLittleState Player;
    private LittleStateMovement LightBall;


    private BoxCollider2D boxCollider2D;

    private float timer=0;

    private bool openKey;

    public float speed; // 物体的移动速度


    private void Start()
    {

        Player = GameObject.Find("Circle").GetComponent<PlayerLittleState>();
        LightBall = GameObject.Find("Circle").GetComponent<LittleStateMovement>();
        EventCenter.Instance.AddEventListener<object>("IsDashValue", (object obj) =>
        {
            PlayerMove player = obj as PlayerMove; // 将 obj 转换为 PlayerMove
            if (player != null)
            {
                openKey = player.isDashing;
            }
        });
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (openKey||Player.isLittle)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
        else
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }

    }

    private Vector2 CalculateMoveDirection(Vector2 playerMoveDirection)
    {
        Vector2 moveDirection = Vector2.zero;

        if (playerMoveDirection.x > 0 && Math.Abs(playerMoveDirection.y) < Math.Abs(playerMoveDirection.x))
        {
            moveDirection.x = 1;
        }
        else if (playerMoveDirection.x < 0 && Math.Abs(playerMoveDirection.y) < Math.Abs(playerMoveDirection.x))
        {
            moveDirection.x = -1;
        }
        else if (playerMoveDirection.y > 0 && Math.Abs(playerMoveDirection.y) > Math.Abs(playerMoveDirection.x))
        {
            moveDirection.y = 1;
        }
        else if (playerMoveDirection.y < 0 && Math.Abs(playerMoveDirection.y) > Math.Abs(playerMoveDirection.x))
        {
            moveDirection.y = -1;
        }
        Debug.Log(moveDirection);
        return moveDirection;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (openKey)
            {
                timer = 0.1f;
                Vector2 playerMoveDirection = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                Vector2 objectMoveDirection = CalculateMoveDirection(playerMoveDirection);
                //GetComponent<Rigidbody2D>().velocity = objectMoveDirection * speed;
            }
        }
    }

    public void ReBoundPlayer(int number)
    {
        if (openKey||Player.isLittle)
        {
            Player.canTransformToLightBall = true;
            if (Player.isLittle)
            {
                LightBall.canMove = true;
                if (number == 1)
                { LightBall.isRight = true; }
                else if (number == 2)
                { LightBall.isLeft = true; }
                else if (number == 3)
                { LightBall.isUp = true; }
                else
                { LightBall.isDown = true; }
            }
            else 
            {
                Player.SwitchPlayerState();
                if (LightBall != null)
                {
                    if (number == 1)
                    { LightBall.isRight = true; }
                    else if (number == 2)
                    { LightBall.isLeft = true; }
                    else if (number == 3)
                    { LightBall.isUp = true; }
                    else
                    { LightBall.isDown = true; }

                }
            }
        }
    }
}
