using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SkillsInfo : MonoBehaviour {
    public static SkillsInfo instance;
    private Dictionary<int, SkillInfo> skillInfoDict = new Dictionary<int, SkillInfo>();
	void Awake () {
        instance = this;
        ReadInfo();
       
	}
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.O))
        {
           SkillInfo info= GetSkillInfoById(10001);
           Debug.Log(info);
        }
	}
    void ReadInfo()
    {
        string skillText = (Resources.Load<TextAsset>("Text/SkillsInfoList")).text;//获取整张配置表
        string[] skillInfoArray = skillText.Split('\n');
        foreach (string skillInfoStr in skillInfoArray)
        {
            string[] str = skillInfoStr.Split(',');
            SkillInfo info = new SkillInfo();
            info.id = int.Parse(str[0]);
            info.name = str[1];
            info.iconName = str[2];
            info.des = str[3];
            info.applyType = (ApplyType)Enum.Parse(typeof(ApplyType), str[4]);
            info.applyProperty = (ApplyProperty)Enum.Parse(typeof(ApplyProperty), str[5]);
            info.applyValue = int.Parse(str[6]);
            info.applyTime = int.Parse(str[7]);
            info.mp = int.Parse(str[8]);
            info.coldTime = int.Parse(str[9]);
            info.level = int.Parse(str[10]);
            info.releaseType = (ReleaseType)Enum.Parse(typeof(ReleaseType), str[11]);
            info.distance = float.Parse(str[12]);
            info.efxName = str[13];
            info.aniName = str[14];
            info.aniTime = float.Parse(str[15]);
            skillInfoDict.Add(info.id, info);
        }
    }
    //根据id获取信息
    public SkillInfo GetSkillInfoById(int id)
    {
        SkillInfo info = null;
        skillInfoDict.TryGetValue(id, out info);
        return info;
    }
  
}

//id  
//技能名称    
//技能图片   
//技能描述   
//作用类型   
//作用属性   
//作用值  
//作用时间  
//消耗蓝量   
//技能CD   
//使用等级    
//释放类型   
//释放距离   
//技能特效     
//动画  
//动画时长  
public class SkillInfo
{
    public int id;
    public string name;
    public string iconName;
    public string des;
    public ApplyType applyType;
    public ApplyProperty applyProperty;
    public int applyValue;
    public int applyTime;
    public int mp;
    public int coldTime;
    public int level;
    public ReleaseType releaseType;
    public float distance;
    public string efxName;
    public string aniName;
    public float aniTime;
}
//作用类型
public enum ApplyType
{
    Passive,//增益HP,MP
    Buff,//增加攻击，防御，速度，攻速等
    SingleTarget,//单个目标
    MultiTarget//多个目标
}
//作用属性
public enum ApplyProperty
{
    Attack,
    Def,
    Speed,
    AttackSpeed,
    HP,
    MP
}
//释放类型
public enum ReleaseType
{
    Self,
    Enemy,
    Position
}