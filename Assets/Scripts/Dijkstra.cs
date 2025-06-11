using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{

    protected Node[] _nodesInScene;

    public void GetAllNodes()
    {
        _nodesInScene = FindObjectsByType<Node>(FindObjectsSortMode.None);
        
    }

    private void Awake()
    {
        //GetAllNodes();
    }

    /*protected virtual void Start()
    {
        List<Node> path = FindShortestPath(startNode, endNode);
        DebugPath(path);
    }*/

    public void DebugPath(List<Node> path)
    {
        for (int i = 0; i <path.Count - 1; i++)
        {
            Debug.DrawLine(path[i].transform.position, path[i + 1].transform.position, Color.green, 60f);
        }
    }


    public List<Node> FindShortestPath(Node start, Node goal)
    {
        if(RunAlgorithm(start, goal))
        {
            List<Node> results = new List<Node>();
            Node current = goal;
            do
            {
                results.Insert(0, current);
                current = current.PreviousNode;
            } while (current != null);
            return results;
        }

        return null;
    }

    protected virtual bool RunAlgorithm(Node start, Node goal)
    {
        List<Node> unexplored = new List<Node>();
        Node sNode = start; //startNode
        Node eNode = goal; //endNode

        SetUnexplored(ref unexplored);

        sNode.PathWeightProperity = 0;
        while (unexplored.Count > 0)
        {
            unexplored.Sort((a,b) => a.PathWeightProperity.CompareTo(b.PathWeightProperity));
            Node current = unexplored[0];
            unexplored.RemoveAt(0);

            foreach (var neighbourNode in current.neighbours)
            {
                if (!unexplored.Contains(neighbourNode))
                {
                    continue;
                }

                float neighbourWeight = Vector3.Distance(current.transform.position, neighbourNode.transform.position);

                neighbourWeight += current.PathWeightProperity;

                if(neighbourWeight < neighbourNode.PathWeightProperity)
                {
                    neighbourNode.PathWeightProperity = neighbourWeight;
                    neighbourNode.PreviousNode = current;
                }
            }
            if (current == eNode) return true;
        }

        return false;
    }

    protected void SetUnexplored(ref List<Node> unexplored)
    {
        foreach(var node in _nodesInScene)
        {
            node.ResetNode();
            unexplored.Add(node);
        }
    }
}
