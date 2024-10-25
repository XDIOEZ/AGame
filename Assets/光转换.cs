using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 光转换 : MonoBehaviour
{
    public GameObject targetPrefab; // 在 Inspector 中拖入预制体引用
    Transform yuan;
    Transform shu;
    [Header("检测参数")]
    public Vector2 weizhi;
    public float fanwei;
    public LayerMask jianceduixiang;
    [Header("状态")]
    public bool jiechu;
    // Start is called before the first frame update

    void Start()
    {
        if (targetPrefab != null)
        {
            GameObject wanjia = Instantiate(targetPrefab); // 实例化
            Debug.Log("预制体实例化成功！");
            yuan = wanjia.transform.Find("圆型光圈");
            shu = wanjia.transform.Find("竖状光条");
            if (yuan != null) Debug.Log("找到了");
            if (shu != null) Debug.Log("找到了");
        }
        else
        {
            Debug.LogError("请在 Inspector 面板中分配 targetPrefab 引用。");
        }
    }

    // Update is called once per frame
    void Update()
    {
        jiancha();
        触发();
    }
    private void jiancha()
    {
        jiechu = Physics2D.OverlapCircle((Vector2)transform.position + weizhi, fanwei, jianceduixiang);
    }
    private void 触发()
    {
        if (jiechu)
        {
            yuan.gameObject.SetActive(false);
            shu.gameObject.SetActive(true);
        }
    }
}
