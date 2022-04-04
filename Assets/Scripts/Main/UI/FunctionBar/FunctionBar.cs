using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FunctionBar : MonoBehaviour {

    private Button packsackBtn;
    private Button roleBtn;
    private Button SettingBtn;
    private Button TaskBtn;
    private Button transcactionBtn;
    
	void Start () {
        packsackBtn = transform.Find("packsackBtn").GetComponent<Button>();
        roleBtn = transform.Find("roleBtn").GetComponent<Button>();
        SettingBtn = transform.Find("SettingBtn").GetComponent<Button>();
        TaskBtn = transform.Find("TaskBtn").GetComponent<Button>();
        transcactionBtn = transform.Find("transcactionBtn").GetComponent<Button>();

        packsackBtn.onClick.AddListener(OnClickPacksackBtn);
        roleBtn.onClick.AddListener(OnClickRoleBtn);
        SettingBtn.onClick.AddListener(OnClickSettingBtn);
        TaskBtn.onClick.AddListener(OnClickTaskBtn);
        transcactionBtn.onClick.AddListener(OnClickTrancationBtn);
	}

    private void OnClickTrancationBtn()
    {
        ShopPanel.instance.TransformState();
    }

    private void OnClickTaskBtn()
    {
        Taskmanger.instance.TransformState();
    }

    private void OnClickSettingBtn()
    {
        Setting.instance.TransformState();
    }

    private void OnClickRoleBtn()
    {
        PlayerUi.instance.TransformState();
    }

    private void OnClickPacksackBtn()
    {
        Inventory.instance.TransformState();
    }
	
}
