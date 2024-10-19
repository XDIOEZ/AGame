using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Options_Item : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private Image image;
    private Text text;
    private bool isSelect;

    private Color blackColor = new Color(0, 0, 0, 0.6f);
    private Color whiteColor = new Color(1, 1, 1, 0.6f);

    private DialogPlayerSelect dialogPlayerSelect;


    public bool IsSelect { 
        get => isSelect;
        set
        {
            isSelect = value;
            if (isSelect)
            {
                image.color = blackColor;
                text.color = Color.white;
            }
            else
            {
                image.color = whiteColor;
                text.color = Color.black;

            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsSelect = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsSelect = false;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < dialogPlayerSelect.DialogEventMpdles.Count; i++)
        {
            UI_Dialog.Instance.ParseDialogEvent(dialogPlayerSelect.DialogEventMpdles[i].DialogEvent, dialogPlayerSelect.DialogEventMpdles[i].Args);

        }
    }

    public void Init(DialogPlayerSelect model)
    {
        image = GetComponent<Image>();
        text = transform.Find("Text").GetComponent<Text>();
        dialogPlayerSelect=model;
        text.text = model.Conent;

        IsSelect = false;

    }


}
