using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    
    public static StatsManager instance;
    public TMP_Text healthText;

    [Header("Combat Stats")]
    public int damage = 1;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    [Header ("Movement Stats")]
    public float speed;

    [Header("Health Stats")]

    public int maxHealth;
    public int currentHealth;

    private void Awake()
    {
        if (instance == null)
            instance = this;
      
        else
            Destroy(gameObject);
        
    }

    public void UpdateMaxHealth(int Amount)
    {
        maxHealth += Amount;
        healthText.text = "HP: " + currentHealth + "/" + maxHealth;

    }

}
