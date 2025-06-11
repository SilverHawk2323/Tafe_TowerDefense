using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Node : MonoBehaviour
{
    public List<Node> neighbours;

    private float _pathWeight = float.PositiveInfinity;
    public float PathWeightProperity
    {
        get => _pathWeight;
        set => _pathWeight = value;
    }

    public Node PreviousNode { get; set; }

    private float heuristic;
    public float Heuristic 
    { 
        get => heuristic; 
        set => heuristic = value;
    }

    public float pathHeuristicWeight
    {
        get => _pathWeight + heuristic;
    }

    public float SetHeuristic(Vector3 goal)
    {
        heuristic = Vector3.Distance(transform.position, goal);
        return heuristic;
    }

    public void ResetNode()
    {
        _pathWeight = float.PositiveInfinity;
        PreviousNode = null;
    }

    private void OnDrawGizmos()
    {
        if(neighbours == null) return;
        float radius = 0.2f;

        Gizmos.color = Color.blue;
        foreach (var node in neighbours)
        {
            if (node == null) continue;
            Vector3 direction = node.transform.position - transform.position;
            Vector3 right = Vector3.Cross(direction, Vector3.up).normalized * 0.03f;

            Gizmos.DrawRay(transform.position + right, direction);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void OnValidate() => ValidateNeighbours();

    private void ValidateNeighbours()
    {
        foreach(var node in neighbours)
        {
            if(node == null)
            {
                continue;
            }

            if (!node.neighbours.Contains(this))
            {
                node.neighbours.Add(this);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var node in neighbours)
        {
            if (node.neighbours.Contains(this))
            {
                node.neighbours.Remove(this);
            }
        }
    }
}