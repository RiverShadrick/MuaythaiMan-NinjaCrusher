using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SkillSlot : MonoBehaviour
{
    public List<SkillSlot> prerequisiteSkillSlots;
    public SkillSO skillSO;

   public Image skillIcon;

   public int currentLevel;
   public Button skillButton;
   public bool isUnlocked;
   
   public TMP_Text skillLevelText;

   public static event Action<SkillSlot> OnAbilityPointSpent;

   public static event Action<SkillSlot> OnSkillMaxed;

    private void OnValidate()
    {
       if(skillSO != null && skillLevelText != null)
       {
              UpdateUI();

        }

    }

    public void TryUpgradeSkill()
    {
        if (isUnlocked && currentLevel < skillSO.maxLevel)
        {
            currentLevel++;
            OnAbilityPointSpent?.Invoke(this);

            if (currentLevel >= skillSO.maxLevel) 
            {

                OnSkillMaxed?.Invoke(this);
            }

            UpdateUI();
        }
    }

    
    public bool CanUnlockSkill()
    {
        foreach (SkillSlot slot in prerequisiteSkillSlots)
        {
            if (!slot.isUnlocked || slot.currentLevel < slot.skillSO.maxLevel)
            {
                return false;
            }
        }
        return true;
    }



    public void Unlocked()
    {
        isUnlocked = true;
        UpdateUI();
    }



    private void UpdateUI()
    {
        skillIcon.sprite = skillSO.skillIcon;
        if (isUnlocked)
        { 
            skillButton.interactable = true;
            skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
            skillIcon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillIcon.color = Color.gray;
        }
    }
}
