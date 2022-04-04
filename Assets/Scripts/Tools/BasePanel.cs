using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BasePanel : MonoBehaviour {

    private RectTransform transformPanel;
    private bool isShow = true;
    public virtual void Start () {
        transformPanel = transform as RectTransform;
        Tweener TW = transformPanel.DOLocalMove(Vector3.zero, 1);
        TW.SetAutoKill(false);
        TW.Pause();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void ShowPanel()
    {
        isShow = false;
        transformPanel.DOPlayForward();
    }
    void HidePanel()
    {
        isShow = true;
        transformPanel.DOPlayBackwards();
    }
    public virtual void TransformState()
    {
        if (isShow)
        {
            ShowPanel();
        }
        else
        {
            HidePanel();
        }
    }
   
}
