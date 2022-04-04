using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlFunction : MonoBehaviour {

    private Button shrinkBtn;
    private Button unfoldBtn;
    private RectTransform functionBar;
    //private RectTransform shortcutSkill;
	void Start () {
        shrinkBtn = transform.Find("shrink").GetComponent<Button>();
        unfoldBtn = transform.Find("unfold").GetComponent<Button>();
        functionBar = GameObject.Find("Canvas/FunctionBar").GetComponent<RectTransform>();
       // shortcutSkill = GameObject.Find("Canvas/ShortcutSkill").GetComponent<RectTransform>();
        unfoldBtn.gameObject.SetActive(false);
        //shortcutSkill.gameObject.SetActive(false);

        shrinkBtn.onClick.AddListener(OnClickShrinkBtn);
        unfoldBtn.onClick.AddListener(OnClickUnfoldBtn);
	}

    private void OnClickUnfoldBtn()
    {
        shrinkBtn.gameObject.SetActive(true);
        unfoldBtn.gameObject.SetActive(false);
        functionBar.position = new Vector3(functionBar.position.x - 332, functionBar.position.y, functionBar.position.z);
       // shortcutSkill.gameObject.SetActive(false);
    }
    //收起来
    private void OnClickShrinkBtn()
    {      
        shrinkBtn.gameObject.SetActive(false);
        unfoldBtn.gameObject.SetActive(true);
        functionBar.position = new Vector3(functionBar.position.x+332, functionBar.position.y, functionBar.position.z);
        //shortcutSkill.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
