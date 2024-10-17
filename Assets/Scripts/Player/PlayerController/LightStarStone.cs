using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStarStone : MonoBehaviour
{
    public GameObject targetObject;

    //ÅÐ¶¨Ìõ¼þ
    [SerializeField] bool isInArea;
    
    
    
   

    private void Update()
    {
        if (isInArea == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
               if (targetObject != null)
                {
                    Destroy(targetObject);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StarStone"))
        {
            isInArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("StarStone"))
        {
            isInArea = false;
        }
    }


}
