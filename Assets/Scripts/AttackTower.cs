using System.Collections.Generic;
using UnityEngine;

public class AttackTower : MonoBehaviour
{
    public LayerMask nodeLayer;
    //private List<Node> nodes = new List<Node>();
    private void Start()
    {
        List<Node> nodes = new List<Node>();
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 5f, Vector3.zero, nodeLayer);

        foreach (RaycastHit hit in hits)
        {
            nodes.Add(hit.transform.GetComponent<Node>());
        }
        foreach (Node node in nodes)
        {
            node.AddToHeuristic(10f);
        }
    }
}
