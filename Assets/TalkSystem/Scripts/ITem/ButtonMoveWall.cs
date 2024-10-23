using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMoveWall : MonoBehaviour
{
    private GameObject MoveWall;
    private GameObject Button;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem Effect;

    public Color pressedColor;
    private Transform PointEnd;

    private Transform PointStart;

    public float speed=1;
    private bool Movekey=false;


    // Start is called before the first frame update
    void Start()
    {
        Effect = transform.Find("ButtonEffect").gameObject.GetComponent<ParticleSystem>();

        spriteRenderer = transform.Find("Button").gameObject.GetComponent<SpriteRenderer>();

        MoveWall = transform.Find("CanMoveWall").gameObject;

        PointEnd = transform.Find("End").transform;

        PointStart = transform.Find("Start").transform;

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
        Effect.Play();
        spriteRenderer.color = pressedColor;
        if (collision.gameObject.CompareTag("Player")) 
        {
            Movekey = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        MoveWallBack();
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

    }
}
