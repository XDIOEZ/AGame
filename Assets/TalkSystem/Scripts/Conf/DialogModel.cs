using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[Serializable]
public class DialogModel
{
    [HideLabel]
    [OnValueChanged("NPCConfOnValueChanged")]
    public NPCConf NPCConf;
    [HorizontalGroup("NPC", 75, LabelWidth = 50),ReadOnly, HideLabel, PreviewField(75)]
    public Sprite NPChead;
    

    [Required(ErrorMessage ="总得说些什么吧")]
    [VerticalGroup("NPC/NPCField"), HideLabel, MultiLineProperty(4)]
    public string NPCContent;

    [LabelText("NPC事件")]
    public List<DialogEventModel> DialogEventModels;
    [LabelText("玩家选择")]
    public List<DialogPlayerSelect> dialogPlayerSelects;

    public void NPCConfOnValueChanged()
    {
        if (NPCConf == null || NPCConf.head == null)
        {
            NPChead = null;
        }
        else
        {
            NPChead=NPCConf.head;
        }
    }


}

public enum DialogEventEnmu
{
    [LabelText("下一条对话")]
    NextDialog,
    [LabelText("退出对话")]
    ExitDialog,
    [LabelText("跳转对话")]
    JumpDialog,
    [LabelText("屏幕效果")]
    ScreenEF
}



/// <summary>
/// 对话事件数据
/// </summary>
[Serializable]
public class DialogEventModel
{
    [HideLabel,HorizontalGroup("事件",Width=100)]
    public DialogEventEnmu DialogEvent;
    [HideLabel, HorizontalGroup("事件")]
    public string Args;

}

[Serializable]
public class DialogPlayerSelect
{
    [LabelText("选项文字"),MultiLineProperty(2),LabelWidth(50)]
    public string Conent;
    [LabelText("事件")]
    public List<DialogEventModel> DialogEventMpdles;
}
