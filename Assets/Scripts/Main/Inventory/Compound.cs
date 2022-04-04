using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Compound : MonoBehaviour
{

    private Button compoundBtn;
	void Start () {
        compoundBtn = transform.Find("compoundBtn").GetComponent<Button>();

        compoundBtn.onClick.AddListener(OnClickCompoundBtn);
        this.gameObject.SetActive(false);
	}
    //合成
    private void OnClickCompoundBtn()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void IsCompound(int id)
    {
        this.gameObject.SetActive(true);
    }

}
