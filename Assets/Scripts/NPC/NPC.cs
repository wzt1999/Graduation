using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    //鼠标移上去
    void OnMouseEnter()
    {
        CursorManager.instance.SetCursorNpcTalk();
    }
    //鼠标移开的时候
    void OnMouseExit()
    {
        CursorManager.instance.SetCursorNormal();
    }
}
