using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActive : MonoBehaviour
{
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.GetInstance().AddEventListener("7ɫ������", () =>
        {
         //����boss
        boss.SetActive(true);

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
