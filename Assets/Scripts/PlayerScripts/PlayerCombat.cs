using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator anim;
    public Transform attackPoint;

    public LayerMask enemyLayer;

    public float cooldown = 0;
    private float timer;

    // Particle prefab to spawn when hitting an enemy (assign in Inspector)
    public ParticleSystem bloodPrefab;          

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);

            timer = cooldown;
        }
    }

    public void FinishAttacking()
    { 
        anim.SetBool("isAttacking", false);
    }

    public void dealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.instance.weaponRange, enemyLayer);

        if (enemies.Length > 0)
        {
            var enemy = enemies[0];
            enemy.GetComponent<Enemy_Health>().ChangeHealth(-StatsManager.instance.damage);
            enemy.GetComponent<Enemy_Knockback>().Knockback(transform, StatsManager.instance.knockbackForce, StatsManager.instance.knockbackTime, StatsManager.instance.stunTime);

            // Spawn blood particle effect at the enemy position
            if (bloodPrefab != null)
            {
                Vector3 spawnPos = enemy.transform.position;
                ParticleSystem ps = Instantiate(bloodPrefab, spawnPos, Quaternion.identity);
                
                float life = ps.main.duration;
                Destroy(ps.gameObject, life + 0.5f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(attackPoint.position, StatsManager.instance.weaponRange);
    }

}

