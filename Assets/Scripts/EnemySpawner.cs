using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Tools;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private EnemyPool pool;
    private List<Transform> spawnPoints;
    private List<Transform> endPoints;
    private List<List<Vector3>> allPaths = new List<List<Vector3>>();
    [SerializeField] private PathFinder pathFinder;
    
    void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").Select(x => x.transform).ToList();
        endPoints = GameObject.FindGameObjectsWithTag("Base").Select(x => x.transform).ToList();
        
        if(spawnPoints.Count == 0) Debug.LogError("No Spawn Points detected");
        if(endPoints.Count == 0) Debug.LogError("No Bases detected");

        foreach (var spawnPoint in spawnPoints)
        {
            foreach (var endPoint in endPoints)
            {
                allPaths.Add(pathFinder.GetPath(spawnPoint.position, endPoint.position));
            }
        }
    }
    
    public void SpawnWave(int count)
    {
        StartCoroutine(SpawnNewWave(count));
    }

    private IEnumerator SpawnNewWave(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var newEnemyPath = allPaths[i % allPaths.Count];
            var newEnemy = pool.GetNewEnemy(newEnemyPath[0]);
            newEnemy.Initialize(GameManager.CurrentWave);
            newEnemy.StartMoving(newEnemyPath);
            yield return new WaitForSeconds(0.7f);
        }
    }
    
}
