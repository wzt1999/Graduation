using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopWeaponUI : MonoBehaviour {
    public static ShopWeaponUI instance;
    private Transform content;
    private Transform buyContainer;
    private InputField numberInput;
    private Button confirmBtn;
    private int buyId;
	void Start () {
        instance = this;
        content = transform.Find("Scroll View/Viewport/Content");
        buyContainer = transform.Find("buyContainer");
        numberInput = buyContainer.Find("InputField").GetComponent<InputField>();
        confirmBtn = buyContainer.Find("confirmBtn").GetComponent<Button>();

        confirmBtn.onClick.AddListener(OnClickConfirmBtn);
        buyContainer.gameObject.SetActive(false);
        InitWeaponShop();
	}

    private void OnClickConfirmBtn()
    {
        if (numberInput!=null)
        {
            int count = int.Parse(numberInput.text);
            if (count>0)
            {
                int price = ObjectsInfo.instance.GetObjectInfoById(buyId).priceBuy;
                int totalprice = count * price;
                bool isSuccess = Inventory.instance.GetCoin(totalprice);
                if (isSuccess)
                {
                    Inventory.instance.GetId(buyId, count);
                }
            }
        }
        buyId = 0;
        numberInput.text = "";
        buyContainer.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //初始化装备武器商店
    void InitWeaponShop()
    {
        string weaponID = (Resources.Load<TextAsset>("Text/ShopWeaponIDList")).text;
        string[] weaponIdList = weaponID.Split(',');
        foreach (string id in weaponIdList)
        {
            GameObject item = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/WeaponShopItem"));
            item.transform.SetParent(content);
            item.transform.localPosition = Vector3.zero;
            item.GetComponent<ShopWeaponItem>().SetWeaponListInfo(int.Parse(id));
        }
    }
    //购买函数
    public void OnBuy(int id)
    {
        buyId = id;
        buyContainer.gameObject.SetActive(true);
    }
}
