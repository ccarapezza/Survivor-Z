using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {
    private int initialHp;
    public Image hpBar;
    public Image stBar;
	
	// Update is called once per frame
	void Update () {
        hpBar.fillAmount = (float)Player.Instance.hp / (float)Player.Instance.maxHp;
        stBar.fillAmount = 1f-(float)Player.Instance.staminaTime / (float)Player.Instance.maxStaminaTime;
    }
}
