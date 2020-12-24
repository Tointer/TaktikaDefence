using System.Collections;
using System.IO;
using MoreMountains.Tools;
using UnityEngine;

public struct ConfigData
{
    public int WaveCooldown;
    public float GameSpeed;

    public ConfigData(int waveCooldown, float gameSpeed)
    {
        WaveCooldown = waveCooldown;
        GameSpeed = gameSpeed;
    }
}

public class GameManager : MonoBehaviour, 
    MMEventListener<EnemyDestroyed>, 
    MMEventListener<EnemyReachedEndPositon>
{

    [SerializeField]
    private EnemySpawner spawner;
    public static int CoinAmount { get; private set; }
    public static int PlayerHp { get; private set; } = 50;
    public static int CurrentWave { get; private set; }
    public static int EnemiesKilled { get; private set; }

    private int waveCooldown;
    
    void Start()
    {
        var configData = JsonUtility.FromJson<ConfigData>
            (File.ReadAllText(Application.dataPath + "/StreamingAssets/GameSettings.config"));
        waveCooldown = configData.WaveCooldown;
        Time.timeScale = configData.GameSpeed;
        Debug.Log($"Wave cooldown: {waveCooldown}, Game Speed: {Time.timeScale}");
        StartCoroutine(StartWaves());
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
    
    IEnumerator StartWaves()
    {
        while (true)
        {
            CurrentWave++;
            spawner.SpawnWave(Random.Range(CurrentWave, CurrentWave + CurrentWave/2));
            yield return new WaitForSeconds(10);
        }
    }

    public void OnMMEvent(EnemyDestroyed eventType)
    {
        EnemiesKilled++;
        var reward = eventType.Enemy.Reward;
        CoinAmount += reward;
        CoinCountChanged.Trigger(reward);
    }

    public void OnMMEvent(EnemyReachedEndPositon eventType)
    {
        var damage = eventType.Enemy.Damage;
        PlayerHp -= (int)damage;
        if (PlayerHp <= 0)
        {
            PlayerHp = 0;
            GameOver();
        }
        PlayerHpChanged.Trigger((int)damage);
    }

    public static void SpendCoins(int amount)
    {
        CoinAmount -= amount;
        CoinCountChanged.Trigger(amount);
    }

    private void GameOver()
    {
        GameEnded.Trigger();

        CoinAmount = 0;
        PlayerHp = 50;
        CurrentWave = 0;
        EnemiesKilled = 0;
        
        Time.timeScale = 0f;
    }
    
}
