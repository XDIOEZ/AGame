using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemMove : MonoBehaviour
{
    public int itemMoveDistance;
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.GetInstance().AddEventListener("7ɫ������", () =>
        {
            Debug.Log("7ɫ������");
            //������2Dƽ���������ƶ�,ʹ��Dotween����
            transform.DOMove(new Vector3(transform.position.x, transform.position.y + itemMoveDistance, transform.position.z), 1f);
          
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
