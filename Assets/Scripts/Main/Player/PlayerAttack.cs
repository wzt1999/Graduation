using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//主角的状态
public enum PlayerState
{
    ControlWalk,
    NormalAttack,
    SkillAttack,
    Death
}

//攻击时候的状态
public enum AttackState
{
    Moving,
    Idle,
    Attack
}
public class PlayerAttack : MonoBehaviour {

    private PlayerMove playerMove;
    private Animation animation;

    public PlayerState playerState = PlayerState.ControlWalk;
    public AttackState attackState = AttackState.Idle;
    public string aniNameDeath;//死亡动画
    public string aniNameNormalAttack;//普通攻击的动画
    public string aniNameIdle;//idle的动画
    public string aniNameDeffence;//miss动画
    public string aniNameNow;//当前播放的动画
    public float timeNormalAttack=1.1f;//普通攻击动画播放的时间
    public float rateNormalAttack = 1;//攻击速率
    private float timer = 0;//计时器
    public float minDistance = 5;//最小的攻击距离
    private Transform targetAttack;//攻击目标
    private float missRate = 0.15f;
   private GameObject effectNormal;//普通攻击的特效
   private PlayerStatus playerStatus;
   private bool isShowEffect;//是否显示特效

   private float timeDeath;
   private GameObject body;
   private Color normalColor;
   public GameObject[] effectPrefabArray;
   private Dictionary<string, GameObject> efxDict = new Dictionary<string, GameObject>();

   private SkillInfo skillInfo;
   private ShortCutSlot shortCutSlot;
   private bool isLockTarget = false;//是否选中目标
	void Start () {
        animation = this.GetComponent<Animation>();
        playerMove = this.GetComponent<PlayerMove>();
        playerStatus = this.GetComponent<PlayerStatus>();
        body = transform.Find("Archer_Female_04").gameObject;
        normalColor = body.GetComponent<Renderer>().material.color;
        effectNormal = Resources.Load<GameObject>("Effect/norattackeffect");
        foreach (GameObject go in effectPrefabArray)
        {
            efxDict.Add(go.name, go);//让特效的名字和特效预制体建立联系
        }
	}
	
	
	void Update () {
        if (playerState == PlayerState.Death)
        {
            timeDeath += Time.deltaTime; 
            animation.CrossFade(aniNameDeath);
            if (timeDeath>=2.1)
            {
                animation.Stop();
            }          
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
           
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            //当点击到的是敌人
            if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.tag.Equals(Tags.enemy))
            {
               
                //设置攻击目标
                targetAttack = hitInfo.collider.transform;
                //主角进入攻击状态
                playerState = PlayerState.NormalAttack;
                timer = 0;
                isShowEffect = false;
            }
            else//点击到其他物品，需要继续保持行走状态
            {
                playerState = PlayerState.ControlWalk;
                targetAttack = null;
            }
        }
        //根据人物状态来进行对应处理
        switch (playerState)
        {
            case PlayerState.ControlWalk:
                break;
            case PlayerState.NormalAttack:
                NormalAttack();
                break;
            case PlayerState.SkillAttack:
                SkillAttack();
                break;
        }
	}

    //普通攻击
    void NormalAttack()
    {
        if (targetAttack != null)//找到攻击目标
        {
            if (Vector3.Distance(transform.position, targetAttack.position) <= minDistance)//在攻击范围内，需要进行攻击
            {   //进行攻击
                attackState = AttackState.Attack;
                transform.LookAt(targetAttack.position);
                animation.CrossFade(aniNameNow);
                timer += Time.deltaTime;
                if (timer >= timeNormalAttack)//一次攻击动作完成
                {
                    aniNameNow = aniNameIdle;//1.1
                  
                    if (!isShowEffect)//动作完成，开始播放特效
                    {
                        GameObject.Instantiate(effectNormal, new Vector3(targetAttack.position.x, targetAttack.position.y+1, targetAttack.position.z), Quaternion.identity);
                        bool isDeath = targetAttack.GetComponent<EnemyState>().TakeDamage(GetAttack());
                        if (isDeath)
                        {
                            targetAttack = null;
                        }
                        isShowEffect = true;
                    }
                }
                if (timer >=1.1) //(1f / rateNormalAttack))//1f/rateNormalAttack:攻击一次所需要的时间  表示一次攻击已经结束，需要进行切换
                {
                    timer = 0;
                    isShowEffect = false;
                    aniNameNow = aniNameNormalAttack;
                }
            }
            else//不再攻击范围内，需要走向敌人
            {
                attackState = AttackState.Moving;
                playerMove.SimpleMove(targetAttack.position);
            }

        }
        else//丢失攻击目标
        {
            playerState = PlayerState.ControlWalk;
        }
    }
    //计算攻击力
    int GetAttack()
    {
        return (int)(playerStatus.attack + playerStatus.attackPlus + playerStatus.attackEquip);
    }

    //技能攻击
    void SkillAttack()
    {
      
        if (skillInfo.applyType == ApplyType.SingleTarget || skillInfo.applyType == ApplyType.MultiTarget)//单个目标的技能攻击
        {           
            //开始锁定敌人
            LockEnemy();
        }
    }
    //锁定敌人
    void LockEnemy()
    {
        if (Input.GetMouseButtonDown(2) && !isLockTarget)//按下鼠标中键且没有选中敌人的时候，就需要开始选择敌人
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.tag.Equals(Tags.enemy))
            {
                //对技能施法的距离进行判定
                if (Vector3.Distance(transform.position, hitInfo.point) > skillInfo.distance)
                {
                    Debug.Log("超出施法距离");
                    CursorManager.instance.SetCursorNormal();
                    playerState = PlayerState.ControlWalk;
                    attackState = AttackState.Idle;
                    isLockTarget = false;
                }
                else//在范围内，可以施法
                {
                    if (shortCutSlot.OnUseSkillState(skillInfo))
                    {
                        isLockTarget = true;//锁定目标，技能释放
                        if (skillInfo.applyType == ApplyType.SingleTarget)
                        {
                            StartCoroutine(OnLockSingleTarget(skillInfo, hitInfo));//开启协程，进行技能释放
                        }
                        else if (skillInfo.applyType == ApplyType.MultiTarget)
                        {
                            StartCoroutine(OnLockMutipleTarget(skillInfo, hitInfo));
                        }
                    }
                }
            }
        }
    }
    //受到伤害
    public void TakeDamage(int enemyAttack)
    {//如果人物死亡，就不执行
        if (playerState==PlayerState.Death)
        {
            return;
        }
        float def = playerStatus.def + playerStatus.defPlus + playerStatus.defEquip;//总防御力
        //敌人攻击力-(30%*def)
        float damage = enemyAttack - (int)(0.3 * def);
        if (damage<=0)//处理最小伤害
        {
            damage = 1;
        }
        float missValue = Random.Range(0f, 1f);
        if (missValue < missRate)//miss
        {
            //animation.CrossFade(aniNameDeffence);
            //Debug.Log("miss");
            //playerState = PlayerState.ControlWalk;//停止攻击 
            //animation.CrossFade(aniNameDeffence);
            //timer += Time.deltaTime;
            //if (timer>0.8f)//一次闪避动作完成
            //{
            //    playerState = PlayerState.NormalAttack;
            //}
          
           
        }
        //受到伤害
        else
        {
            playerStatus.remainHP -= damage;
            StartCoroutine(ShowBodyState());
            if (playerStatus.remainHP<=0)//死了
            {
                playerState = PlayerState.Death;
            }
        }
        TopBar.instance.UpdateHeadShow();
    }

    //受到伤害身体颜色受到变化
    IEnumerator ShowBodyState()
    {
        body.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        body.GetComponent<Renderer>().material.color = normalColor;
    }
    //处理选择目标的准备工作（接收需要释放的技能信息）
    public void ReceiveSkillInfo(SkillInfo info, ShortCutSlot slot)
    {        
        skillInfo = info;
        shortCutSlot = slot;
    }
    //使用技能
    public void UseSkill(SkillInfo skillInfo)
    {
        switch (skillInfo.applyType)
        {
            //增益
            case ApplyType.Passive:
                StartCoroutine(OnUsePassiveSkill(skillInfo));
                break;
            case ApplyType.Buff:
               // StartCoroutine(OnUseBuffSkill(skillInfo));
                break;
            case ApplyType.SingleTarget:
             
                break;
            case ApplyType.MultiTarget:
               
                break;
        }
    }
    //处理增益技能
    IEnumerator OnUsePassiveSkill(SkillInfo skillInfo)
    {
        //动画播放
        animation.CrossFade(skillInfo.aniName);
        yield return new WaitForSeconds(skillInfo.aniTime);
        playerState = PlayerState.ControlWalk;
        //增加属性
        int hp = 0, mp = 0;
        if (skillInfo.applyProperty == ApplyProperty.HP)
        {
            hp = skillInfo.applyValue;
        }
        else if (skillInfo.applyProperty == ApplyProperty.MP)
        {
            mp = skillInfo.applyValue;
        }
        playerStatus.GetDrug(hp, mp);
        //实例化技能特效
        GameObject prefab = null;
        efxDict.TryGetValue(skillInfo.efxName, out prefab);
        GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
    }
    //处理单体技能
    
    IEnumerator OnLockSingleTarget(SkillInfo skillInfo, RaycastHit hitInfo)
    {
        //动画播放
        animation.CrossFade(skillInfo.aniName);
        yield return new WaitForSeconds(skillInfo.aniTime);
        playerState = PlayerState.ControlWalk;
        //实例化技能特效
        GameObject prefab = null;
        efxDict.TryGetValue(skillInfo.efxName, out prefab);
        GameObject.Instantiate(prefab, hitInfo.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        //特效播放完成后，需要进行伤害计算
      
        hitInfo.transform.GetComponent<EnemyState>().TakeDamage(GetAttack() * (skillInfo.applyValue / 10));
        //一次技能释放完成后，需要复位
        isLockTarget = false;
        CursorManager.instance.SetCursorNormal();
    }
    //处理群体技能
    IEnumerator OnLockMutipleTarget(SkillInfo skillInfo, RaycastHit hitInfo)
    {
        //动画播放
        animation.CrossFade(skillInfo.aniName);
        yield return new WaitForSeconds(skillInfo.aniTime);
        playerState = PlayerState.ControlWalk;
        //实例化技能特效
        GameObject prefab = null;
        efxDict.TryGetValue(skillInfo.efxName, out prefab);
        GameObject effcet = GameObject.Instantiate(prefab, hitInfo.point, Quaternion.identity);
        //特效播放完成后，需要进行伤害计算
        effcet.GetComponent<ArcherSphere>().attack = GetAttack() * (skillInfo.applyValue / 10);
        //一次技能释放完成后，需要复位
        isLockTarget = false;
        CursorManager.instance.SetCursorNormal();
    }
}
