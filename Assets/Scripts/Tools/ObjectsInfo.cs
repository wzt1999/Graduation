using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInfo : MonoBehaviour
{

    //作为单例
    public static ObjectsInfo instance;
    //获取配置表
    private TextAsset objectInfoList;
    private Dictionary<int, ObjectInfo> objectInfoDic = new Dictionary<int, ObjectInfo>();
    void Awake()
    {
        instance = this;
        ReadInfo();
    }
    void ReadInfo()
    {
        objectInfoList = Resources.Load<TextAsset>("text/Iteminformation");
        //获取配置表中的所有文本信息
        string text = objectInfoList.text;
        //得到每一行的信息
        string[] strArray = text.Split('\n');
        //把每一行数据拆分成单独的每一个数据
        foreach (string str in strArray)
        {
            //得到每一个具体的数据
            string[] proArray = str.Split(',');
            ObjectInfo info = new ObjectInfo();

            //得到id
            info.id = int.Parse(proArray[0]);
            info.name = proArray[1];//名字
            info.iconName = proArray[2];//图片名字
            info.objectType = (ObjectType)System.Enum.Parse(typeof(ObjectType), proArray[3]);
            if (info.objectType == ObjectType.Drug)
            {
                info.hp = int.Parse(proArray[4]);
                info.mp = int.Parse(proArray[5]);
                info.priceSell = int.Parse(proArray[6]);
                info.priceBuy = int.Parse(proArray[7]);
            }
            else if (info.objectType == ObjectType.Equip)//如果是装备类型
            {

                info.attack = int.Parse(proArray[4]);
                info.def = int.Parse(proArray[5]);
                info.speed = int.Parse(proArray[6]);
                info.dressType = (DressType)System.Enum.Parse(typeof(DressType), proArray[7]);
                info.priceSell = int.Parse(proArray[8]);//出售价
                info.priceBuy = int.Parse(proArray[9]);//购买价
            }
            else if (info.objectType == ObjectType.Mat)
            {
                info.priceBuy = int.Parse(proArray[4]);
                //string[] s= proArray[5].Split('\r');
                info.priceSell = int.Parse(proArray[5]);
            }
            objectInfoDic.Add(info.id, info);
        }
    }
    //给外部使用的接口，根据id查询信息
    public ObjectInfo GetObjectInfoById(int id)
    {
        ObjectInfo info = null;
        objectInfoDic.TryGetValue(id, out info);
        return info;
    }

}
//穿戴类型
public enum DressType
{
    Headgear,
    Armor,
    Shoe,
    Weapon,
    Accessory
}
public enum ObjectType
{
    Drug,
    Equip,
    Mat
}

//药品                         装备                                 材料
//id                            id                                  id
//物品名称		                物品名称                            物品名称
//图片名字                      图片名字                            图片名字
//类型（药品，装备，材料）      类型（药品，装备，材料）            类型（药品，装备，材料）
//加血量                        加攻击                              购买价
//加蓝量                        加防御                              出售价
//出售价                        加速度
//购买价                        穿戴类型
//                              出售价
//         

public class ObjectInfo
{
    public int id;
    public string name;
    public string iconName;
    public ObjectType objectType;
    public int hp;
    public int mp;
    public int priceSell;
    public int priceBuy;
    public int attack;
    public int def;
    public int speed;
    public DressType dressType;
}