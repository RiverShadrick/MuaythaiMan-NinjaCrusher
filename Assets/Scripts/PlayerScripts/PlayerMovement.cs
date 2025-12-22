using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public int facingDirection = 1;

    public Rigidbody2D rb;
    public Animator anim;
    public bool isKnockedback;
    public PlayerCombat playerCombat;

    private void Update()
    {
        if (Input.GetButtonDown("Slash"))
        {
            playerCombat.Attack();
        }
    }




    // Update 50x per second
    void FixedUpdate()
    {
        if (isKnockedback == false)
        { 
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal > 0 && transform.localScale.x < 0 ||
                horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }

            anim.SetFloat("horizontal", Mathf.Abs(horizontal));

            anim.SetFloat("vertical", Mathf.Abs(vertical));


            rb.linearVelocity = new Vector2(horizontal, vertical) * StatsManager.instance.speed;
        }
    }


    void Flip()
    {
        facingDirection *= -1;
        transform. localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }


    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKnockedback = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.linearVelocity = direction * force;
        StartCoroutine(KnockbackCounter(stunTime));
    }


    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity = Vector2.zero;
        isKnockedback = false;
    }
}
