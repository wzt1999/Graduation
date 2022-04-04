using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Status : BasePanel {

    public static Status instance;
    private Button colseBtn;
    private Text attackLabel;
    private Text defLabel;
    private Text speedLabel;
    private Text remainPointLabel;
    private Button attackPlusBtn;
    private Button defPlusBtn;
    private Button speedPlusBtn;
    private PlayerStatus playerStatus;

	public override void Start () {
        base.Start();
        instance = this;
        colseBtn = transform.Find("colseBtn").GetComponent<Button>();
        attackLabel=transform.Find("content/attack/Text").GetComponent<Text>();
        defLabel=transform.Find("content/def/Text").GetComponent<Text>();
        speedLabel=transform.Find("content/speed/Text").GetComponent<Text>();
        remainPointLabel = transform.Find("content/Text").GetComponent<Text>();
        attackPlusBtn = transform.Find("content/attack/addBtn").GetComponent<Button>();
        defPlusBtn = transform.Find("content/def/addBtn").GetComponent<Button>();
        speedPlusBtn = transform.Find("content/speed/addBtn").GetComponent<Button>();

        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();

        colseBtn.onClick.AddListener(OnClickColseBtn);
        attackPlusBtn.onClick.AddListener(OnClickAttackPlusBtn);
        defPlusBtn.onClick.AddListener(OnClickDefPlusBtn);
        speedPlusBtn.onClick.AddListener(OnClickSpeedPlusBtn);
        UpdateShow();
	}

    private void OnClickSpeedPlusBtn()
    {
        if (playerStatus.GetPoint())
        {
            playerStatus.speedPlus++;
            UpdateShow();
        }
    }

    private void OnClickAttackPlusBtn()
    {
        if (playerStatus.GetPoint())
        {
            playerStatus.attackPlus += 5 ;
            UpdateShow();
        }
    }

    private void OnClickDefPlusBtn()
    {
        if (playerStatus.GetPoint())
        {
            playerStatus.defPlus+=3;
            UpdateShow();
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
    }
    //更新显示界面 人物属性：基础属性+加点的属性点数+装备属性
    public void UpdateShow()
    {
        attackLabel.text = playerStatus.attack + " + " + playerStatus.attackEquip + " + " + playerStatus.attackPlus;
        defLabel.text = playerStatus.def + " + " + playerStatus.defEquip + " + " + playerStatus.defPlus;
        speedLabel.text = playerStatus.speed + " + " + playerStatus.speedEquip + " + " + playerStatus.speedPlus;
        remainPointLabel.text = "剩余的点数:" + playerStatus.remainPoint;
       
        if (playerStatus.remainPoint > 0)
        {
            attackPlusBtn.gameObject.SetActive(true);
            defPlusBtn.gameObject.SetActive(true);
            speedPlusBtn.gameObject.SetActive(true);
        }
        else
        {
            attackPlusBtn.gameObject.SetActive(false);
            defPlusBtn.gameObject.SetActive(false);
            speedPlusBtn.gameObject.SetActive(false);
        }
    }
}
