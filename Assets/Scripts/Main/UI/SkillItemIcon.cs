using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SkillItemIcon : MonoBehaviour ,IPointerDownHandler,IDragHandler,IEndDragHandler,ICanvasRaycastFilter
{

    private bool isRaycastLocationVaild = true;//默认是射线不能穿透物品
    private GameObject cloneIcon;
    private Transform canvasTrans;
    private int skillId;
	void Start () {
        canvasTrans = GameObject.Find("Canvas").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        skillId = transform.parent.GetComponent<SkillItem>().id;
        cloneIcon = GameObject.Instantiate(this.gameObject);
        cloneIcon.transform.SetParent(canvasTrans);
        cloneIcon.transform.position = Input.mousePosition;
        cloneIcon.GetComponent<Image>().raycastTarget = false;
        isRaycastLocationVaild = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        cloneIcon.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject.Destroy(cloneIcon);
        GameObject go = eventData.pointerCurrentRaycast.gameObject;
        if (go != null)
        {
            if (go.tag.Equals(Tags.shortcutslot))//放下的位置是快捷栏
            {
                go.GetComponent<ShortCutSlot>().SetSkillInfo(skillId);
            }
            else if (go.tag.Equals(Tags.shortcuticon))//放下的位置已经有技能，替换
            {
                go.transform.parent.GetComponent<ShortCutSlot>().SetSkillInfo(skillId);
            }
        }
        isRaycastLocationVaild = true;
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return isRaycastLocationVaild;
    }
}
