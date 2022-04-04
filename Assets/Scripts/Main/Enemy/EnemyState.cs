using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WolfState
{
    Idle,
    Walk,
    Attack,
    Death
}
public class EnemyState : MonoBehaviour {
    private Animation animation;
    private CharacterController characterController;
    private GameObject yellowTiger;
    public WolfState wolfstate=WolfState.Idle;
    public int hp = 100;
    private Color normalColor;
    private Image hpFill;
    //总血量
    public float totalHp=100;
    public int attack = 10;
    public int exp = 40;
    public float missRate = 0.1f;
    public string aniNameDeath;
    public string aniNameIdle;
    public string aniNameWalk;
    public string aniNameRun;
    public string aniNameDodge;
    public string aniNameNow;
    //巡逻速度
    public float patrolSpeed = 1;
    //计时器
    public float timer = 0;
    public float time = 1.6f;

    public string aniNameNormalAttack;//普通攻击的动画
    public float timeNormalAttack;//普通攻击播放动画所需要时间
    public string aniNameCrazyAttack;//暴击攻击的动画
    public float timeCrazyAttack;//暴击攻击的时间
    public float rateCrazyAttack;//暴击的概率（普通攻击和暴击之间切换）
    public string aniNameAttackNow;//当前攻击所播放的动画
    public int rateAttack = 1;//攻速  每秒
    public float attackTimer = 0;//攻击的计时器

    public Transform target;//攻击目标
    public float minDistance = 2;//最小攻击范围
    public float maxDistance = 5;//最大攻击范围

    public TigerSpawn wolfSpawn;
    private PlayerStatus playerStatus;
    private PlayerAttack playerAttack;

	void Start () {
        animation = this.GetComponent<Animation>();
        characterController = this.GetComponent<CharacterController>();
        yellowTiger = transform.Find("LaoHu_01").gameObject;
        normalColor = yellowTiger.GetComponent<Renderer>().material.color;
        aniNameNow = aniNameIdle;
        hpFill = transform.Find("CanvasEnemy/hp/hpfill").GetComponent<Image>();
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        playerAttack = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAttack>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (wolfstate)
        {
            case WolfState.Idle:
                Patrol();
                break;
            case WolfState.Walk:
               // Patrol();
                break;
            case WolfState.Attack://攻击
                AutoAttack();
                break;
            case WolfState.Death://死亡
                animation.CrossFade(aniNameDeath);
                break;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(10);
        }
	}
    void Patrol()
    {
        animation.CrossFade(aniNameNow);
        if (aniNameNow==aniNameWalk)
        {
            characterController.SimpleMove(transform.forward * patrolSpeed);
        }
        timer += Time.deltaTime;
        if (timer>=time)
        {
            timer = 0;
            RandomState();
        }
    }
    //随机怪物位置
    void RandomState()
    {
        int state = Random.Range(0, 2);
        if (0==state)
        {
            aniNameNow = aniNameIdle;
        }
        else
        {
            if (aniNameWalk==aniNameNow)
            {
                transform.Rotate(transform.up * Random.Range(0, 360));
            }
            aniNameNow = aniNameWalk;
        }
    }
    //收到伤害
    public bool TakeDamage(int attackPro)
    {
        if (wolfstate==WolfState.Death)
        {
            return true;
        }
        float missValue = Random.Range(0f, 1.0f);//随机miss的概率
        if (missValue < missRate)
        { 
            //ToDO 闪避动作
           // aniNameNow = aniNameDodge;
           
        }
        else//被打中
        {
            this.hp -= attackPro;
            hpFill.fillAmount = this.hp / totalHp;
            //受到伤害将主角作为目标攻击
            target = GameObject.FindGameObjectWithTag(Tags.player).transform;
            wolfstate = WolfState.Attack;
            StartCoroutine(ShowBodyRed());
            if (this.hp<=0)
            {
                wolfstate = WolfState.Death;
                GameObject.Destroy(this.gameObject, 1f); 
                return false;
            }
        }
        return false;
    }
    //身体颜色改变
    IEnumerator ShowBodyRed()
    {
        yellowTiger.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.6f);
        yellowTiger.GetComponent<Renderer>().material.color = normalColor;
    }
    //自动攻击
    void AutoAttack()
    {
        //如果目标不为空,可以攻击 在攻击范围内，在攻击范围外，以及大于最小攻击范围，小于最大攻击范围
        if (null!=target)
        {
            if (playerAttack.playerState == PlayerState.Death)
            {
                target = null;
                wolfstate = WolfState.Idle;
                return;
            }
            //判断距离
            float distance = Vector3.Distance(target.position, transform.position);
            //不在攻击范围内，停止攻击，恢复状态
            if (distance>maxDistance)
            {
                target = null;
                wolfstate = WolfState.Idle;
                this.hp = (int)totalHp;
            }
                //在范围内
            else if (distance<=minDistance)
            {//敌人看向目标
                transform.LookAt(target);
                //播放动画
                animation.CrossFade(aniNameAttackNow);
                attackTimer += Time.deltaTime;
                //当前播放动画为普通攻击
                if (aniNameAttackNow==aniNameNormalAttack)
                {
                    //计时器大于普通攻击的时间，代表一次普通攻击完成
                    if (attackTimer>timeNormalAttack)
                    {
                        //TODO   人物那边实现（受到伤害）
                        target.GetComponent<PlayerAttack>().TakeDamage(attack);
                        //动作复位
                        aniNameNow = aniNameIdle;
                    }
                }
                //当前播放的动画为暴击攻击
                else if (aniNameAttackNow==aniNameCrazyAttack)
                {
                    if (attackTimer>timeCrazyAttack)
                    {
                        int crazyattack = 2 * attack;
                        // 受到伤害
                        target.GetComponent<PlayerAttack>().TakeDamage(crazyattack);
                        //动作复位
                        aniNameNow = aniNameIdle;
                    }
                }
                //一次攻击结束
                if (attackTimer>1.6)//(1f/rateAttack))//   1f/rateAttack:每一次攻击所需要的时间 attackTimer：攻击的计时器
                {
                    attackTimer = 0;
                    //随机下一次的攻击
                    RandomAttack();
                }
            }
            //大于最小攻击距离，小于最大攻击距离  需要进行追击
            else
            {
                transform.LookAt(target);
                characterController.SimpleMove(transform.forward * patrolSpeed);
                animation.CrossFade(aniNameRun);
            }
        }
        //目标为空，巡逻
        else
        {
            wolfstate = WolfState.Idle;
        }
    }
    //随机攻击
    void RandomAttack()
    {
        float attackValue = Random.Range(0f, 1f);
        if (attackValue < rateCrazyAttack)//进行暴击
        {
            aniNameAttackNow = aniNameCrazyAttack;
        }
        else//普通攻击
        {
            aniNameAttackNow = aniNameNormalAttack;
        }
    }

    void OnDestroy()
    {
        if (wolfSpawn != null)
        {
            wolfSpawn.MinusNumber();
        }
        playerStatus.GetExp(exp);
        Taskmanger.instance.OnKillEnemy();
       // BarNPC.instance.OnKillEnemy();
    }
    void OnMouseEnter()
    {
        CursorManager.instance.SetCursorcCombat();
    }
    void OnMouseExit()
    {
        CursorManager.instance.SetCursorNormal();
    }
}
