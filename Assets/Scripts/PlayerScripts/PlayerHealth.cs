using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    


  

    public TMP_Text healthText;
    public Animator healthTextAnim;


    private void Start()
    {
        healthText.text = "HP: " + StatsManager.instance.currentHealth + "/" + StatsManager.instance.maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        StatsManager.instance.currentHealth += amount;
        healthTextAnim.Play("TextUpdate");

        healthText.text = "HP: " + StatsManager.instance.currentHealth + "/" + StatsManager.instance.maxHealth;

        if (StatsManager.instance.currentHealth <= 0)
        { 
        
            gameObject.SetActive(false);
        }
  
    }



}


