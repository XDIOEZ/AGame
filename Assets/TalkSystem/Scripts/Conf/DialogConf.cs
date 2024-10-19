using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "对话配置", menuName = "对话配置/新增对话")]
public class DialogConf : ScriptableObject
{
    [LabelText("对话数据")]
    [ListDrawerSettings(ShowIndexLabels = true,AddCopiesLastElement =true)]
    public List<DialogModel> DialogModels;
}
