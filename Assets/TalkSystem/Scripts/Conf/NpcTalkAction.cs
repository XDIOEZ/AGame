using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTalkAction : MonoBehaviour
{
    public static NpcTalkAction instance;
    public GameObject TalkSystem;
    private GameObject player;
    public float timer;

    public float distances;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        player = GameObject.Find("PlayerRemake");
        TalkSystem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        distances=Vector2.Distance(player.transform.position,transform.position);
        if (distances < 2)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                timer = 10;
                TalkSystem.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                }
            }

        }
        if (distances > 5)
        {
            timer-= Time.deltaTime;
            if (timer<0) 
            {
                TalkSystem.SetActive(false);
                timer = 7;
            }
            
        }
    }
}
