using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskNPC : MonoBehaviour
{
	 void Start () {
         
	}
	void Update () {
		
	}
    //开始触摸
    void  OnTriggerEnter(Collider col)
    {
        //弹出是否对话
        //是，对话结束，任务面板
        //否，取消弹窗
        if (col.tag==Tags.player)
        {
            TalkwithNpc.instance.TransformState();
        }
      
    }
    //结束触摸
    void OnTriggerExit(Collider col)
    {
        //结束是否对话
        if (col.tag == Tags.player)
        {
            TalkwithNpc.instance.TransformState();
        }
    }
    
   
}
