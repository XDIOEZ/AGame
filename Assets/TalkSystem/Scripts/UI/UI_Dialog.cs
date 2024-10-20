using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class UI_Dialog : MonoBehaviour
{
    public static UI_Dialog Instance;
    private Image head;
    private Text nameText;
    private Text mainText;
    private RectTransform content;
    private Transform Options;
    private GameObject prefab_OptionItem;

    private DialogConf currConf;
    private int currIndex;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        head = transform.Find("Main/Head").GetComponent<Image>();
        nameText= transform.Find("Main/Name").GetComponent<Text>();
        //mainText = transform.Find("Main/MainText").GetComponent<Text>();
        mainText = transform.Find("Main/Scroll View/Viewport/Content/MainText").GetComponent<Text>();
        content = transform.Find("Main/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        Options = transform.Find("Options");
        prefab_OptionItem = Resources.Load<GameObject>("Options_Item");
        TestDialog();
    }

    /// <summary>
    /// 开始对话
    /// </summary>
    private void TestDialog()
    {
        currConf=GameManager.Instance.GetDialogConf(0);

        currIndex = 0;

        StartDialog(currConf, currIndex);
    }

    private void StartDialog(DialogConf conf,int index)
    {
        DialogModel model = conf.DialogModels[index];
        head.sprite = model.NPCConf.head;
        nameText.text = model.NPCConf.name;

        StartCoroutine(DoMainTextEF(model.NPCContent));
        //NPC
        for (int i = 0; i < model.DialogEventModels.Count; i++)
        {
            ParseDialogEvent(model.DialogEventModels[i].DialogEvent, model.DialogEventModels[i].Args);
        }
        //Player删除选项
        Transform[] items = Options.GetComponentsInChildren<Transform>();
        for (int i =1;i<items.Length ;i++)
        {
            Destroy(items[i].gameObject);
        }
        //Player生成选项
        for (int i = 0; i < model.dialogPlayerSelects.Count; i++)
        {
            UI_Options_Item item = GameObject.Instantiate<GameObject>
                (prefab_OptionItem,Options).GetComponent<UI_Options_Item>();
            item.Init(model.dialogPlayerSelects[i]);
        }
    }

    public void ParseDialogEvent(DialogEventEnmu dialogEvent,string agrs)
    {
        switch (dialogEvent)
        {
            case DialogEventEnmu.NextDialog:
                NextDialoigEvent();
                break;
            case DialogEventEnmu.ExitDialog:
                ExitDialoigEvent();
                break;
            case DialogEventEnmu.JumpDialog:
                JumpDialoigEvent(int.Parse(agrs));
                break;
            case DialogEventEnmu.ScreenEF:
                GameManager.Instance.ScreenEF(float.Parse(agrs));
                break;
            default:
                break;
        }
       
    }

    /// <summary>
    /// 下一条
    /// </summary>
    void NextDialoigEvent()
    {
        currIndex += 1;
        StartDialog(currConf, currIndex);
    }

    /// <summary>
    /// 离开
    /// </summary>
    void ExitDialoigEvent()
    {
        Debug.Log("离开成功");
    }

    /// <summary>
    /// 跳转
    /// </summary>
    void JumpDialoigEvent(int index)
    { 
        currIndex= index;
        StartDialog(currConf,currIndex);
    }


    IEnumerator DoMainTextEF(string txt)
    {

        // 字符数量决定了 conteng的高 每23个字符增加25的高
        float addHeight = txt.Length / 23 + 1;
        content.sizeDelta = new Vector2(content.sizeDelta.x, addHeight*25);

        string currStr ="";
        for (int i = 0; i < txt.Length; i++)
        {
            currStr += txt[i];
            yield return new WaitForSeconds(0.08f);
            mainText.text = currStr;
            // 每满23个字，下移一个距离 25
            if (i>23*3&&i % 23 == 0)
            {
                content.anchoredPosition = new Vector2(content.anchoredPosition.x, content.anchoredPosition.y+25);
            }
        }
    }
   
}
