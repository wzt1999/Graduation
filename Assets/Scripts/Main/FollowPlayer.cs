using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    private Transform player;
    private Vector3 offsetPosition;//位置偏移

    private float distance = 0;//主角与摄像机之间的距离
    private float scrollSpeed = 10;//镜头拉近拉远的速度

    private bool isRotating = false;//当按下鼠标右键才进行旋转
    private float rotateSpeed = 2;
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        transform.LookAt(player.position);
        offsetPosition = transform.position - player.position;
	}
	
	
	void Update () {
        RotateView();
        ScrollView();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameOverPanel.instance.TransformState();
        }
	}
    void LateUpdate()
    {
        transform.position = player.position + offsetPosition;
    }
    //旋转视角
    void RotateView()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }
        if (isRotating)
        {
            //左右旋转
            transform.RotateAround(player.position, player.up, rotateSpeed * Input.GetAxis("Mouse X"));
            //处理上下的视角
            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;
            transform.RotateAround(player.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
           //限制上下的旋转
            float x = transform.eulerAngles.x;
            if (x<10||x>75)
            {
                transform.position = originalPos;
                
                transform.rotation = originalRotation;
            }
            //旋转完成后需要更新偏移量
            offsetPosition = transform.position - player.position;
        }
    }

    //处理摄像机拉近与拉远
    void ScrollView()
    {
        //距离开方公式
        distance = offsetPosition.magnitude;
        //得到最新的摄像机与人物之间距离
        distance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        //作限制，
        distance = Mathf.Clamp(distance, 2, 18);
        //计算新的偏移量
        offsetPosition = offsetPosition.normalized * distance;
    }
}
