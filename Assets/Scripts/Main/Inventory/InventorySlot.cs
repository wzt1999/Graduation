using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour {

    public int id;
	void Start () {
		
	}
	
	void Update () {
		
	}
    //相同物品数量叠加
    public void PlusNumber(int number)
    {
        InventoryItem item = this.GetComponentInChildren<InventoryItem>();
        item.SetNum(number);
        
    }
    //更新物品信息
    public void SetSlotInfo(int id,int num)
    {
        this.id = id;
        InventoryItem item = this.GetComponentInChildren<InventoryItem>();
        item.SetItemInfo(id, num);
    }
    //设置id，其实就是用于物品交换后需要重置格子id信息
    public void SetId(int id)
    {
        this.id = id;
    }
    //清空格子信息
    public void CleanInfo()
    {
        this.id = 0;
    }
    public int MinusNumber(int num = 1)
    {
        InventoryItem item = this.GetComponentInChildren<InventoryItem>();
        if (item != null)
        {
            return item.JudgeItemNum();
        }
        else
        {
            Debug.Log("该格子下没有物品");
            return -1;
        }
    }
}
