using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TopBar : MonoBehaviour {

    public static TopBar instance;
    private Button headportraitBtn;
    //名字
    private Text nameLabel;
    private PlayerStatus playerStatus;
    //血条
    private Text hpLabel;
    private Image hpImage;
    //蓝条
    private Text mpLabel;
    private Image mpImage;
    //经验条
    private Text expLabel;
    private Image expImage;

	void Start () {
        instance = this;
        headportraitBtn = transform.Find("headportrait").GetComponent<Button>();
        nameLabel = transform.Find("headportrait/name").GetComponent<Text>();
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        hpLabel = transform.Find("headportrait/lifebarbg/Text").GetComponent<Text>();
        hpImage = transform.Find("headportrait/lifebarbg/lifebar").GetComponent<Image>();
        mpLabel = transform.Find("headportrait/bluebarbg/Text").GetComponent<Text>();
        mpImage = transform.Find("headportrait/bluebarbg/bluebar").GetComponent<Image>();
        expLabel = transform.Find("headportrait/expbg/Text").GetComponent<Text>();
        expImage = transform.Find("headportrait/expbg/expfill").GetComponent<Image>();

        UpdateHeadShow();

        headportraitBtn.onClick.AddListener(OnClickheadportraitBtn);
	}

    private void OnClickheadportraitBtn()
    {
        //Status.instance.TransformState();
        //Status.instance.UpdateShow();
        SkillUI.instance.TransformState();
    }
	
	
	void Update () {
		
	}
    //更新头像框的显示
   public void UpdateHeadShow()
    {
        if (playerStatus.remainHP<=0)
        {
            playerStatus.remainHP = 0;
        }
        nameLabel.text = "Lv." + playerStatus.level + "  " + playerStatus.name;
        hpImage.fillAmount = playerStatus.remainHP / playerStatus.hp;
        mpImage.fillAmount = playerStatus.remainMP / playerStatus.mp;
        hpLabel.text = ((playerStatus.remainHP / playerStatus.hp) * 100).ToString("f1") + "%";
        mpLabel.text = ((playerStatus.remainMP / playerStatus.mp) * 100).ToString("f1") + "%";
    }
}
