using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private static Player instance;
    public static Player Instance { get { return instance; } }

    public PlayerAnimatorController animatorController;
    private CharacterController characterController;
    public Gun gun { get { return GunsManager.Instance.CurrentGun(); } }
    //public Gun gun;

    private Vector3 moveDirection = Vector3.zero;
    public float walkSpeed;
    public float runSpeed;
    public float jumpSpeed;
    public float gravity;

    private bool blockRun;
    public float maxStaminaTime;
    public float staminaTime;

    public int hp;
    public int maxHp;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    void Start () {
        animatorController = GetComponent<PlayerAnimatorController>();
        characterController = GetComponent<CharacterController>();
        maxHp = hp;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        LookAtMouse();
        Fire();
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gun.PullTrigger();
        }

        if (Input.GetMouseButtonUp(0))
        {
            gun.ReleaseTrigger();
        }

        if (Input.GetKeyUp(KeyCode.E))
            GunsManager.Instance.NextGun();
        if (Input.GetKeyUp(KeyCode.Q))
            GunsManager.Instance.PrevGun();
    }

    void Move()
    {
        if (characterController.isGrounded)
        {
            var axisX = Input.GetAxis("Horizontal");
            var axisY = Input.GetAxis("Vertical");

            moveDirection = new Vector3(axisX, 0, axisY);
            moveDirection = transform.TransformDirection(moveDirection).normalized;

            animatorController.Move(axisX, axisY);

            if (Input.GetKey(KeyCode.LeftShift) && moveDirection != Vector3.zero && !blockRun)
            {
                staminaTime += Time.deltaTime;
                moveDirection *= runSpeed;
                animatorController.Run();
            }
            else if (moveDirection != Vector3.zero)
            {
                staminaTime -= Time.deltaTime;
                moveDirection *= walkSpeed;
                animatorController.Walk();
            }
            else {
                staminaTime -= Time.deltaTime;
                animatorController.Idle();
            }
            staminaTime = Mathf.Clamp(staminaTime, 0, maxStaminaTime);

            if (staminaTime == maxStaminaTime)
                blockRun = true;

            if (blockRun && staminaTime == 0)
                blockRun = false;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void LookAtMouse()
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        position = Camera.main.ScreenToWorldPoint(position);
        position.y = gun.transform.position.y;
        Debug.DrawRay(position, Vector3.forward, Color.blue);
        Debug.DrawRay(position, Vector3.right, Color.blue);
        Debug.DrawRay(position, Vector3.left, Color.blue);
        Debug.DrawRay(position, Vector3.back, Color.blue);

        if (Vector3.Distance(position, transform.position) > 2)
        {
            transform.LookAt(new Vector3(position.x, transform.position.y, position.z));

            //gun.transform.forward = (position - gun.transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemys"))
        {
            Damage(10);
            BloodParticleGenerator.Instance.GenerateBloodSplash(transform, other.transform.position, other.GetComponentInParent<Collider>().transform.forward);
        }
    }

    public void Damage(int damage)
    {
        hp--;
        if (hp <= 0)
            Dead();
    }

    public void Dead() {
        Destroy(gameObject);
    }

    public void Heal(int value)
    {
        hp += value;
    }
}
