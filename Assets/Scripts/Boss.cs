using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public static int health = 500;
    public static int startingHealth;


    public bool playerInAttackRange;
    public float attackRange;
    private bool alreadyAttacked = false;

    public GameObject spellProjectile;
    public float projectileSpeed = 30f;
    public Transform firePoint;
    private float rotationSpeed = 5;

    public AudioSource spawnSound;

    public Animator anim;

    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        startingHealth = health;
    }

    private void Start()
    {
        spawnSound.Play();
    }

    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (playerInAttackRange)
        {
            AttackPlayer();
        }
        else ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        Vector3 direction = player.position - transform.position;
        direction.y = 0f; 
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);


        if (!alreadyAttacked)
        {
            anim.SetBool("isAttacking", true);
            InstantiateProjectile(firePoint);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), 5);
            Invoke("StopAttackAnimation", 5f);
        }

    }

    void InstantiateProjectile(Transform firePoint)
    {
        Vector3 direction = (player.position - firePoint.position).normalized;
        direction.y += 0.018f;
        var projectileObj = Instantiate(spellProjectile, firePoint.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;

    }

    private IEnumerator AttackWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        InstantiateProjectile(firePoint);
        ResetAttack();
    }

    void StopAttackAnimation()
    {
        anim.SetBool("isAttacking", false);
    }

    private void ResetAttack()
    {
        anim.SetBool("isAttacking", false);
        alreadyAttacked = false;

    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            UiManager.isGameEnded = true;
            Destroy(gameObject);
        }
    }
}
