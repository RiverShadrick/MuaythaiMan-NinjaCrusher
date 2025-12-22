using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.XR;

public class Enemy_Movement : MonoBehaviour
{
    public float Speed;
    public float attackRange = 2;
    public float attackCooldown = 2;
    public Transform detectionPoint;
    public LayerMask playerLayer;
    public float playerDetectRange = 5;



    private float attackCooldownTimer;
    private int facingDirection = 1;
    private Rigidbody2D rb;
    private Transform Player;
    private Animator anim;
    private EnemyState enemyState;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }


    // Update is called once per frame
     void Update()
    {
        CheckForPlayer();
        if (enemyState != EnemyState.Knockback)

        if (attackCooldownTimer > 0)
        { 
        attackCooldownTimer -= Time.deltaTime;
        }

        if (enemyState == EnemyState.Chasing)
        {
            Chase();
        }
        else if ( enemyState ==EnemyState.Attacking)
        {
            //attacky stuff

            rb.linearVelocity = Vector2.zero;
        }
    }

   void Chase()
    {
      
        if (Player.position.x < transform.position.x && facingDirection == -1 ||
            Player.position.x > transform.position.x && facingDirection == 1)
        {
            Flip();
        }

        Vector2 direction = (Player.position - transform.position).normalized;
        rb.linearVelocity = direction * Speed;
    }   






    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);

        if (hits.Length > 0)
        {

            Player = hits[0].transform;


            if (Vector2.Distance(transform.position, Player.position) <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }

            else if (Vector2.Distance(transform.position, Player.position) > attackRange && enemyState != EnemyState.Attacking)
            {

                ChangeState(EnemyState.Chasing);

            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }

    }



    void Flip()
    {
        facingDirection *= -1;
       transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }




    public void ChangeState(EnemyState newState)
    {
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);
        else if (enemyState == EnemyState.Knockback)
            anim.SetBool("KnockBack", false);


        enemyState = newState;

        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", true);
        else if (enemyState == EnemyState.Knockback)
            anim.SetBool("KnockBack", true);
    }

}


public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Knockback,
}