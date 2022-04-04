using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferPlayer1 : MonoBehaviour {

    private Transform playerstatus;
    private PlayerMove playerMove;

    private Transform playerPos1;
    public bool isTransfer1 = true;
	void Start () {
        playerPos1 = GameObject.Find("playerPos1").transform;
        playerstatus = GameObject.FindGameObjectWithTag(Tags.player).transform;
        playerMove = playerstatus.GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //开始触发
    void OnTriggerEnter(Collider col)
    {
        if (isTransfer1)
        {
            playerMove.SimpleMove(playerstatus.position);
            playerstatus.position = playerPos1.position;
            GameObject.Find("transferEffect2").GetComponent<TransferPlayer>().isTransfer = false;
        }
    }
    //结束触发
    void OnTriggerExit(Collider col)
    {
       
            isTransfer1=true;
    }
}
