using System.Collections;
using UnityEngine;

public class SimpleTower : MonoBehaviour
{
    public TowerStatsSO myStats;
    private int currentLevel;
    private float currentAttackSpeed;
    private float currentAttackRange;
    private float currentDamage;
    public int NextUpgradeCost { get; private set; }

    [SerializeField] private LineRenderer ray;
    [SerializeField] private SpriteRenderer rend;

    private Color startColor;

    private void Start()
    {
        LevelUp();
        startColor = rend.color;
        ray.colorGradient = GradientFromColor(Color.clear);
        StartCoroutine(StartFire());
    }
    

    private IEnumerator StartFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f/currentAttackSpeed);

            foreach (var target in TowerTarget.allTargets)
            {
                if ((target.transform.position - transform.position).sqrMagnitude < currentAttackRange*currentAttackRange + 0.5f)
                {
                    StartCoroutine(LaunchVisualEffect(target.transform.position, transform.position));
                    target.GetHit(currentDamage);
                    break;
                }
            }
        }
    }


    public void LevelUp()
    {
        currentLevel++;
        currentAttackSpeed = myStats.GetCurrentAttackSpeed(currentLevel);
        currentDamage = myStats.GetCurrentDamage(currentLevel);
        currentAttackRange = myStats.GetCurrentAttackRange(currentLevel);
        NextUpgradeCost = myStats.GetCurrentUpgradeCost(currentLevel);
    }

    private IEnumerator LaunchVisualEffect(Vector3 startPoint, Vector3 endPoint)
    {
        ray.SetPositions(new []{startPoint, endPoint});
        ray.colorGradient = GradientFromColor(Color.white);
        rend.color = Color.white;
        transform.Rotate(Vector3.back, 5f);
        yield return new WaitForSeconds(Mathf.Min(0.1f, 1f/currentAttackSpeed));
        rend.color = startColor;
        ray.colorGradient = GradientFromColor(Color.clear);
    }
    
    private Gradient GradientFromColor(Color color)
    {
        var gradient = new Gradient();
            
        var colorKey = new GradientColorKey[2];
        colorKey[0].color = color;
        colorKey[0].time = 0f;
        
        var alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = color.a;
        alphaKey[0].time = 0.0f;

        gradient.SetKeys(colorKey, alphaKey);
        return gradient;

    }
}
