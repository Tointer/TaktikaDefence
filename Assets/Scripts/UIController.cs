using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
    , MMEventListener<CoinCountChanged>
    , MMEventListener<PlayerHpChanged>
    , MMEventListener<GameEnded>
{
    [SerializeField]
    private TextMeshProUGUI coinCount;
    [SerializeField]
    private TextMeshProUGUI playerHp;
    [SerializeField] 
    private TextMeshProUGUI gameOverText;
    [SerializeField]
    private Canvas gameOverCanvas;

    void OnEnable()
    {
        gameOverCanvas.enabled = false;
        playerHp.text = GameManager.PlayerHp.ToString();
        coinCount.text = GameManager.CoinAmount.ToString();
        this.MMEventStartListening<CoinCountChanged>();
        this.MMEventStartListening<PlayerHpChanged>();
        this.MMEventStartListening<GameEnded>();
    }
     
    void OnDisable()
    {
        this.MMEventStopListening<CoinCountChanged>();
        this.MMEventStopListening<PlayerHpChanged>();
        this.MMEventStopListening<GameEnded>();
    }

    public void OnMMEvent(PlayerHpChanged eventType)
    {
        playerHp.text = GameManager.PlayerHp.ToString();
    }

    public void OnMMEvent(CoinCountChanged eventType)
    {
        coinCount.text = GameManager.CoinAmount.ToString();
    }

    public void OnMMEvent(GameEnded eventType)
    {
        gameOverCanvas.enabled = true;
        gameOverText.text = $"Game over!\n\nLast wave: {GameManager.CurrentWave}\nEnemies killed: {GameManager.EnemiesKilled}";
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
