using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseController : MonoBehaviour {

    protected NavMeshAgent agent;
    protected Animator animator;
    protected Collider bodyCollider;

    public HpIndicatorController hpIndicatorPrefab;
    private HpIndicatorController hpIndicator;

    protected Canvas canvas;

    public int hp;
    protected int initialHp;
    public float distanceAttack;
    protected bool dead;
    public int damagePoints;

    private Vector3 destination;


    // Use this for initialization
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<Collider>();
        dead = false;
        initialHp = hp;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        hpIndicator = Instantiate(hpIndicatorPrefab);
        hpIndicator.transform.SetParent(canvas.transform);
        hpIndicator.UpdateIndicator(1, Camera.main.WorldToScreenPoint(transform.position));
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!dead)
        {
        hpIndicator.UpdateIndicator((float)hp / (float)initialHp, Camera.main.WorldToScreenPoint(transform.position));
            if (Player.Instance != null)
            {
                if (distanceAttack > Vector3.Distance(Player.Instance.transform.position, transform.position))
                {
                    agent.Stop();
                    animator.SetBool("Attack", true);
                }
                else
                {
                    agent.Resume();
                    animator.SetBool("Attack", false);
                }
                destination = Player.Instance.transform.position;
            }
            else {
                agent.Resume();
                animator.SetBool("Attack", false);
                if (distanceAttack > Vector3.Distance(transform.position, destination))
                {
                    Vector2 random = Random.insideUnitCircle * 5;
                    destination = new Vector3(destination.x + random.x, destination.y, destination.z + random.y);
                }
            }
            agent.destination = destination;
        }
    }

    public void Damage(int damage, Vector3 hitPosition, Vector3 hitDirection)
    {
        BloodParticleGenerator.Instance.GenerateBloodSplash(transform, hitPosition, hitDirection);
        hp -= damage;
        if (hp <= 0)
        {
            Spawner.Instance.ZombieDead();
            Dead();
        }
    }

    protected virtual void Dead()
    {
        Destroy(hpIndicator.gameObject);
        bodyCollider.enabled = false;
        agent.Stop();
        dead = true;
    }
}