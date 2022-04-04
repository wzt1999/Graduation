using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour {
    //用来储存角色预制体
    public GameObject[] characterPrefabs;
    //用来存储实例化后的角色物体
    private GameObject[] characterGameobjects;

    private Button prevBtn;
    private Button nextBtn;
    private int selectedIndex;//选择角色的索引

    private CanvasGroup HintUi;
    private Button closeBtn;
    private CanvasGroup RegisterUi;
    private Button okBtn;
    private InputField nameInput;
    private Image enterName;
    private RawImage title;
    private RawImage loading;
	void Start () {
        characterGameobjects = new GameObject[characterPrefabs.Length];
        for (int i = 0; i < characterGameobjects.Length; i++)
        {//将实例化后的游戏角色存储
            characterGameobjects[i] = GameObject.Instantiate(characterPrefabs[i]);
        }
        UpdateCharacterShow();
        title = GameObject.Find("Canvas/title").GetComponent<RawImage>();
        loading = GameObject.Find("Canvas/loadingBg").GetComponent<RawImage>();
        enterName = GameObject.Find("Canvas/enterName").GetComponent<Image>();
        HintUi = GameObject.Find("CanvasHint").GetComponent<CanvasGroup>();
        RegisterUi = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        prevBtn = GameObject.Find("Canvas/prevBtn").GetComponent<Button>();
        nextBtn = GameObject.Find("Canvas/nextBtn").GetComponent<Button>();
        okBtn = GameObject.Find("Canvas/enterName/okBtn").GetComponent<Button>();
        closeBtn = GameObject.Find("CanvasHint/bg/CloseBtn").GetComponent<Button>();
        nameInput = GameObject.Find("Canvas/enterName/InputField").GetComponent<InputField>();

        loading.gameObject.SetActive(false);
        prevBtn.onClick.AddListener(OnClickPrevBtn);
        nextBtn.onClick.AddListener(OnClickNextBtn);
        okBtn.onClick.AddListener(OnClickOkBtn);
        closeBtn.onClick.AddListener(OnClickCloseBtn);
        
	}

    private void OnClickCloseBtn()
    {
        HintUi.alpha = 0;
        RegisterUi.alpha = 1;
    }

    private void OnClickOkBtn()
    {
        loading.gameObject.SetActive(true);
        if (nameInput.text=="")
        {
            HintUi.alpha = 1;
            RegisterUi.alpha = 0;
            return;
        }
        //储存选择的角色索引
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedIndex);
        //存储输入的角色昵称
        PlayerPrefs.SetString("name", nameInput.text);
        enterName.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(false);
        prevBtn.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        //同步加载场景
        //SceneManager.LoadScene(2);
        //异步加载场景
        AsyncLoadScene.instance.StartAsync();
    }

    private void OnClickNextBtn()
    {
        selectedIndex++;
        //处理越界的情况
        selectedIndex %= characterPrefabs.Length;
        UpdateCharacterShow();
    }

    private void OnClickPrevBtn()
    {
        selectedIndex--;
        if (selectedIndex<=-1)
        {
            selectedIndex = characterPrefabs.Length - 1;
        }
        UpdateCharacterShow();
        
    }
    //更新角色显示方法
	void UpdateCharacterShow()
    {
        characterGameobjects[selectedIndex].SetActive(true);
        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            if (i!=selectedIndex)
            {
                //把为选择的角色设置为隐藏
                characterGameobjects[i].SetActive(false);
            }
        }
    }

}
