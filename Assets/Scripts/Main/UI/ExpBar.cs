using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpBar : MonoBehaviour {
    
    public static ExpBar instance;
    private Image fillExp;
    private Text expLabel;
	void Start () {
        instance = this;
        fillExp = transform.Find("expfill").GetComponent<Image>();
        expLabel = transform.Find("Text").GetComponent<Text>();
        SetExpValue(0);
	}
	
	void Update () {
		
	}
    //设置经验值
    public void SetExpValue(float expValue)
    {
        fillExp.fillAmount = expValue;
        //f1一位小数
        expLabel.text = (expValue * 100).ToString("f1") + "%";
    }
}
