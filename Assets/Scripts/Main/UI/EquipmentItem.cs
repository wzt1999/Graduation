using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class EquipmentItem : MonoBehaviour ,IPointerClickHandler
{

    private Image icon;
    public int id;
    private PlayerStatus playerStatus;
	void Awake () {
        icon = this.GetComponent<Image>();
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
	}
	
	void Update () {
		
	}
   public void SetInfo(ObjectInfo info)
    {
        this.id = info.id;
        // icon.sprite = Resources.Load<Sprite>("Icon/" + info.iconName);
       icon.sprite = Resources.Load("Icon/" + info.iconName,typeof(Sprite)) as Sprite;
    }

   public void OnPointerClick(PointerEventData eventData)
   {
       GameObject.Find("Canvas/Player/equipment").GetComponent<EquipmentUI>().TakeOffEquip(id, gameObject);
   }
}
