using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlayerDir : MonoBehaviour
{
    private bool isMoving = false;
    [HideInInspector]//标签，只对下面一行起效，在面板上隐藏坐标
    //主角需要移动到的目标位置
    public Vector3 targetPosition = Vector3.zero;

    public GameObject effectClickPrefab;
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;
    void Start()
    {
        targetPosition = transform.position;
        playerMove = this.GetComponent<PlayerMove>();
        playerAttack = this.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttack.playerState == PlayerState.Death)
        {
            return;
        }
        //判断是否点击的是UI界面，防止打开任务版的时候，在三D世界中移动
        //EventSystem.current.IsPointerOverGameObject()
        if (Input.GetMouseButtonDown(0)&& !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.tag == Tags.ground)
            {
                showClickEffect(hitInfo.point);
            }
            isMoving = true;

        }
        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;

        }
        //开始移动  
        //1.得到要移动到的目标位置  2.主角朝向目标位置  3.移动到目标位置
        if (isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider.tag == Tags.ground)
            {
                LookAtTarget(hitInfo.point);
            }
        }
        else
        {
            //在移动的过程，需要去更新角色的朝向，避免产生误差
            if (playerMove.contorWalkState == ContorWalkState.Run)
            {
                LookAtTarget(targetPosition);
            }
        }
    }

    //让主角朝向目标位置
  void LookAtTarget(Vector3 hitPoint)
    {
        targetPosition = hitPoint;
        targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        transform.LookAt(targetPosition);
    }
    //实例化特效
    void showClickEffect(Vector3 hitPoint)
    {
        hitPoint = new Vector3(hitPoint.x, hitPoint.y + 0.1f, hitPoint.z);
        GameObject.Instantiate(effectClickPrefab, hitPoint, Quaternion.identity);
    }
}
