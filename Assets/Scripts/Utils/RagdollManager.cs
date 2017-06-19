using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour {

    public bool enable;
    public bool ragdollStatus;
    private Rigidbody[] ragdollElements;
    private Animator animator;

    // Use this for initialization
    void Start () {
        ragdollElements = GetComponentsInChildren<Rigidbody>();
        animator = GetComponentInParent<Animator>();
        DisableRagdoll();
        enable = false;
    }

    void Update()
    {
        if (enable)
            EnableRagdoll();
        else
            DisableRagdoll();
    }

    public void EnableRagdoll()
    {
        animator.enabled = false;
        foreach (Rigidbody rb in ragdollElements)
            rb.isKinematic = false;
    }

    public void DisableRagdoll()
    {
        animator.enabled = true;
        foreach (Rigidbody rb in ragdollElements)
            rb.isKinematic = true;
    }

}
