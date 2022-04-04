using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Setting : BasePanel {

    public static Setting instance;
    private Button settingColseBtn;
    private Button backgroundmusicColseBtn;
    private Button backgroundmusicOpenBtn;
    private Button soundeffectColseBtn;
    private Button soundeffectOpenBtn;
    //背景音乐
    private AudioSource musicBg;
	public override void Start () {
        instance = this;
        base.Start();
        settingColseBtn = transform.Find("settingcolseBtn").GetComponent<Button>();
        backgroundmusicColseBtn = transform.Find("content/backgroundmusicColseBtn").GetComponent<Button>();
        backgroundmusicOpenBtn = transform.Find("content/backgroundmusicOpenBtn").GetComponent<Button>();
        soundeffectColseBtn = transform.Find("content/soundeffectColseBtn").GetComponent<Button>();
        soundeffectOpenBtn = transform.Find("content/soundeffectOpenBtn").GetComponent<Button>();
        musicBg = this.GetComponent<AudioSource>();
        musicBg.loop=true;//循环播放

        settingColseBtn.onClick.AddListener(OnClicksettingColseBtn);
        backgroundmusicColseBtn.onClick.AddListener(OnClickbackgroundmusicColseBtn);
        backgroundmusicOpenBtn.onClick.AddListener(OnClickbackgroundmusicOpenBtn);
        soundeffectColseBtn.onClick.AddListener(OnClicksoundeffectColseBtn);
        soundeffectOpenBtn.onClick.AddListener(OnClicksoundeffectOpenBtn);

        backgroundmusicColseBtn.gameObject.SetActive(false);
        soundeffectColseBtn.gameObject.SetActive(false);
	}

    private void OnClicksettingColseBtn()
    {
        TransformState();
    }

    private void OnClicksoundeffectOpenBtn()
    {
        soundeffectOpenBtn.gameObject.SetActive(false);
        soundeffectColseBtn.gameObject.SetActive(true);
    }

    private void OnClicksoundeffectColseBtn()
    {
        soundeffectOpenBtn.gameObject.SetActive(true);
        soundeffectColseBtn.gameObject.SetActive(false);
    }

    private void OnClickbackgroundmusicOpenBtn()
    {//停止播放
        musicBg.Stop();
        backgroundmusicColseBtn.gameObject.SetActive(true);
        backgroundmusicOpenBtn.gameObject.SetActive(false);
       
    }

    private void OnClickbackgroundmusicColseBtn()
    {//播放
        musicBg.Play();
        backgroundmusicColseBtn.gameObject.SetActive(false);
        backgroundmusicOpenBtn.gameObject.SetActive(true);
    }	
	// Update is called once per frame
	void Update () {
		
	}

    public override void TransformState()
    {
        base.TransformState();
    }
}
