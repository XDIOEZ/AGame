using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActive : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossEffect;
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.GetInstance().AddEventListener("7色被点亮", () =>
        {
         //激活boss
        boss.SetActive(true);
            bossEffect.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
