using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Taskcontent : MonoBehaviour
{
    private Text taskcontent;
    public Text showProgress;
    //杀怪的数量
    public int killCount = 0;
    //需要的要求
    private int require = 0;
    private int id;
    private int awardcoin;
	void Awake () {
        taskcontent = this.GetComponent<Text>();//内容 
        showProgress = transform.Find("Text").GetComponent<Text>();
        showProgress.gameObject.SetActive(false);
	}
    public void SetTaskId(int id)
    {
        this.id = id;
        TaskInformation taskinfo = TaskMessage.instance.GetTaskmessageById(id);
        taskcontent.text = GetTaskContent(taskinfo);
        
        require = taskinfo.require;//获取任务是否完成的要求
    }
    //显示任务的进度
   public void ShowTaskProgress()
    {
        showProgress.text= "完成：" + killCount;
    }
    //任务内容信息拼接
    string GetTaskContent(TaskInformation taskinfo)
    {
        string str = "";
        str += "任务名称：" + taskinfo.taskname + "\n";
        str += "任务内容：" + taskinfo.taskmessage + "\n";
        str += "任务奖励：" + taskinfo.award + "金币\n";
        awardcoin = taskinfo.award;
        str += "要求：" + taskinfo.require;
        return str;
    } 
   public bool FinishTask()
    {
        if (killCount >= require)
        {
            killCount = 0;
           // ShowTaskDes();f
            Inventory.instance.UpdateCoinLabel(awardcoin);
            return true;
        }
        else
        {
            Debug.Log("请完成要求！");
            return false;
        }
    }
    void Update()
   {
       if (Input.GetKeyDown(KeyCode.H))
       {
           killCount += 4;
       }
   }
    //打怪
    public void OnKillEnemy()
    {
        killCount++;
    }
}
