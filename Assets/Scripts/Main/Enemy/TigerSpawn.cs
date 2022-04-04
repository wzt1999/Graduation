using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Small,
    Middle,
    Big,
    Boss
}
public class TigerSpawn : MonoBehaviour
{

    public EnemyType enemyType = EnemyType.Small;
    public int maxNum;//最大数量
    public int currentNum;//当前数量
    public float time;//每几秒钟生成一个
    public float timer;//计时器
    private GameObject enemyPrefab;
	void Start () {
        switch (enemyType)
        {
            case EnemyType.Small:
                enemyPrefab = Resources.Load<GameObject>("Enemy/LaoHu01");
                break;
            case EnemyType.Middle:
                enemyPrefab = Resources.Load<GameObject>("Enemy/LaoHu02");
                break;
            case EnemyType.Big:
                enemyPrefab = Resources.Load<GameObject>("Enemy/LaoHu03");
                break;
            case EnemyType.Boss:
                enemyPrefab = Resources.Load<GameObject>("Enemy/LaoHu04");
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (currentNum < maxNum)
        {
            timer += Time.deltaTime;
            if (timer > time)//计时结束，需要生成一个怪物
            {
                Vector3 pos = transform.position;
                pos.x += Random.Range(-5, 5);
                pos.z += Random.Range(-5, 5);
                GameObject enemy = GameObject.Instantiate(enemyPrefab, pos, Quaternion.identity);
                enemy.GetComponent<EnemyState>().wolfSpawn = this;
                timer = 0;
                currentNum++;
            }
        }
	}

    //怪物数量减少
    public void MinusNumber()
    {
        this.currentNum--;
        if (this.currentNum <= 0)
        {
            this.currentNum = 0;
        }
    }
}
