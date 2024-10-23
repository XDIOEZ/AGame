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

    //当脚本被激活时


    public void OnEnable()
    {
        Debug.Log("Boss skill 2 activated");
        //以transform为圆心以8为半径在圆的范围内随机生成amount个敌人
        for (int i = 0; i < amount; i++)
        {
            Vector3 pos = Random.insideUnitCircle * 8;
            pos.z = 0;
            Instantiate(Enemys, transform.position + pos, Quaternion.identity);
        }
    }
    // Update is called once per frame
   
}
