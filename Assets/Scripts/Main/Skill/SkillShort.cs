using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillShort : MonoBehaviour {

    public static SkillShort instance;
    private Button normalBtn;
    private Button skillWBtn;
    private Button skillEBtn;
    private Button skillRBtn;
    private Button skillQBtn;
    //private PlayerAttack playerattack;
	void Awake () {
        instance = this;
        normalBtn = transform.Find("normal/attackBtn").GetComponent<Button>();
        skillWBtn = transform.Find("W/attackBtn").GetComponent<Button>();
        skillEBtn = transform.Find("E/attackBtn").GetComponent<Button>();
        skillRBtn = transform.Find("R/attackBtn").GetComponent<Button>();
        skillQBtn = transform.Find("Q/attackBtn").GetComponent<Button>();


        normalBtn.onClick.AddListener(OnClickNormalBtn);
        skillWBtn.onClick.AddListener(OnClickSkillWBtn);
        skillEBtn.onClick.AddListener(OnClickSkillEBtn);
        skillRBtn.onClick.AddListener(OnClickSkillRBtn);
        skillQBtn.onClick.AddListener(OnClickSkillQBtn);

	}

    private void OnClickSkillQBtn()
    {
        
    }

    private void OnClickSkillRBtn()
    {
       
    }

    private void OnClickSkillEBtn()
    {
        
    }

    private void OnClickSkillWBtn()
    {
        
    }

    private void OnClickNormalBtn()
    {
        
    }
	
	
	void Update () {
		
	}
}
