using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkHandler : MonoBehaviour {
    private Animator anim;
    public Transform leftHand;
    public Transform rightHand;

    // Use this for initialization
    void Start () {
		anim  = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

        anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftHand.transform.position);
    }
}
