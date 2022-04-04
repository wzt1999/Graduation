using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryDes : MonoBehaviour {

	//单例
    public static InventoryDes instance;
    private Text desLabel;
	void Start () {
        instance = this;
        desLabel = transform.Find("Text").GetComponent<Text>();
        this.gameObject.SetActive(false);
	}
	void Update () {
        transform.position = Input.mousePosition + new Vector3(75, 75, 0);
	}
    //显示信息界面
    public void ShowInfo(int id)
    {
        this.gameObject.SetActive(true);
        //根据id获取到一条完整的信息
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        string infoDes = "";
        switch (info.objectType)
        {
            case ObjectType.Drug:
                infoDes = GetDrugDes(info);
                break;
            case ObjectType.Equip:
                infoDes = GetEquipDes(info);
                break;
            case ObjectType.Mat:
                infoDes = GetMatDes(info);
                break;
        }
       //更新文本描述框
        desLabel.text = infoDes;
    }
    //药品信息的拼接
    string GetDrugDes(ObjectInfo info)
    {
        string str = "";
        str += "名称:" + info.name + "\n";
        str += "+HP:" + info.hp + "\n";
        str += "+MP:" + info.mp + "\n";
        str += "出售价:" + info.priceSell + "\n";
        str += "购买价:" + info.priceBuy + "\n";
        return str;
    }
    //武器信息的拼接
    string GetEquipDes(ObjectInfo info)
    {
        string str = "";
        str += "名称:" + info.name + "\n";
        str += "+攻击力:" + info.attack + " ";
        str += "+防御力:" + info.def + "\n";
        str += "+速度:" + info.speed + " ";
        str += "穿戴类型:" + info.dressType + "\n";
        str += "出售价:" + info.priceSell + "\n";
        str += "购买价:" + info.priceBuy + "\n";
        return str;
    }
    //材料的信息拼接
    string GetMatDes(ObjectInfo info)
    {
        string str = "";
        str += "名称：" + info.name + "\n";
        str += "购买价：" + info.priceBuy + "\n";
        str += "出售价：" + info.priceSell + "\n";
        return str;
    }
    //隐藏界面
    public void HideInfo()
    {
        this.gameObject.SetActive(false);
    }
}
