using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour {

    int walkHash = Animator.StringToHash("Walk");
    int runHash = Animator.StringToHash("Run");
    int fireHash = Animator.StringToHash("Fire");
    int shootMultiplier = Animator.StringToHash("ShootMultiplier");
    int singleFireHash = Animator.StringToHash("SingleFire");

    int axisXHash = Animator.StringToHash("AxisX");
    int axisYHash = Animator.StringToHash("AxisY");

    private Animator animator;

    // Use this for initialization
    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void SingleFire()
    {
        animator.SetTrigger(singleFireHash);
        animator.SetBool(fireHash, false);
    }

    public void StartFire(float speed)
    {
        animator.SetFloat(shootMultiplier, speed);
        animator.SetBool(fireHash, true);
    }

    public void StopFire()
    {
        animator.SetFloat(shootMultiplier, 1);
        animator.SetBool(fireHash, false);
    }

    public void Idle()
    {
        animator.SetBool(walkHash, false);
        animator.SetBool(runHash, false);
    }

    public void Walk()
    {
        animator.SetBool(walkHash, true);
        animator.SetBool(runHash, false);
    }

    public void Run()
    {
        animator.SetBool(runHash, true);
        animator.SetBool(walkHash, false);
    }

    public void Move(float axisX, float axisY) {
        animator.SetFloat(axisXHash, axisX);
        animator.SetFloat(axisYHash, axisY);
    }

}
