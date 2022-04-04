using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopPanel : BasePanel {

    public static ShopPanel instance;
    //返回按钮
    private Button returnBtn;
    //药品界面按钮
    private Button medicinalBtn;
    private Transform durg;
    //装备界面按钮
    private Button equipBtn;
    private Transform equip;

   
	public override void Start () {
        instance = this;
        base.Start();
        returnBtn = transform.Find("returnBtn").GetComponent<Button>();
        medicinalBtn = transform.Find("medicinalBtn").GetComponent<Button>();
        equipBtn = transform.Find("equipBtn").GetComponent<Button>();
        durg = transform.Find("ItemPanel/Durg");
        equip = transform.Find("ItemPanel/Equip");
        equip.gameObject.SetActive(false);

        returnBtn.onClick.AddListener(OnClickReturnBtn);
        medicinalBtn.onClick.AddListener(OnClickMedicinalBtn);
        equipBtn.onClick.AddListener(OnClickeEuipBtn);
	}

    private void OnClickeEuipBtn()
    {
        durg.gameObject.SetActive(false);
        equip.gameObject.SetActive(true);
        medicinalBtn.GetComponent<Image>().color = new Color(100/255f, 100/255f, 100/255f, 255f);
        equipBtn.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
    }

    private void OnClickMedicinalBtn()
    {
        equipBtn.GetComponent<Image>().color = new Color(100 / 255f, 100 / 255f, 100 / 255f, 255f);
        medicinalBtn.GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
        durg.gameObject.SetActive(true);
        equip.gameObject.SetActive(false);
    }

    private void OnClickReturnBtn()
    {
        ShopPanel.instance.TransformState();
    }
	
	
	void Update () {
		
	}
    public override void TransformState()
    {
        base.TransformState();
    }
}
