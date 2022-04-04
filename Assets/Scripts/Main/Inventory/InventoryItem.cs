using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//导入事件头文件
using UnityEngine.EventSystems;
public class InventoryItem : MonoBehaviour ,IBeginDragHandler,IDragHandler,IEndDragHandler,ICanvasRaycastFilter,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private int id;
    private int number;
    private Image icon;
    private Text numberLabel;

    private Transform canvasTrans;
    //默认射线是不穿透物品
    private bool isRaycastLocationValid = true;
    //记录当前物品是属于哪个格子的
    private Transform nowParent;
    public static bool isDrug = false;

   private ObjectInfo objectInfo;
	void Awake () {
        icon = this.GetComponent<Image>();
        numberLabel = transform.Find("Text").GetComponent<Text>();
        canvasTrans = GameObject.Find("Canvas").transform;
	}
	void Update () {
	}
    //设置物品数量（相同物品id的情况）
    public void SetNum(int count)
    {
        number += count;
        numberLabel.text = number.ToString();
    }
    //更新物品信息
    public void SetItemInfo(int id,int count)
    {
        this.id = id;
        number += count;
        objectInfo = ObjectsInfo.instance.GetObjectInfoById(id);
        icon.sprite = Resources.Load<Sprite>("Icon/" + objectInfo.iconName);
        numberLabel.text = number.ToString();
    }

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrug = true;
        //保存被拖拽物体的格子信息
        nowParent = transform.parent;
        transform.SetParent(canvasTrans);
        //ui穿透，当拖拽物体的时候鼠标下一直有物体跟随遮挡，如果不穿透那么将无法获取到鼠标终点位置的信息
        isRaycastLocationValid = false;
    }
    //拖拽过程
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    //拖拽结束
    public void OnEndDrag(PointerEventData eventData)
    {
        //获取鼠标终点位置下的物品信息
        GameObject gomouse = eventData.pointerCurrentRaycast.gameObject;
        //鼠标下有物体
        if (gomouse!=null)
        {
            //当物体拿起后，马上松手，物体还是在原来的格子，会导致id会清空，避免拿起来就放下数据被清空问题
            if (gomouse.tag.Equals(nowParent.name))
            {
                SetParentAndPosition(transform, nowParent);
            }
            //鼠标终点位置下是空格子 1.物品要放入到新的空格子，位置要居中  2.格子信息要交换
           else if (gomouse.tag.Equals(Tags.slot))
            {
                //设置父节点
                SetParentAndPosition(transform, gomouse.transform);
                //获取当前格子信息
                InventorySlot slot1 = nowParent.GetComponent<InventorySlot>();
                //获取鼠标位置下的格子信息
                InventorySlot slot2 = gomouse.GetComponent<InventorySlot>();
                //将原来格子的id设置给新的格子
                slot2.SetId(slot1.id);
                //将原来的格子id设置为0
                slot1.SetId(0);
            }
              //鼠标终点位置下是物品 ，交换位置
            else if (gomouse.tag.Equals(Tags.item))
            {
                //交换之前需要保存两个格子信息
                //获取当前格子的信息
                InventorySlot slot1 = nowParent.GetComponent<InventorySlot>();
                //获取鼠标位置下的格子信息,此时的gomouse是物品
                InventorySlot slot2 = gomouse.transform.parent.GetComponent<InventorySlot>();
                int id = slot2.id;
                //将原来格子的id设置给新的格子
                slot2.SetId(slot1.id);
                //将原来的格子id设置为
                slot1.SetId(id);
                //将被拖拽的物品放入到鼠标终点位置下的格子中
                SetParentAndPosition(transform, gomouse.transform.parent);
                //将鼠标位置下的物品信息放入到被拖拽物品的格子下
                SetParentAndPosition(gomouse.transform, nowParent);
            }
            //鼠标终点位置下的快捷栏
            else if (gomouse.tag.Equals(Tags.shortcutslot))
            {
                if (objectInfo.objectType.Equals(ObjectType.Drug))
                {
                    gomouse.GetComponent<ShortCutSlot>().SetDrugInfo(id);
                    SetParentAndPosition(transform, nowParent);                    
                }
                else
                {
                    SetParentAndPosition(transform, nowParent);
                }
            }
            //鼠标终点位置下是无效位置，放回原来的位置
            else
            {
                SetParentAndPosition(transform, nowParent);
            }
        }
        //鼠标下没有物体，可以处理成丢弃物品
        else
        {
            //获取当前格子信息
            InventorySlot slot1 = nowParent.GetComponent<InventorySlot>();
            //清除当前格子信息
            slot1.SetId(0);
            //销毁物品
            Destroy(this.gameObject);
        }
        //拖拽结束后，关闭ui穿透
        isRaycastLocationValid = true;
        isDrug = false;
    }
    

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return isRaycastLocationValid;
    }
    //设置物品的父节点以及位置信息
    void SetParentAndPosition(Transform child, Transform parent)
    {
        child.SetParent(parent);
        child.position = parent.position;
    }
    //当鼠标放上去的时候会调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isDrug)
        {
            InventoryDes.instance.ShowInfo(id);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryDes.instance.HideInfo();
    }
    //鼠标点击的时候用
    public void OnPointerClick(PointerEventData eventData)
    {
        OptionPanel.instance.ShowOptionPanel(id);
       
        //bool isSuccess=GameObject.Find("Canvas/Player/equipment").GetComponent<EquipmentUI>().Dress(id);
        //bool isSuccess = OptionPanel.instance.IsSuccessEquip();
        //if (isSuccess)
        //{
        //    JudgeItemNum();
        //}
    }
    public int JudgeItemNum(int num = 1)
    {
        if (this.number >= num)
        {
            this.number -= num;
            if (this.number == 0)
            {
                //格子里面数据清空   
                transform.parent.GetComponent<InventorySlot>().CleanInfo();
                //关闭显示提示框
                InventoryDes.instance.HideInfo();
                //清除物品本身
                GameObject.Destroy(this.gameObject);
                return 0;
            }
            numberLabel.text = this.number.ToString();
        }
        return this.number;
    }
}
