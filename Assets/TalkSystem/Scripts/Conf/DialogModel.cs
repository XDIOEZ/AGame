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
    

    [Required(ErrorMessage ="�ܵ�˵Щʲô��")]
    [VerticalGroup("NPC/NPCField"), HideLabel, MultiLineProperty(4)]
    public string NPCContent;

    [LabelText("NPC�¼�")]
    public List<DialogEventModel> DialogEventModels;
    [LabelText("���ѡ��")]
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
    [LabelText("��һ���Ի�")]
    NextDialog,
    [LabelText("�˳��Ի�")]
    ExitDialog,
    [LabelText("��ת�Ի�")]
    JumpDialog,
    [LabelText("��ĻЧ��")]
    ScreenEF
}



/// <summary>
/// �Ի��¼�����
/// </summary>
[Serializable]
public class DialogEventModel
{
    [HideLabel,HorizontalGroup("�¼�",Width=100)]
    public DialogEventEnmu DialogEvent;
    [HideLabel, HorizontalGroup("�¼�")]
    public string Args;

}

[Serializable]
public class DialogPlayerSelect
{
    [LabelText("ѡ������"),MultiLineProperty(2),LabelWidth(50)]
    public string Conent;
    [LabelText("�¼�")]
    public List<DialogEventModel> DialogEventMpdles;
}
