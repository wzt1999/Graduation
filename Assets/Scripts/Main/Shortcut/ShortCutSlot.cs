using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//快捷栏药品的类型
public enum DrugType
{
    MP,
    HP,
    None
}
//快捷栏的类型
public enum ShortCutType
{
    Drug,
    Skill,
    None
}
public class ShortCutSlot : MonoBehaviour {

    public KeyCode keyCode;
    private Image icon;
    private DrugType drugType = DrugType.None;
    private ShortCutType shortCutType = ShortCutType.None;
    private PlayerStatus playerStatus;
    private SkillInfo skillInfo;
    public int id;
    private ObjectInfo objectInfo;
    private ShortCut shortCut;
    private Image cdMask;
    private bool isColding;
    private float timeCD;
    private PlayerAttack playerAttack;

	void Start () {
        icon = transform.Find("durgiocn").GetComponent<Image>();
        icon.gameObject.SetActive(false);
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        playerAttack = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAttack>();
        shortCut = transform.parent.GetComponent<ShortCut>();
        cdMask = transform.Find("mask").GetComponent<Image>();
        cdMask.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (playerAttack.playerState == PlayerState.Death)
        {
            return;
        }
        if (Input.GetKeyDown(keyCode))
        {
            if (shortCutType == ShortCutType.Drug)
            {
                OnDrugUse();
            }
            else if (shortCutType == ShortCutType.Skill)
            {
                OnSkillUse();
            }
        }
        if (isColding)
        {
           
            timeCD += Time.deltaTime;
            cdMask.fillAmount = (skillInfo.coldTime - timeCD) / skillInfo.coldTime;
            if (timeCD >= skillInfo.coldTime)
            {
                isColding = false;
                timeCD = 0;
                cdMask.gameObject.SetActive(false);
            }
        }
	}
    //将技能应用到快捷栏上
    public void SetSkillInfo(int id)
    {
        icon.gameObject.SetActive(true);
        skillInfo = SkillsInfo.instance.GetSkillInfoById(id);
        icon.sprite = Resources.Load<Sprite>("Icon/Skill/" + skillInfo.iconName);
        shortCutType = ShortCutType.Skill; 
    }
    public void SetDrugInfo(int id)
    {
        if (shortCut.JudgeShortCutSlotID(id))
        {
            this.id = id;
            icon.gameObject.SetActive(true);
            objectInfo = ObjectsInfo.instance.GetObjectInfoById(id);
            icon.sprite = Resources.Load<Sprite>("Icon/" + objectInfo.iconName);
            shortCutType = ShortCutType.Drug;
            if (objectInfo.hp > 0&& 0 == objectInfo.mp)
            {
                drugType = DrugType.HP;
            }
            else if (objectInfo.mp > 0&& 0 == objectInfo.hp)
            {
                drugType = DrugType.MP;
            }
            else
            {
                drugType = DrugType.None;
            }
        }
        else
        {
            Debug.Log("快捷栏已存在该物品");
        }
    }
    //快捷使用药品
    void OnDrugUse()
    {
        bool isChangeState = playerStatus.GetHpOrMpState(drugType);
        if (isChangeState)
        {
            int num = Inventory.instance.MinusItem(id);//背包已经扣除数量
            if (num > 0)
            {
                playerStatus.GetDrug(objectInfo.hp, objectInfo.mp);
            }
            else if (num == 0)//不仅需要加血还需要清除快捷方式
            {
                playerStatus.GetDrug(objectInfo.hp, objectInfo.mp);
          
                drugType = DrugType.None;
                id = 0;
                objectInfo = null;
                icon.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("当前血量或者蓝量是满的");
        }
    }
    //使用技能的方法
    void OnSkillUse()
    {
        if (!isColding)//未在CD中，可以进行施法
        {
            if (skillInfo.applyType == ApplyType.SingleTarget || skillInfo.applyType == ApplyType.MultiTarget)//单体目标技能
            {
                
                if (skillInfo.mp <= playerStatus.remainMP)//在释放技能前对剩余的蓝量进行判断
                {
                    playerAttack.playerState = PlayerState.SkillAttack;//设置为技能攻击状态
                    //TODO 设置技能信息
                    playerAttack.ReceiveSkillInfo(skillInfo, this);
                }
                else
                {
                    Debug.Log("MP不足");
                }
                return;
            }
            OnUseSkillState(skillInfo);
        }
    }
    //处理技能的状态
    public bool OnUseSkillState(SkillInfo skillInfo)
    {
        bool isSuccess = playerStatus.TakeMP(skillInfo.mp);//判断蓝量是否足够
        //释放技能
        if (isSuccess)
        {
            isColding = true;
            cdMask.gameObject.SetActive(true);
            cdMask.fillAmount = 1;
            //TODO 主角释放技能（增益，增强）
            if (skillInfo.applyType == ApplyType.Buff || skillInfo.applyType == ApplyType.Passive)
            {
                playerAttack.UseSkill(skillInfo);
            }
        }
        else
        {
            Debug.Log("MP不足");
        }
        return isSuccess;
    }
}
