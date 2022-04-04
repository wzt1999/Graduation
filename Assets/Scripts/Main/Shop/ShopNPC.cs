using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : NPC {

	void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShopPanel.instance.TransformState();
        }
    }
	void Start () {
		
	}
	
	
	void Update () {
		
	}
}
