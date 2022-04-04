using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillItem : MonoBehaviour {
    private Image icon;
    private Text nameLabel;
    private Text desLabel;
    private Text applyTypeLabel;
    private Text mpLabel;
    private Image mask;
    public int id;
    private SkillInfo info;

	void Awake () {
        icon = transform.Find("icon").GetComponent<Image>();
        nameLabel = transform.Find("name").GetComponent<Text>();
        desLabel = transform.Find("des").GetComponent<Text>();
        applyTypeLabel = transform.Find("applyType").GetComponent<Text>();
        mpLabel = transform.Find("mp").GetComponent<Text>();
        mask = transform.Find("mask").GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //更新技能显示
    public void SetSkillItemInfo(int id)
    {
        this.id = id; 
       // SkillInfo info = null;
       // info=SkillsInfo.instance.GetSkillInfoById(id);
       info = SkillsInfo.instance.GetSkillInfoById(id);
        
        icon.sprite = Resources.Load<Sprite>("Icon/Skill/" + info.iconName);
        nameLabel.text = info.name;
        switch (info.applyType)
        {
            case ApplyType.Passive:
                applyTypeLabel.text = "增益";
                break;
            case ApplyType.Buff:
                applyTypeLabel.text = "增强";
                break;
            case ApplyType.SingleTarget:
                applyTypeLabel.text = "单个目标";
                break;
            case ApplyType.MultiTarget:
                applyTypeLabel.text = "多个目标";
                break;
        }
        desLabel.text = info.des;
        mpLabel.text = info.mp + "MP";
    }
    //更新技能遮罩图片
    public void UpdateShow(int level)
    {
        if (info.level <= level)
        {
            mask.gameObject.SetActive(false);
        }
        else
        {
            mask.gameObject.SetActive(true);
        }
    }
}
