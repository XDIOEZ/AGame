using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform  target;   //���λ��

    public Transform BackGround,foreGround;  //����

    private Vector2 lastPos;//������λ��


    void Start()
    {
        lastPos = transform.position;
    }

   
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, target.position.z);//����������λ��

        Vector2 moveAmount = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        BackGround.position += new Vector3(moveAmount.x, moveAmount.y, 0f);

        foreGround.position += new Vector3(-moveAmount.x , moveAmount.y , 0f);

        lastPos = transform.position;

    }
}
