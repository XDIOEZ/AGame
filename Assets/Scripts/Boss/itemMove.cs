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
        EventCenter.GetInstance().AddEventListener("7色被点亮", () =>
        {
            Debug.Log("7色被点亮");
            //物体在2D平面上向上移动,使用Dotween动画
            transform.DOMove(new Vector3(transform.position.x, transform.position.y + itemMoveDistance, transform.position.z), 1f);
          
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
