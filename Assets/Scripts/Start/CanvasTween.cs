using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CanvasTween : MonoBehaviour
{
    private CanvasGroup titleContentBg;
    private float titleContentAlpha = 1.0f;//背景图变化结束值
    private float titleContentSpeed = 0.8f;
    private bool isAnyKeyDown = false;//是否有任意键被按下
    private Button pressAnyKeyBtn;
	
	void Start () {
        titleContentBg = transform.Find("content").GetComponent<CanvasGroup>();
        pressAnyKeyBtn = transform.Find("content/pressAnyKey").GetComponent<Button>();
	}
	void Update () {
        CGTween(titleContentAlpha, titleContentBg, titleContentSpeed);
        //处理任意按钮按下
        if (!isAnyKeyDown)
        {
            if (Input.anyKeyDown)
            {
                pressAnyKeyBtn.gameObject.SetActive(false);
                isAnyKeyDown = true;
                //数据本地持久化保存
                PlayerPrefs.SetFloat("DataFromSave", 0);
                //让某物体创建后不再随场景的切换而销毁，一般用于音效，联网等
                //DontDestroyOnLoad(this.gameObject);
                SceneManager.LoadScene(1);
            }
        }
	}
    //处理动画效果
    void CGTween(float changeValue, CanvasGroup canvasGroup, float speed)
    {
        if (changeValue != canvasGroup.alpha)
        {//插值运算
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, changeValue, speed * Time.deltaTime);
            if (Mathf.Abs(changeValue - canvasGroup.alpha) <= 0.01f)
            {
                canvasGroup.alpha = changeValue;
            }
        }
    }
}
