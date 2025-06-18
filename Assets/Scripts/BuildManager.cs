using TMPro;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private GameObject _towerToBuild;
    public GameObject tower;
    public float buildCooldown;
    public float maxCooldown = 5f;
    public bool canBuild;
    public TMP_Text timerText;

    public static BuildManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        _towerToBuild = tower;
        buildCooldown = 0;
        canBuild = true;
        timerText.text = "Build Cooldown: \n" + Mathf.RoundToInt(buildCooldown * 100) / 100 + " seconds";
    }

    private void Update()
    {
        if (!canBuild)
        {
            buildCooldown -= Time.deltaTime;
            timerText.text = "Build Cooldown: \n" + Mathf.RoundToInt(buildCooldown * 100) / 100 + " seconds";
            if(buildCooldown <= 0)
            {
                canBuild = true;
            }
        }
    }

    public GameObject GetTowerToBuild()
    {
        return _towerToBuild;
    }
}
