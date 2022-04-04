using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TalkwithNpc : BasePanel {
    public static TalkwithNpc instance;
    private CanvasGroup talkpanel;
    private Button talkBtn;
	public override void Start () {
        base.Start();
        instance = this;
        talkpanel = GameObject.Find("Canvas/Talk").GetComponent<CanvasGroup>();
        talkpanel.alpha = 1;
        talkBtn = GameObject.Find("Canvas/Talk/talkNpc").GetComponent<Button>();

        talkBtn.onClick.AddListener(OnClicktalkBtn);
	}
    private void OnClicktalkBtn()
    {
        Taskmanger.instance.TransformState();
        talkpanel.alpha = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void TransformState()
    {
        talkpanel.alpha = 1;
        base.TransformState();
    }
}
