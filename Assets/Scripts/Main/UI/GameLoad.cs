using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad : MonoBehaviour {

	
	void Awake () {
        int selectedIndex = PlayerPrefs.GetInt("SelectedCharacterIndex");
        string playerName = PlayerPrefs.GetString("name");
        GameObject player = null;
        if (selectedIndex == 0)
        {
            player = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Player/Archer"));
        }
        else if (selectedIndex == 1)
        {
            player = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Player/Mage"));
        }
        else if (selectedIndex == 2)
        {
            player = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Player/Warrior"));
        }
        player.GetComponent<PlayerStatus>().name = playerName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
