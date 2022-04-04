using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyCoin : MonoBehaviour {

    private int topcoinNumber;
    private Text coinNumberLabel;
    //购买建
    private Button addcoinBtn;
	void Awake () {
        coinNumberLabel = transform.Find("Text").GetComponent<Text>();
        addcoinBtn = transform.Find("addBtn").GetComponent<Button>();

        addcoinBtn.onClick.AddListener(OnClickAddcionBtn);
	}

    private void OnClickAddcionBtn()
    {
        hint.instance.TransformState();
    }
	
	
	void Update () {
        topcoinNumber = Inventory.instance.GetCoinTopNumber();
        coinNumberLabel.text = topcoinNumber.ToString();
	}
}
