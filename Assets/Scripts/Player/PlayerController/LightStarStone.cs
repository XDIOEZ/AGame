using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStarStone : MonoBehaviour
{
    public GameObject targetObject;//��ȡ���壬����Ϊ��ʯ�ϵ�Canvas���

    //�ж�����
    [SerializeField] bool isInArea;//�ж��Ƿ񵽴���ʯ���ж�����
    
    
    
   

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
