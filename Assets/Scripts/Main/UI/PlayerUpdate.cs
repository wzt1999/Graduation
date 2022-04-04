using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUpdate : MonoBehaviour {

    private Button attributeBtn;
    private Button colseBtn;
    private Button reloadingBtn;
    private PlayerStatus playerStatus;
    private Text nameLabel;
    private Text lvLabel;
    private Text expLabel;
    private Text attackLabel;
    private Text defLabel;
    private Text speedLabel;
	void Start () {
        attributeBtn = transform.Find("attributeBtn").GetComponent<Button>();
        colseBtn = transform.Find("colseBtn").GetComponent<Button>();
        reloadingBtn = transform.Find("reloadingBtn").GetComponent<Button>();
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        nameLabel = transform.Find("name").GetComponent<Text>();
        lvLabel = transform.Find("lv").GetComponent<Text>();
        expLabel = transform.Find("exp").GetComponent<Text>();
        attackLabel = transform.Find("attack").GetComponent<Text>();
        defLabel = transform.Find("def").GetComponent<Text>();
        speedLabel = transform.Find("speed").GetComponent<Text>();

        attributeBtn.onClick.AddListener(OnClickAttributeBtn);
        colseBtn.onClick.AddListener(OnClickColseBtn);
        reloadingBtn.onClick.AddListener(OnClickReloadingBtn);
	}
    //换装
    private void OnClickReloadingBtn()
    {
        PlayerReloading.instance.SetReloading();
    }

    private void OnClickColseBtn()
    {
        PlayerUi.instance.TransformState();
    }

    private void OnClickAttributeBtn()
    {
        Status.instance.TransformState();
        Status.instance.UpdateShow();
    }
	
	
	void Update () {
        UpdateShowPlayer();
	}
    void UpdateShowPlayer()
    {
        nameLabel.text = "玩家名字："+playerStatus.name;
        lvLabel.text = "Lv."+playerStatus.level;
        expLabel.text = "经验值： " + playerStatus.exp;
        attackLabel.text = "攻击力：" + playerStatus.attack+"+"+playerStatus.attackEquip+"+"+playerStatus.attackPlus;
        defLabel.text = "防御力：" + playerStatus.def+"+"+playerStatus.defEquip+"+"+playerStatus.defPlus;
        speedLabel.text = "速度：" + playerStatus.speed+"+"+playerStatus.speedEquip+"+"+playerStatus.speedPlus;
    }
}
