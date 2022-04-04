using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMessage : MonoBehaviour {

    public static TaskMessage instance;
    //获取配置表
    private TextAsset taskmessageList;
    private Dictionary<int, TaskInformation> taskDic = new Dictionary<int, TaskInformation>();
	void Awake () {
        instance = this;
        ReadMessage();
	}
	
	void ReadMessage()
    {
        taskmessageList = Resources.Load<TextAsset>("text/Taskinformation");
        string text = taskmessageList.text;
        string[] strArry = text.Split('\n');
        foreach (string  str in strArry)
        {
            //得到每一个具体的数据
            string[] proArray = str.Split(',');
            TaskInformation info = new TaskInformation();
            info.id = int.Parse(proArray[0]);
            info.taskname = proArray[1];
            info.taskmessage = proArray[2];
            info.tasktype = (TaskType)System.Enum.Parse(typeof(TaskType), proArray[3]);
            if (info.tasktype==TaskType.Combat)
            {
                info.require = int.Parse(proArray[4]);
            }
            info.award = int.Parse(proArray[5]);

            taskDic.Add(info.id,info);
        }
    }
    //给外部使用的接口，根据id查询信息
    public TaskInformation GetTaskmessageById(int id)
    {
        TaskInformation taskinfo = null;
        taskDic.TryGetValue(id, out taskinfo);
        return taskinfo;
    }
}
public enum TaskType
{
    Combat,
    Chat,
    Buy
}
//id   
//任务名字	
//任务内容         
//任务类型（打斗，交流，购买）  
//任务要求  
//任务奖励  金币
public class TaskInformation
{
    public int id;
    public string taskname;
    public string taskmessage;
    public TaskType tasktype;
    public int require;
    public int award;
}
