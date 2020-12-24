using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour, MMEventListener<EnemyDestroyed>
{
    [SerializeField] private ParticleSystem myParticleSystem;
    void OnEnable()
    {
        this.MMEventStartListening<EnemyDestroyed>();
    }
     
    void OnDisable()
    {
        this.MMEventStopListening<EnemyDestroyed>();
    }

    public void OnMMEvent(EnemyDestroyed eventType)
    {
        transform.position = eventType.Enemy.transform.position;
        myParticleSystem.Play();
    }
}
