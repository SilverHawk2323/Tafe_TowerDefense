using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private GameObject _towerToBuild;
    public GameObject tower;

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
    }

    public GameObject GetTowerToBuild()
    {
        return _towerToBuild;
    }
}
