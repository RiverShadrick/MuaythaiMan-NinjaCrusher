using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public PlayerCombat combat;

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
    }


    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
    }


    private void HandleAbilityPointSpent(SkillSlot slot) 
    { 
        string skillName = slot.skillSO.skillName;

        switch (skillName)
        {
            case "Max Health Boost":
                StatsManager.instance.UpdateMaxHealth(1);
                break;

            case "Slash":
                combat.enabled = true;
                break;


            default:
                Debug.LogWarning("Unhandled skill: " + skillName);
                break;
        }

    }

}




