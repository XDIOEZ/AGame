using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMoveWall : MonoBehaviour
{
    private GameObject MoveWall;
    private SpriteRenderer spriteRendererOne;
    private SpriteRenderer spriteRendererTwo;
    private ParticleSystem Effect;

    public Color pressedColor;

    public Transform StartPoint;
    public Transform EndPoint;

    private Transform PointEnd;
    private Transform PointStart;

    public float speed=1;
    private bool Movekey=false;
    public bool isStart;


    // Start is called before the first frame update
    void Start()
    {
        Effect = GameObject.Find("ButtonEffect").gameObject.GetComponent<ParticleSystem>();

        spriteRendererOne = gameObject.GetComponent<SpriteRenderer>();


        MoveWall = GameObject.Find("CanMoveWall").gameObject;



        PointStart = StartPoint;

        PointEnd = EndPoint;

        pressedColor = Color.yellow;
    }


    private void Update()
    {
        if (Movekey)
        {
            MoveWallGo();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Effect.transform.position = transform.position;
        Effect.Play();
        spriteRendererOne.color = pressedColor;
        if (collision.gameObject.CompareTag("Player")) 
        {
            Movekey = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isStart)
        {
            MoveWallGo();
        }
        else
        {
            MoveWallBack();
        }
    }


    private void MoveWallGo()
    {
        //Movekey = true;
        Vector2 direction = PointEnd.position - MoveWall.transform.position;

        float dinstace = Vector2.Distance(MoveWall.transform.position, PointEnd.position);
        if (dinstace < 1.5)
        {
            Movekey = false;
        }
        else
        {
            MoveWall.transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void MoveWallBack()
    {
        Vector2 direction = PointStart.position - MoveWall.transform.position;

        float dinstace = Vector2.Distance(MoveWall.transform.position, PointEnd.position);
        if (dinstace < 1.5)
        {
            Movekey = false;
        }
        else
        {
            MoveWall.transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
