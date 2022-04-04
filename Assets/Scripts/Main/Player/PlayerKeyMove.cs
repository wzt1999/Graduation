using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//按键移动
public class PlayerKeyMove : MonoBehaviour {

	//移动速度
    private float Playerspeed = 5;
    private CharacterController characterPlayer;
    private Vector3 moveDirection = Vector3.zero;
	void Start () {
        characterPlayer = this.GetComponent<CharacterController>();
	}
	

	void Update () {
		//使用charactercontroller移动
        //是否在地面上
        if (characterPlayer.isGrounded)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            moveDirection = new Vector3(h, 0, v);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= Playerspeed;
            //跳跃,空格
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = 5.0f;
            }
        }
        //模拟重力
        moveDirection.y -= 10 * Time.deltaTime;
        characterPlayer.Move(moveDirection * Time.deltaTime);

	}
}
