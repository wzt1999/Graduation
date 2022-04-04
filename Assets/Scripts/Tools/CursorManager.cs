using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {
    public static CursorManager instance;

	private Texture2D Cursor_combat;
    private Texture2D Cursor_normal;
    private Texture2D Cursor_game1;
    private Texture2D Cursor_NpcTalk;
    private Texture2D Cursor_pickup;

    private Vector2 hostpos = Vector2.zero;
    
    private CursorMode cursirMode = CursorMode.Auto;
	void Start () {
        //单例模式
        instance = this;
        Cursor_combat = Resources.Load<Texture2D>("mousecursor/Cursor-combat");
        Cursor_normal = Resources.Load<Texture2D>("mousecursor/Cursor-normal");
        Cursor_game1 = Resources.Load<Texture2D>("mousecursor/Cursor_game1");
        Cursor_NpcTalk = Resources.Load<Texture2D>("mousecursor/Cursor-Npc Talk");
        Cursor_pickup = Resources.Load<Texture2D>("mousecursor/Cursor-pickup");
	}
    //正常的鼠标样式
	public void SetCursorNormal()
    {
        Cursor.SetCursor(Cursor_normal, hostpos, cursirMode);
    }
    //npc鼠标样式
    public void SetCursorNpcTalk()
    {
        Cursor.SetCursor(Cursor_NpcTalk, hostpos, cursirMode);
    }
    //拾取的鼠标样式
    public void SetCursorPickup()
    {
        Cursor.SetCursor(Cursor_pickup, hostpos, cursirMode);
    }
    //点击到敌人的鼠标样式
    public void SetCursorcCombat()
    {
        Cursor.SetCursor(Cursor_combat, hostpos, cursirMode);
    }
    //UI界面里的鼠标样式
    public void SetCursorGame1()
    {
        Cursor.SetCursor(Cursor_game1, hostpos, cursirMode);
    }
   
}
