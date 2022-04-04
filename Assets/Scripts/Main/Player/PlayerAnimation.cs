using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    private Animation animation;
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;
	void Start () {
        animation = this.GetComponent<Animation>();
        playerMove = this.GetComponent<PlayerMove>();
        playerAttack = this.GetComponent<PlayerAttack>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (playerAttack.playerState==PlayerState.Death)
        {
            return;
        }
        if (playerAttack.playerState==PlayerState.ControlWalk)
        {
            if (playerMove.contorWalkState == ContorWalkState.Idle)
            {
                animation.Play("Idle");
            }
            else if (playerMove.contorWalkState == ContorWalkState.Run)
            {
                animation.Play("Run");
            }
        }
        else if (playerAttack.playerState==PlayerState.NormalAttack)
        {
            if (playerAttack.attackState==AttackState.Moving)
            {
                animation.Play("Run");
            }
        }
       
	}
}
