using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : BasePanel {
    public static Inventory instance;
    private Text coinNumberLabel;
    private int slotnumber;//动态获取格子的个数，方便扩容
    private int coinNumber;//金币数目
    private List<InventorySlot> slotList = new List<InventorySlot>();//获取所有的格子
 
	public override void Start () {
        base.Start();
        instance = this;
        coinNumberLabel = transform.Find("coin/Text").GetComponent<Text>();
        slotnumber = transform.Find("content").childCount;
        for (int i = 0; i < slotnumber; i++)
        {
            slotList.Add(transform.Find("content/inventorySlot" + (i + 1)).GetComponent<InventorySlot>());
        }
         UpdateCoinLabel(1000);
	}
	public void UpdateCoinLabel(int coinNum)
    {
        coinNumber += coinNum;
        coinNumberLabel.text = coinNumber.ToString();
    }
    //顶部的金币数
    public int GetCoinTopNumber()
    {
       return coinNumber;
    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //C#中的随机数，是左闭右开的区间
            GetId(Random.Range(3001, 3014));
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetId(Random.Range(2001, 2037));
        }
	}
    public override void TransformState()
    {
        base.TransformState();
    }
    //根据id给背包中添加物品
   public void GetId(int id,int count=1,int lv=0)
    {
        //根据id获取当前物品的所有信息
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        //1.遍历所有的格子 1.有东西-》id是否相同-》合并   2.有东西-》id不同-》继续寻找下一个空格子   空格子没有-》扩容/丢弃
        //保存找到符合条件的格子的
        InventorySlot slot = null;
        foreach (InventorySlot temp in slotList)
        {
            //格子的id和传进来的id相同时，说明该物体已经存在
            if (temp.id==id)
            {
                slot = temp;
                break;
            }
        }
        //找到有物品的格子，数量+count,数量是要加到物品上去的
        if (slot!=null)
        {
            slot.PlusNumber(count);
        }
        //没有找到相同物品，寻找空格子，然后存入
        else
        {
            foreach (InventorySlot temp in slotList)
            {//空格子
                if (temp.id==0)
                {
                    slot = temp;
                    break;
                }
            }
            //找到空格子后需要往空格子里面添加物品
            if (slot!=null)
            {
                //创建物品
                GameObject item = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/inventoryItem"));
                //将物品放入到格子中
               Transform trans = slot.gameObject.transform;//获取格子的recttransform
                item.transform.SetParent(trans);
                //将物品调整到格子中间
                item.transform.localPosition = Vector3.zero;
                item.transform.localScale = new Vector3(1, 1, 1);
                //更新格子信息
                slot.SetSlotInfo(id, count);
            }
            else
            {
                //格子满了,扩容
                return;
            }
        }

    }
    //判断金币是否足够
    public bool GetCoin(int count)
    {
        if (coinNumber>=count)
        {
            coinNumber -= count;
            //显示金币
            coinNumberLabel.text = coinNumber.ToString();
            return true;
        }
        return false;
    }
    
    //判断背包物品数量
    public int MinusItem(int id, int count = 1)
    {
        InventorySlot tempSlot = null;
        foreach (InventorySlot slot in slotList)
        {
            if (slot.id == id)
            {
                tempSlot = slot;
                break;
            }
        }
        if (tempSlot != null)
        {
            return tempSlot.MinusNumber(count);
        }
        else//物品不存在
        {
            Debug.Log("该id所对应的物品不存在");
            return -1;
        }
    }
}
