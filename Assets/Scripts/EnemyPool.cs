using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Tools;
using UnityEngine;

public class EnemyPool : MonoBehaviour, MMEventListener<EnemyDestroyed>, MMEventListener<EnemyReachedEndPositon>
{
    [SerializeField]
    private GameObject enemy;
    private HashSet<Enemy> activeEnemies = new HashSet<Enemy>();
    private HashSet<Enemy> availableEnemies = new HashSet<Enemy>();
    
    public Enemy GetNewEnemy(Vector3 position)
    {
        if (availableEnemies.Count == 0)
        {
            return Instantiate(enemy, position, Quaternion.identity).GetComponent<Enemy>();
        }
        var newEnemy = availableEnemies.First();
        newEnemy.gameObject.SetActive(true);
        activeEnemies.Add(newEnemy);
        availableEnemies.Remove(newEnemy);
        newEnemy.transform.position = position;
        return newEnemy;

    }
    
     void OnEnable()
     {
         this.MMEventStartListening<EnemyDestroyed>();
         this.MMEventStartListening<EnemyReachedEndPositon>();
     }
     
     void OnDisable()
     {
    	this.MMEventStopListening<EnemyDestroyed>();
        this.MMEventStopListening<EnemyReachedEndPositon>();
     }
     
    private void ReturnEnemyToPool(Enemy targetEnemy)
    {
        targetEnemy.gameObject.SetActive(false);
        availableEnemies.Add(targetEnemy);
        activeEnemies.Remove(targetEnemy);
    }
    
    public void OnMMEvent(EnemyDestroyed destroyedEnemy)
    {
        ReturnEnemyToPool(destroyedEnemy.Enemy);
    }

    public void OnMMEvent(EnemyReachedEndPositon reachedEnemy)
    {
        ReturnEnemyToPool(reachedEnemy.Enemy);
    }
}
