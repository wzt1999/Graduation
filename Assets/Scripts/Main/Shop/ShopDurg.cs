using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopDurg : MonoBehaviour {

    private int buyId;
    private Button buy1001Btn;
    private Button buy1002Btn;
    private Button buy1003Btn;
    private Button buy1004Btn;
	void Start () {
        buy1001Btn = transform.Find("DurgItem1/buy").GetComponent<Button>();
        buy1002Btn = transform.Find("DurgItem2/buy").GetComponent<Button>();
        buy1003Btn = transform.Find("DurgItem3/buy").GetComponent<Button>();
        buy1004Btn = transform.Find("DurgItem4/buy").GetComponent<Button>();

        buy1001Btn.onClick.AddListener(OnClickBuy1001Btn);
        buy1002Btn.onClick.AddListener(OnClickBuy1002Btn);
        buy1003Btn.onClick.AddListener(OnClickBuy1003Btn);
        buy1004Btn.onClick.AddListener(OnClickBuy1004Btn);
	}

    private void OnClickBuy1004Btn()
    {
        buyId = 1004;
        BuyDurg();
    }

    private void OnClickBuy1003Btn()
    {
        buyId = 1003;
        BuyDurg();
    }

    private void OnClickBuy1002Btn()
    {
        buyId = 1002;
        BuyDurg();
    }
    private void OnClickBuy1001Btn()
    {
        buyId = 1001;
        BuyDurg();
    }
	void BuyDurg()
    {
        ObjectInfo info = ObjectsInfo.instance.GetObjectInfoById(buyId);
        //获取到物品所对应的单价
        int price = info.priceBuy;
        if (Inventory.instance.GetCoin(price))
        {
            Inventory.instance.GetId(buyId);
        }
            //不够钱
        else
        {
            Debug.Log("金币不够！请充值再来");
        }
    }
	void Update () {
		
	}
}
