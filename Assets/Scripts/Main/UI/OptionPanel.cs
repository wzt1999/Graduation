using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionPanel : MonoBehaviour {

    public static OptionPanel instance;
    private Button equipBtn;
    private Button sellBtn;
    private Button decomposeBtn;
    private Text equipLabel;
    private int id;//保存id信息
    private bool issuccess;
    private ObjectInfo info;
	void Start () {
        instance = this;
        equipBtn = transform.Find("equip").GetComponent<Button>();
        sellBtn = transform.Find("sell").GetComponent<Button>();
        decomposeBtn = transform.Find("decompose").GetComponent<Button>();
        equipLabel = transform.Find("equip/Text").GetComponent<Text>();
        
        equipBtn.onClick.AddListener(OnClickEquipBtn);
        sellBtn.onClick.AddListener(OnClickSellBtn);
        decomposeBtn.onClick.AddListener(OnClickDecomposeBtn);
	}
    //分解
    private void OnClickDecomposeBtn()
    {
        int matid = Random.Range(3001, 3014);
        Inventory.instance.GetId(matid);
        Inventory.instance.MinusItem(id);   
        Debug.Log(matid); 
        //隐藏界面
        this.gameObject.SetActive(false);
    }

    private void OnClickSellBtn()
    {
        Inventory.instance.UpdateCoinLabel(info.priceSell);
        Inventory.instance.MinusItem(id);
        //隐藏界面
        this.gameObject.SetActive(false);
    }

    private void OnClickEquipBtn()
    {
        if (info.objectType==ObjectType.Equip)
        {
            issuccess=GameObject.Find("Canvas/Player/equipment").GetComponent<EquipmentUI>().Dress(id);
            if (issuccess)
            {
                Inventory.instance.MinusItem(id);
               // JudgeItemNum();
            }
        }
        else
        {
           // GameObject.Find("Canvas/packsack/compound").GetComponent<Compound>().IsCompound(id);

            int num = Random.Range(2001, 2037);
            Inventory.instance.GetId(num);
            Inventory.instance.MinusItem(id);
        }        
        //隐藏界面
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (info!=null)
        {
            if (info.objectType == ObjectType.Equip)
            {
                equipLabel.text = "装备";
                decomposeBtn.gameObject.SetActive(true);
            }
            else
            {
                equipLabel.text = "合成";
                decomposeBtn.gameObject.SetActive(false);
            }
        }
       
        //transform.position = Input.mousePosition + new Vector3(75, 75, 0);
	}
    //点击后显示界面
    public void ShowOptionPanel(int id)
    { 
        this.id = id;
        info = ObjectsInfo.instance.GetObjectInfoById(id);
        if (info.objectType!=ObjectType.Drug)
        {
            this.gameObject.SetActive(true);
            transform.position = Input.mousePosition + new Vector3(70, -25, 0);
        }
    }
    public bool IsSuccessEquip()
    {
        return issuccess;
    }
}
