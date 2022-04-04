using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopWeaponItem : MonoBehaviour {

    private int id;
    private Image icon;
    private Text nameLabel;
    private Text effectLabel;
    private Text sellLabel;
    private Button buyBtn;
	void Awake () {
        icon = transform.Find("icon").GetComponent<Image>();
        nameLabel = transform.Find("name").GetComponent<Text>();
        effectLabel = transform.Find("des").GetComponent<Text>();
        sellLabel = transform.Find("sell").GetComponent<Text>();
        buyBtn = transform.Find("buy").GetComponent<Button>();

        buyBtn.onClick.AddListener(OnClickBuyBtn);
	}

    private void OnClickBuyBtn()
    {
        ShopWeaponUI.instance.OnBuy(id);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
     //加载武器列表
    public void SetWeaponListInfo(int id)
    {
        this.id = id;
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(id);
        icon.sprite = Resources.Load<Sprite>("Icon/" + info.iconName);
        nameLabel.text = "名称：" + info.name;
        sellLabel.text = "售价：" + info.priceBuy;
        if (info.attack>0)
        {
            effectLabel.text = "效果：+攻击力：" + info.attack;
        }
        else if (info.def>0)
        {
           effectLabel.text = "效果：+防御力：" + info.def;
        }
        else if (info.speed > 0)
        {
            effectLabel.text = "效果：+速度：" + info.speed;
        }
    }

}
