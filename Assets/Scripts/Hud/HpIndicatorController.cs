using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpIndicatorController : MonoBehaviour {

    public Image hpBar;

    public void UpdateIndicator(float value, Vector3 newPosition)
    {
        hpBar.fillAmount = value;
        transform.position = newPosition;
    }
}
