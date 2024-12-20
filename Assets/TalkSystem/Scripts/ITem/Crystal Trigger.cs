using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrystalTrigger : MonoBehaviour
{
    public bool Right;
    public bool Left;
    public bool Up;
    public bool Down;
    public GameObject[] brother;

    public float speed;
    public float timer;
    private float timers;


    private Rigidbody2D ParentRb2d;
    private Crystalreflection Parents;

    private float horizontalMove = 0.0f;
    private float verticalMove = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timers= 0f;
        ParentRb2d = GetComponentInParent<Rigidbody2D>();
        Parents = GetComponentInParent<Crystalreflection>();
    }

    private void Update()
    {
        timers-= Time.deltaTime;
        if (timers>0)
        {
            Vector2 moveDirection = new Vector2(horizontalMove, verticalMove);
            moveDirection *= speed;
            Debug.Log("001");
            ParentRb2d.velocity += moveDirection;
            for (int i = 0; i < brother.Length; i++)
            {
                brother[i].SetActive(false);
            }
        }
        else
        {
            ParentRb2d.velocity = Vector2.zero;
            for (int i = 0; i < brother.Length; i++)
            {
                brother[i].SetActive(true);
            }
        }
    }

        // Update is called once per frame

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (Parents.openKey || Parents.Player.isLittle)
                {
                    if (Right)
                    {
                    Parents.ReBoundPlayer(1);
                    timers = timer;
                    horizontalMove = -1.0f;
                    }
                    else if (Left)
                    {
                    Parents.ReBoundPlayer(2);
                    timers = timer;
                    horizontalMove = 1.0f;
                    }
                    else if (Up)
                    {
                    Parents.ReBoundPlayer(3);
                    timers = timer;
                    verticalMove = -1.0f;
                    }
                    else
                    {
                    Parents.ReBoundPlayer(4);
                    timers = timer;
                    verticalMove = 1.0f;
                    }
                }

            }
            if (collision.gameObject.CompareTag("LittlePlayer"))
            {
                if (Right)
                { Parents.ReBoundPlayer(1);
                timers = timer;
                horizontalMove = -1.0f;
                }
                else if (Left)
                { Parents.ReBoundPlayer(2);
                timers = timer;
                horizontalMove = 1.0f;
                }
                else if (Up)
                { Parents.ReBoundPlayer(3);
                timers = timer;
                verticalMove = -1.0f;
                }
                else
                { Parents.ReBoundPlayer(4);
                timers = timer;
                verticalMove = 1.0f;
                }
            }
        }
    }
