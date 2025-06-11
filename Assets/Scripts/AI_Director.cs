using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class AI_Director : MonoBehaviour
{
    public Dijkstra pathfinder;
    public Dijkstra pathfinder2;
    public GridGenerator grid;

    private void Start()
    {
        //grid.GenerateGrid();
        pathfinder.GetAllNodes();
        pathfinder2.GetAllNodes();

        Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.InstanceID);
        int startNode = nodes.Length - 1;
        int goalNode = Random.Range(0, nodes.Length);

        Stopwatch timer = new Stopwatch();
        timer.Start();
        

        List<Node> path = pathfinder.FindShortestPath(nodes[nodes.Length - 1], nodes[goalNode]);

        timer.Stop();
        Debug.Log("A* = " + timer.ElapsedMilliseconds);
        pathfinder.DebugPath(path);
        timer = new Stopwatch();
        timer.Start();

        List<Node> path2 = pathfinder2.FindShortestPath(nodes[nodes.Length - 1], nodes[goalNode]);
        timer.Stop();
        Debug.Log("Dijkstra = " + timer.ElapsedMilliseconds);
        pathfinder2.DebugPath(path2);
    }
}