using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class hint : BasePanel {

    public static hint instance;
    private Button closeBtn;
	public override void Start () {
        instance = this;
        base.Start();
        closeBtn = transform.Find("colseBtn").GetComponent<Button>();

        closeBtn.onClick.AddListener(OnClickcloseBtn);
	}

    private void OnClickcloseBtn()
    {
        TransformState();
    }
	
	
	void Update () {
		
	}
    public override void TransformState()
    {
        base.TransformState();
    }
}
