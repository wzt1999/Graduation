using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCut : MonoBehaviour
{

    private List<ShortCutSlot> shortCutSlotList = new List<ShortCutSlot>();
	void Start () {
        for (int i = 0; i < 6; i++)
        {
            shortCutSlotList.Add(transform.Find("shortcut" + (1 + i)).GetComponent<ShortCutSlot>());
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //判断快捷栏是否已经有相同id的快捷键
    public bool JudgeShortCutSlotID(int id)
    {
        foreach (ShortCutSlot shortItem in shortCutSlotList)
        {
            if (shortItem.id == id)//说明快捷栏中已有该快捷方式
            {
                return false;
            }
        }
        return true;
    }
}
