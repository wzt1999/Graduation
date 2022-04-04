using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUi : BasePanel
{

    public static PlayerUi instance;
	public override void Start () {
        base.Start();
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void TransformState()
    {
        base.TransformState();
    }
}
