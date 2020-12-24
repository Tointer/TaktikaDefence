using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTarget : MonoBehaviour
{
    public static readonly HashSet<TowerTarget> allTargets = new HashSet<TowerTarget>();
    public event Action<float> OnGetHit;

    private void OnEnable()
    {
        allTargets.Add(this);
    }

    private void OnDisable()
    {
        allTargets.Remove(this);
    }

    public void GetHit(float damage)
    {
        OnGetHit?.Invoke(damage);
    }
}
