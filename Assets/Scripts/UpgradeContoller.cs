using TMPro;
using UnityEngine;

public class UpgradeContoller : MonoBehaviour
{
    [SerializeField]
    private Canvas tipCanvas;

    [SerializeField] private TextMeshProUGUI costText;

    private SimpleTower[] towers;
    private Camera myCam;
    private const string PanelString = "Click to upgrade\nCost: ";
    
    void Start()
    {
        towers = FindObjectsOfType<SimpleTower>();
        myCam = Camera.main;
        tipCanvas.enabled = false;
    }
    
    void Update()
    {
        var mousePos = (Vector2)myCam.ScreenToWorldPoint(Input.mousePosition);
        foreach (var tower in towers)
        {
            var towerPosition = (Vector2)tower.transform.position;
            if ((mousePos - towerPosition).magnitude > 0.5f) continue;

            transform.position = tower.transform.position - Vector3.right * 1.6f;
            tipCanvas.enabled = true;
            RefreshCost(tower);
            
            if(Input.GetMouseButtonDown(0)) TryToUpgrade(tower);
            return;
        }

        tipCanvas.enabled = false;
    }

    private void RefreshCost(SimpleTower targetSimpleTower)
    {
        costText.text = PanelString + targetSimpleTower.NextUpgradeCost;
    }

    private void TryToUpgrade(SimpleTower targetSimpleTower)
    {
        if(GameManager.CoinAmount < targetSimpleTower.NextUpgradeCost) return;
        GameManager.SpendCoins(targetSimpleTower.NextUpgradeCost);
        targetSimpleTower.LevelUp();
        RefreshCost(targetSimpleTower);
    }
    
}
