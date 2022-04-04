using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//状态控制
public enum ContorWalkState
{
    Run,
    Idle
}
public class PlayerMove : MonoBehaviour
{
    public ContorWalkState contorWalkState = ContorWalkState.Idle;
    private float speed = 4;
    private CharacterController characterController;
    private PlayerDir playerDir;
    private PlayerAttack playerAttack;
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        playerDir = this.GetComponent<PlayerDir>();
        playerAttack = this.GetComponent<PlayerAttack>();
    }

    
    void Update()
    {
        if (playerAttack.playerState == PlayerState.Death)
        {
            return;
        }
        //当进入战斗状态时就不能进行移动
        if (playerAttack.playerState==PlayerState.ControlWalk)
        {
            //目标位置与主角位置大于某个范围的时候就需要移动
            if (Vector3.Distance(playerDir.targetPosition, transform.position) > 0.2f)
            {
                contorWalkState = ContorWalkState.Run;
                characterController.SimpleMove(transform.forward * speed);
            }
            else
            {
                contorWalkState = ContorWalkState.Idle;
            }
        }
        
    }
    //移动的方法
    public void SimpleMove(Vector3 targetPos)
    {
        transform.LookAt(targetPos);
        characterController.SimpleMove(transform.forward * speed);
    }
}
