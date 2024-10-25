using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private DialogConf[] dialogConfs;


    
    public static void CheckDialogConfs()
    {
        DialogConf[] dialogConfs = Resources.LoadAll<DialogConf>("Conf");
        //遍历全部对话数据的配置文件
        for (int i = 0; i < dialogConfs.Length; i++)
        {
            //遍历配置文件中的list模型
            for (int j = 0; j < dialogConfs[i].DialogModels.Count; j++)
            {
                if (dialogConfs[i].DialogModels[j].NPCConf == null || dialogConfs[i].DialogModels[j].NPCConf.head == null)
                {
                    Debug.LogError(dialogConfs[i].name + "---中的第" + j + "条数据缺失NPC配置或头像");

                }
            }
        }
        Debug.Log("检查完毕");
    }

    private void Awake()
    {
        Instance = this;
        dialogConfs = Resources.LoadAll<DialogConf>("Conf");
    }

    public DialogConf GetDialogConf(int index)
    {
        return dialogConfs[index];
    }


    /// <summary>
    /// 摄像机效果-闪烁
    /// </summary>
    public void ScreenEF(float delay)
    {
        StartCoroutine(DoScreenEF(delay));

    }

    private IEnumerator DoScreenEF(float delay)
    {
        GameObject.Find("Canvas/BG").GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(delay);
        GameObject.Find("Canvas/BG").GetComponent<Image>().color = Color.white;
    }
}
