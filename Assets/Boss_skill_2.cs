using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_skill_2 : MonoBehaviour
{
    public int amount;
    public GameObject Enemys;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //���ű�������ʱ


    public void OnEnable()
    {
        Debug.Log("Boss skill 2 activated");
        //��transformΪԲ����8Ϊ�뾶��Բ�ķ�Χ���������amount������
        for (int i = 0; i < amount; i++)
        {
            Vector3 pos = Random.insideUnitCircle * 8;
            pos.z = 0;
            Instantiate(Enemys, transform.position + pos, Quaternion.identity);
        }
    }
    // Update is called once per frame
   
}
