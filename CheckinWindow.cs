
using UnityEngine;
using UnityEngine.UI;
using System;

public class CheckinWindow : MonoBehaviour
{
    #region
    public Text txttime;    //时间
    public Button getBtn;   //领取按钮
    public const string SignNumPrefs = "SignNum";//领取次数的字符串
    public const string SignDataPrefs = "lastDay";//上次领取的时间字符串
    private DateTime today;     //今日时间
    private DateTime stabilization_time;   //时间
    private TimeSpan interval;  //间隔时间
    private int signNum;    //签到次数  默认是0
    private bool isTime;    //是否显示时间
    #endregion
    private int totteryNum = 0;
    private void OnEnable()
    {
        today = DateTime.Now;
        signNum = PlayerPrefs.GetInt(SignNumPrefs, 0);
        stabilization_time = DateTime.Parse(PlayerPrefs.GetString(SignDataPrefs, DateTime.MinValue.ToString()));
        if (IsOneDay())//今天日期是否大于领取日期  可以领取
        {
            Debug.Log("可以领取！");
            if (signNum >= 7)//重新计算签到
            {
                PlayerPrefs.DeleteKey(SignNumPrefs);
                //TODO：把奖励物品重置
            }
            
            getBtn.gameObject.SetActive(true);

        }
        else //签到日期未到
        {
            isTime = true;
            //getBtn.interactable = false;
            getBtn.gameObject.SetActive(false);
            txttime.fontSize = 25;
        }
    }

    private void Update()
    {
        //TimeSpan time=DateTime.Now.AddDays(1).Date - DateTime.Now; //用后一天的时间减掉当前的时间，得到剩下的时分秒
        //Debug.Log(string.Format("{0:D2}:{1:D2}:{2:D2}s",time.Hours,time.Minutes,time.Seconds));
        if (isTime)
        {
            interval = stabilization_time.AddDays(1).Date - DateTime.Now;
            txttime.text = "还有：" + string.Format("{0:D2}:{1:D2}:{2:D2}", interval.Hours, interval.Minutes, interval.Seconds) + " 可签到";
        }
    }

    //签到领取奖励Button
    public void OnSignClick()
    {
        isTime = true;
        //getBtn.interactable = false;
        getBtn.gameObject.SetActive(false);
        txttime.fontSize = 25;
        signNum++;//签到次数
        stabilization_time = today;
        PlayerPrefs.SetString(SignDataPrefs, today.ToString());
        PlayerPrefs.SetInt(SignNumPrefs, signNum);
        totteryNum++;
        //TODO：奖励

    }

    //判断是否可以签到
    private bool IsOneDay()
    {
        if (stabilization_time.Year == today.Year && stabilization_time.Month == today.Month && stabilization_time.Day == today.Day)
        {
            return false;
        }
        if (DateTime.Compare(stabilization_time, today) < 0)//DateTime.Compare(t1,t2) 返回<0  t1<t2   等于0  t1=t2   返回>0 t1>t2
        {
            return true;
        }
        return false;
    }
    //抽奖次数
    public int LotteryNum()
    {
        return totteryNum;
    }

    public void OnClickCloseBtn()
    {
        Destroy(transform.gameObject);
    }
}


