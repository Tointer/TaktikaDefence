using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStatsSO : ScriptableObject
{
    [Space(10)]
    [SerializeField] private float baseDamage  = 1f;
    [SerializeField] private float damageIncreaseRate = 0.5f;
    [SerializeField] private Functions damageProgressionFunction;
    [Space(10)]
    [SerializeField] private int baseReward = 1;
    [SerializeField] private float rewardIncreaseRate = 0.5f;
    [SerializeField] private Functions rewardProgressionFunction;
    [Space(10)]
    [SerializeField] private float baseHp  = 10f;
    [SerializeField] private float hpIncreaseRate = 2f;
    [SerializeField] private Functions hpProgressionFunction;
    [Space(10)]
    [SerializeField] private float baseSpeed  = 1f;
    [SerializeField] private float speedIncreaseRate = 0f;
    [SerializeField] private Functions speedProgressionFunction;
    
    public float GetCurrentDamage(int level)
    {
        return baseDamage +
               MathUtils.GetFunction(damageProgressionFunction)
                   .Invoke(damageIncreaseRate * level);
    }
    public int GetCurrentReward(int level)
    {
        return baseReward +
               (int)MathUtils.GetFunction(rewardProgressionFunction)
                   .Invoke(rewardIncreaseRate * level);
    }
    public float GetCurrentMaxHp(int level)
    {
        return baseHp +
               MathUtils.GetFunction(hpProgressionFunction)
                   .Invoke(hpIncreaseRate * level);
    }
    
    public float GetCurrenSpeed(int level)
    {
        return baseSpeed +
               MathUtils.GetFunction(speedProgressionFunction)
                   .Invoke(speedIncreaseRate * level);
    }
}
