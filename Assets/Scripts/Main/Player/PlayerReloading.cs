using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReloading : MonoBehaviour {

    public static PlayerReloading instance;
    private Material playerreload;
    private GameObject body;
    private Texture2D clothes;
	void Start () {
        instance = this;
        body = transform.Find("Archer_Female_04").gameObject;
        playerreload = body.GetComponent<Renderer>().material;
        clothes=Resources.Load<Texture2D>("Texture/Archer_Female_Costume_01_A");
	}
	
	
	void Update () {
		
	}
    public void SetReloading()
    {
        playerreload.SetTexture("_MainTex", clothes);
    }
}
