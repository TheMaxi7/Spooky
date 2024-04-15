using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ally : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform enemy;
    public LayerMask whatIsGround, whatIsEnemy;

    public float attackRange;
    public bool enemyInAttackRange;

    public GameObject spellProjectile;
    public Transform firePoint;
    public float projectileSpeed = 100f;
    public float arcRange;
    private float rotationSpeed = 5;

    public Animator anim;

    public float timeBetweenAttacks = 2f;
    private bool alreadyAttacked;

    private void Awake()
    {
        enemy = GameObject.Find("Boss(Clone)").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    void Update()
    {
        enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);

        if (enemyInAttackRange)
        {
            AttackEnemy();
        }
        else
        {
            ChaseEnemy();
        }
    }

    private void ChaseEnemy()
    {
        agent.SetDestination(enemy.position);
        Vector3 direction = enemy.position - transform.position;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void AttackEnemy()
    {
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            anim.SetBool("isAttacking", true);
            InstantiateProjectile(firePoint);
            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
        anim.SetBool("isAttacking", false);
    }

    void InstantiateProjectile(Transform firePoint)
    {
        Vector3 direction = (enemy.position - firePoint.position).normalized;
        direction.y += 0.2f;
        var projectileObj = Instantiate(spellProjectile, firePoint.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenAttacks);
            AttackEnemy();
        }
    }
}
