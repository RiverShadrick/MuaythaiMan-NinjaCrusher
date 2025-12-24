using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public AudioSource hitSource;
    public AudioSource deathSource;
    public int expReward = 5;

    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;

    public int currentHealth;
    public int maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }


    public void ChangeHealth(int amount)
    {
        if (amount < 0) { hitSource.Play(); }
        currentHealth += amount;

        if(currentHealth >maxHealth)
            {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            deathSource.Play();
            Debug.Log("Monster Defeated " + expReward);
            OnMonsterDefeated(expReward);
            Destroy(gameObject, .75f);
        }
    }



}
