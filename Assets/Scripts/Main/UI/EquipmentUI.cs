using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour {
    
    private Transform headGear;
    private Transform armor;
    private Transform shoe;
    private Transform weapon;
    private Transform accessory;
    private PlayerStatus playerStatus;
    private List<EquipmentItem> equipItemList = new List<EquipmentItem>();
    private int attack;
    private int def;
    private int speed;
	void Start () {
        headGear = transform.Find("person/headgear");
        armor = transform.Find("person/armor");
        shoe = transform.Find("person/shoe");
        weapon = transform.Find("person/weapon");
        accessory = transform.Find("person/accessory");
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
	}
	
	
	void Update () {
     
	}
    public bool Dress(int id)
    {
       ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        //不是装备类型
        if (info.objectType!=ObjectType.Equip)
        {
            return false;
        }
       Transform dressParent = null;
        switch (info.dressType)
        {
            case DressType.Headgear:
                dressParent = headGear;
                break;
            case DressType.Armor:
                dressParent = armor;
                break;
            case DressType.Shoe:
                dressParent = shoe;
                break;
            case DressType.Weapon:
                dressParent = weapon;
                break;
            case DressType.Accessory:
                dressParent = accessory;
                break;
        }
        EquipmentItem equip = dressParent.GetComponentInChildren<EquipmentItem>();
        //有装备需要进行替换 同id装备，不同id但是穿戴部位相同
        if (equip != null)
        {
            if (equip.id == id)
            {
                Debug.Log("该部位已经穿戴了相同id的装备");
                return false;
            }
            //替换
            Inventory.instance.GetId(equip.id);
            equip.SetInfo(info);
        }
        else
        {
            GameObject equipItem = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/equipItem"));
            equipItem.transform.SetParent(dressParent);
            equipItem.transform.localPosition = Vector3.zero;
            equipItem.GetComponent<EquipmentItem>().SetInfo(info);
            equipItemList.Add(equipItem.GetComponent<EquipmentItem>());
        }
        UpdateProperty();
        return true;
    }
    //更新装备属性
    void UpdateProperty()
    {
        this.attack = 0;
        this.def = 0;
        this.speed = 0;
        //统计装备槽里面装备的总属性
        foreach (EquipmentItem item in equipItemList)
        {
            ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(item.id);
            this.attack += info.attack;
            this.def += info.def;
            this.speed += info.speed;
        }
        playerStatus.attackEquip = this.attack;
        playerStatus.defEquip = this.def;
        playerStatus.speedEquip = this.speed;
        Status.instance.UpdateShow();
    }
    //卸下装备
    public void TakeOffEquip(int id, GameObject equipItem)
    {
        Inventory.instance.GetId(id);//放进背包
        equipItemList.Remove(equipItem.GetComponent<EquipmentItem>());//从列表中移除，从格子中移除
        GameObject.Destroy(equipItem);//删除掉装备
        UpdateProperty();
    }
}
