using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weoponRange;
    public LayerMask PlayerLayer;

    public float knockbackForce;
    public float stunTime;


   

    public void Attack()
        {
      
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weoponRange, PlayerLayer);

        if (hits.Length > 0) 
        {

            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
            hits[0].GetComponent<PlayerMovement>().Knockback(transform, knockbackForce, stunTime);
        }
    }


} 
    
        


