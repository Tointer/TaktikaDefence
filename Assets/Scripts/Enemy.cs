using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public EnemyStatsSO myStats;
    [SerializeField] private TowerTarget target;
    
    public int Reward { get; private set; }
    public float Damage { get; private set; }
    
    private float currentHp;
    private float currentMaxHp;
    private float currentSpeed;

    private void OnEnable()
    {
        target.OnGetHit += OnHit;
    }

    private void OnDisable()
    {
        target.OnGetHit -= OnHit;
    }

    public void StartMoving(IEnumerable<Vector3> movepoints)
    {
        var noise = new Vector3((Random.value - 0.5f)/2, (Random.value - 0.5f)/2, 0);
        var myPath = new Queue<Vector3>(movepoints.Select(x => x + noise));
        StartCoroutine(MoveByWayPoints(myPath));
    }

    private IEnumerator MoveByWayPoints(Queue<Vector3> waypoints)
    {
        var count = waypoints.Count;

        for (var i = 0; i < count; i++)
        {
            var lastPosition = transform.position;
            var currentTarget = waypoints.Dequeue();
            float t = 0;
            while (transform.position != currentTarget)
            {
                t += (Time.deltaTime * currentSpeed)/(lastPosition-currentTarget).magnitude;
                transform.position = Vector3.Lerp(lastPosition, currentTarget, t);
                yield return null;
            }
        }
        
        EnemyReachedEndPositon.Trigger(this);
    }

    public void Initialize(int levelNumber)
    {   
        currentMaxHp = myStats.GetCurrentMaxHp(levelNumber);
        currentSpeed = myStats.GetCurrenSpeed(levelNumber);
        Damage = myStats.GetCurrentDamage(levelNumber);
        Reward = myStats.GetCurrentReward(levelNumber);
        currentHp = currentMaxHp;
    }

    private void OnHit(float damage)
    {
        currentHp -= damage;
        
        if(currentHp <= 0)
            EnemyDestroyed.Trigger(this);
    }
}
