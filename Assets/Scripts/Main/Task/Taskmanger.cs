using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Taskmanger : BasePanel {

    public static Taskmanger instance;
    private Button closeBtn;
    private Transform infinisheding;
    //自动寻路按钮
    private Button autoFindBtn;
    //完成按钮
    private Button finishTaskBtn;
    private Button abandonTaskBtn;
    private Button acceptTaskBtn;
    private int taskNumberid = 101;
    //是否在任务中
    private bool isInTask = false;

    private Taskcontent finisgcontent;
	public override void Start () {
        base.Start();
        instance = this;
        closeBtn = transform.Find("closeBtn").GetComponent<Button>();
        infinisheding = transform.Find("infinisheding");
        autoFindBtn = transform.Find("infinisheding/autoFindBtn").GetComponent<Button>();
        finishTaskBtn = transform.Find("infinisheding/finishTaskBtn").GetComponent<Button>();
        abandonTaskBtn = transform.Find("infinisheding/abandonTaskBtn").GetComponent<Button>();
        acceptTaskBtn = transform.Find("acceptTaskBtn").GetComponent<Button>();
        finisgcontent = transform.Find("taskContent").GetComponent<Taskcontent>();
        infinisheding.gameObject.SetActive(false);
        Getid(taskNumberid);

        closeBtn.onClick.AddListener(OnClickCloseBtn);
        autoFindBtn.onClick.AddListener(OnClickAutoFindBtn);
        finishTaskBtn.onClick.AddListener(OnClickFinishTaskBtn);
        abandonTaskBtn.onClick.AddListener(OnClickAbandonTaskBtn);
        acceptTaskBtn.onClick.AddListener(OnClickAcceptTaskBtn);
	}

    private void OnClickCloseBtn()
    {
        Taskmanger.instance.TransformState();
    }
    //接受任务
    private void OnClickAcceptTaskBtn()
    {         
        isInTask = true;
        finisgcontent.showProgress.gameObject.SetActive(true);
        finisgcontent.ShowTaskProgress();
        infinisheding.gameObject.SetActive(true);
        acceptTaskBtn.gameObject.SetActive(false);        
    }
    
    //放弃任务
    private void OnClickAbandonTaskBtn()
    {
        showButton();
    }
    //完成任务
    private void OnClickFinishTaskBtn()
    {
       bool isSuccess= finisgcontent.FinishTask();
       if (isSuccess)//如果任务完成，进行下一个任务
       {
           taskNumberid++;
           Getid(taskNumberid);
           if (taskNumberid >= 104)
           {
               taskNumberid = 101;
           }
           showButton();
           isInTask = false;
       }
    }
    void showButton()
    {
        infinisheding.gameObject.SetActive(false);
        acceptTaskBtn.gameObject.SetActive(true);
        finisgcontent.showProgress.gameObject.SetActive(false);
    }
    //自动寻路
    private void OnClickAutoFindBtn()
    {

    }
    //当鼠标指针位于collider上的时候系统会每一帧去调用
    // void OnMouseOver()
    //{
    //    if (isInTask)
    //    {
    //        Debug.Log("对对对");
    //    }
    //}
    void Getid(int id)
    {
        TaskInformation taskinfo = TaskMessage.instance.GetTaskmessageById(id);
        Taskcontent taskcontent = this.GetComponentInChildren<Taskcontent>();
        taskcontent.SetTaskId(id);
    }
    public override void TransformState()
    {
        finisgcontent.ShowTaskProgress();
        base.TransformState();
    }
    //打怪
    public void OnKillEnemy()
    {
        if (isInTask)
        {
            finisgcontent.OnKillEnemy();
        }
    }
}
