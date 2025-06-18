using System.Collections.Generic;
using UnityEngine;

public class AttackTower : MonoBehaviour
{
    public LayerMask nodeLayer;
    List<Node> nodes = new List<Node>();
    //private List<Node> nodes = new List<Node>();
    private void Start()
    {
        
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 10f, Vector3.one, 1f, nodeLayer);
        if(hits.Length > 0)
        {
            print(hits.Length);
        }
        foreach (RaycastHit hit in hits)
        {
            nodes.Add(hit.transform.gameObject.GetComponent<Node>());
        }
        foreach (Node node in nodes)
        {
            node.AddToHeuristic(999999f);
            //print(node.pathHeuristicWeight);
            GameManager.gm.UpdateEnemyPath();
        }
    }
}
