using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "�Ի�����", menuName = "�Ի�����/�����Ի�")]
public class DialogConf : ScriptableObject
{
    [LabelText("�Ի�����")]
    [ListDrawerSettings(ShowIndexLabels = true,AddCopiesLastElement =true)]
    public List<DialogModel> DialogModels;
}
