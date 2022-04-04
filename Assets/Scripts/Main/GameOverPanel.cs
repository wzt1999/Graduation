using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverPanel : BasePanel {

    public static GameOverPanel instance;
    private Button quitGameBtn;
    private Button keepGameBtn;
	public override void Start () {
        base.Start();
        instance = this;
        quitGameBtn = transform.Find("quitGame").GetComponent<Button>();
        keepGameBtn = transform.Find("keepGame").GetComponent<Button>();
        quitGameBtn.onClick.AddListener(OnClickQuitGameBtn);
        keepGameBtn.onClick.AddListener(OnClickKeepGameBtn);

	}

    private void OnClickKeepGameBtn()
    {
        this.TransformState();
    }

    private void OnClickQuitGameBtn()
    {
        Application.Quit();
    }
	
	
	void Update () {
		
	}
    public override void TransformState()
    {
        base.TransformState();
    }
}
