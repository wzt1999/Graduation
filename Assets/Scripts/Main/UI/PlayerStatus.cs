using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//主角的状态信息
public class PlayerStatus : MonoBehaviour {

    public int hp = 100;
    public float remainHP = 30;
    public int mp = 100;
    public float remainMP = 50;
    public int level = 1;
    public float exp = 0;
    //升级需要的经验
    public float needexp;
    public float attack = 20;
    public float attackPlus = 0;
    public float attackEquip = 0;
    public float def = 20;
    public float defPlus = 0;
    public float defEquip = 0;
    public float speed = 20;
    public float speedPlus = 0;
    public float speedEquip = 0;
    public int remainPoint = 0;//剩余点数
    private GameObject go;
	void Start () {
	}
	

	void Update () {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GetExp(200);
        }
	}
    //判断点数是否足够
    public bool GetPoint(int point=1)
    {
        if (remainPoint>=point)
        {
            remainPoint -= point;
            return true;
        }
        return false;
    }
    //获取人物状态信息
    public bool GetHpOrMpState(DrugType type)
    {
        if (type == DrugType.HP)
        {
            if (remainHP < this.hp)
            {
                return true;
            }
        }
        else if (type == DrugType.MP)
        {
            if (remainMP < this.mp)
            {
                return true;
            }
        }
        return false;
    }
    //治疗
    public void GetDrug(int addHP, int addMP)
    {
        remainHP += addHP;
        remainMP += addMP;
        if (remainHP >= this.hp)
        {
            remainHP = hp;
        }
        if (remainMP >= this.mp)
        {
            remainMP = mp;
        }
        TopBar.instance.UpdateHeadShow();//更新显示
    }
    //升级
   public void GetExp(int exp)
    {
        this.exp += exp;
        //当前这一级升下一级所需要的经验
        needexp = level * 20 + 100;
        while (this.exp>=needexp)
        {
            this.level++;
            this.exp -= needexp;
            needexp = level * 20 + 100;
            //加点
            remainPoint++;
            //血量恢复,并且血量加多 hp=hp+level*30；
            this.hp += this.level * 30;
            this.remainHP = this.hp;
            //蓝量加满mp=mp+level*10;
            this.mp += this.level * 10;
            this.remainMP = this.mp;
            //特效
          go= GameObject.Instantiate(Resources.Load<GameObject>("Effect/LvUp"), transform.position, Quaternion.identity);
          
        }
        ExpBar.instance.SetExpValue(this.exp / needexp);
        TopBar.instance.UpdateHeadShow();
    }

   //判断蓝量是否足够
   public bool TakeMP(int makeMp)
   {
       if (remainMP >= makeMp)
       {
           remainMP -= makeMp;
           TopBar.instance.UpdateHeadShow();
           return true;
       }
       else
       {
           return false;
       }
   }
  
}
