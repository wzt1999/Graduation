using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferPlayer : MonoBehaviour {

    private Transform playerstatus;
    private PlayerMove playerMove;
 
    private Transform playerPos;
    public bool isTransfer=true;
	void Start () {
        playerPos = GameObject.Find("playerPos").transform;
        playerstatus = GameObject.FindGameObjectWithTag(Tags.player).transform;
        playerMove = playerstatus.GetComponent<PlayerMove>();
	}
	
	
	void Update () {
		
	}
    //开始触发
    void OnTriggerEnter(Collider col)
    {
        if (isTransfer)
        {
            //TO DO人物静止,目标位置为人物位置
            playerMove.SimpleMove(playerstatus.position);

            playerstatus.position = playerPos.position;
            GameObject.Find("transferEffect1").GetComponent<TransferPlayer1>().isTransfer1 = false;

        }
    }
    //结束触发
    void OnTriggerExit(Collider col)
    {
        isTransfer = true;       
    }
}
