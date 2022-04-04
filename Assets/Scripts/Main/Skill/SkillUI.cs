using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillUI : BasePanel
{
    public static SkillUI instance;
    private Button colseBtn;
    private Transform content;
    private int[] swordmanSkillIDList = new int[7] { 10001, 10002, 10003, 10004,10005,10006,10007};
    private PlayerStatus playerStatus;
	public override void Start () {
        base.Start();
        instance = this;
        colseBtn = transform.Find("colseBtn").GetComponent<Button>();
        content = transform.Find("Scroll View/Viewport/Content");
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();

        int[] skillIDList = swordmanSkillIDList;
        colseBtn.onClick.AddListener(OnClickColseBtn);
        for (int i = 0; i < skillIDList.Length; i++)
        {
            GameObject item = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/skilliItem"));
            item.transform.SetParent(content);
            item.transform.localPosition = Vector3.zero;
            item.GetComponent<SkillItem>().SetSkillItemInfo(skillIDList[i]);
        }
	}
    private void OnClickColseBtn()
    {
        TransformState();
    }
	

	void Update () {
		
	}
    public override void TransformState()
    {
        base.TransformState();
        UpdateItemShow();
    }
    //更新技能是否可用
    void UpdateItemShow()
    {
        SkillItem[] items = this.GetComponentsInChildren<SkillItem>();
        foreach (SkillItem skillitem in items)
        {
            skillitem.UpdateShow(playerStatus.level);
        }
    }
}
