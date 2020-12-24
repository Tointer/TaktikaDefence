using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerStats", menuName = "ScriptableObjects/TowerStats", order = 1)]
public class TowerStatsSO : ScriptableObject
{
    [Space(10)]
    [SerializeField] private float baseDamage  = 10f;
    [SerializeField] private float damageIncreaseRate = 2f;
    [SerializeField] private Functions damageProgressionFunction;
    [Space(10)]
    [SerializeField] private float baseAttackSpeed = 1f;
    [SerializeField] private float attackSpeedIncreaseRate = 0.1f;
    [SerializeField] private Functions attackSpeedProgressionFunction;
    [Space(10)]
    [SerializeField] private float baseAttackRange  = 1.5f;
    [SerializeField] private float attackRangeIncreaseRate = 0.01f;
    [SerializeField] private Functions attackRangeProgressionFunction;
    [Space(10)]
    [SerializeField] private int baseUpgradeCost = 10;
    [SerializeField] private int upgradeCostIncreaseRate = 5;
    [SerializeField] private Functions upgradeCostProgressionFunction;

    public float GetCurrentAttackSpeed(int level)
    {
        return baseAttackSpeed +
               MathUtils.GetFunction(attackSpeedProgressionFunction)
                   .Invoke(attackSpeedIncreaseRate * level);
    }

    public float GetCurrentDamage(int level)
    {
        return baseDamage +
               MathUtils.GetFunction(damageProgressionFunction)
                   .Invoke(damageIncreaseRate * level);
    }
    public int GetCurrentUpgradeCost(int level)
    {
        return baseUpgradeCost +
               (int)MathUtils.GetFunction(upgradeCostProgressionFunction)
                   .Invoke(upgradeCostIncreaseRate * level);
    }
    public float GetCurrentAttackRange(int level)
    {
        return baseAttackRange +
               MathUtils.GetFunction(attackRangeProgressionFunction)
                   .Invoke(attackRangeIncreaseRate * level);
    }
    
}
