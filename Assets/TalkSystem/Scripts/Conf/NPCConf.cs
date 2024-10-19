using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NPC配置",menuName="角色配置/新增角色" )]
public class NPCConf : ScriptableObject
{
    [HorizontalGroup("NPC", 75, LabelWidth = 50), HideLabel,PreviewField(75)]
    public Sprite head;
    [VerticalGroup("NPC/NPCField"),LabelText("姓名")]
    public string Name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
