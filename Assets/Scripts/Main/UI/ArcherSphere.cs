using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherSphere : MonoBehaviour
{

    public int attack;
    private List<EnemyState> enemyList = new List<EnemyState>();
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == Tags.enemy)//保证每个敌人只受到一次伤害
        {
            EnemyState enemyState = col.GetComponent<EnemyState>();
            int index = enemyList.IndexOf(enemyState);
            if (index == -1)//当前怪物没有在list中，那么这个怪物需要受到伤害，伤害后加入列表中，下一次就不会再受到伤害
            {
                Debug.Log("555"+attack);
                enemyState.TakeDamage(attack);
                enemyList.Add(enemyState);
            }
        }
    }
}
