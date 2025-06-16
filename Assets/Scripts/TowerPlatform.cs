using UnityEngine;

public class TowerPlatform : MonoBehaviour
{
    public Color hoverColor;

    private GameObject currentTower;

    private Renderer rend;
    private Color startColor;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if(currentTower != null)
        {
            Debug.Log("Can't build there");
            return;
        }
        GameObject towerToBuild = BuildManager.instance.GetTowerToBuild();
        currentTower = Instantiate(towerToBuild, transform.position, transform.rotation);

    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
