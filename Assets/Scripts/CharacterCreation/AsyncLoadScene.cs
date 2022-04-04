using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AsyncLoadScene : MonoBehaviour {

    public static AsyncLoadScene instance;
    private Image loadingImage;
    private Text loadingText;
    private AsyncOperation operation;//场景加载的类
    private float targetValue;
    private float sliderLoadSpeed = 1;//进度条移动的速度
	void Awake () {
        
        instance = this;
        loadingImage = GameObject.Find("Canvas/loadingBg/loadingImage").GetComponent<Image>();
        loadingText = GameObject.Find("Canvas/loadingBg/loadingText").GetComponent<Text>();
        loadingText.text = "";
        
	}
	
	// Update is called once per frame
	void Update () {
        if (operation != null)
        {
            targetValue = operation.progress;//加载进度
            if (operation.progress >= 0.9f)//operation.progress的值最大是0.9
            {
                targetValue = 1.0f;
            }
            //处理进度条
            if (loadingImage.fillAmount != targetValue)
            {
                loadingImage.fillAmount = Mathf.Lerp(loadingImage.fillAmount, targetValue, Time.deltaTime * sliderLoadSpeed);
                if (Mathf.Abs(loadingImage.fillAmount - targetValue) <= 0.01f)
                {
                    loadingImage.fillAmount = targetValue;
                }
            }
            //显示加载百分比
            loadingText.text = (int)(loadingImage.fillAmount * 100) + "%";
            //当进度条完成100%时，需要跳转场景
            if ((int)(loadingImage.fillAmount * 100) == 100)
            {
                //允许异步加载完毕后切换场景
                operation.allowSceneActivation = true;
            }
        }
	}
    //开始加载场景
    public void StartAsync()
    {
        if (SceneManager.GetActiveScene().name == "CharacterCreation")
        {
            StartCoroutine(AsyncLoading());
        }
    }
    //加载场景的协程
    IEnumerator AsyncLoading()
    {
        //异步加载场景
        operation = SceneManager.LoadSceneAsync(2);
        //阻止当加载完成后自动切换场景
        operation.allowSceneActivation = false;
        yield return operation;
    }
}
