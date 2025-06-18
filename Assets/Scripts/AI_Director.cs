using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class AI_Director : MonoBehaviour
{
    public Dijkstra aStar;
    public Dijkstra dijkstra;
    public GridGenerator grid;
    private Transform target;
    private int wavepointIndex;
    private List<Node> path = new List<Node>();
    public float speed = 10f;
    public Node goal;
    public Node start;

    private void Awake()
    {


        aStar = GameManager.gm.astar;
        dijkstra = GameManager.gm.dijkstra;
        grid = GameManager.gm.grid;
        goal = GameManager.gm.goal;
        start = GameManager.gm.start;

    }

    private void Start()
    {
        //grid.GenerateGrid();
        aStar.GetAllNodes();
        dijkstra.GetAllNodes();

        Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.InstanceID);
        int startNode = nodes.Length - 1;
        int goalNode = Random.Range(0, nodes.Length);

        Stopwatch timer = new Stopwatch();
        timer.Start();

        path = aStar.FindShortestPath(start, goal);
        target = path[0].transform;

        timer.Stop();
        Debug.Log("A* = " + timer.ElapsedMilliseconds);
        aStar.DebugPath(path);
        timer = new Stopwatch();
        timer.Start();

        List<Node> path2 = dijkstra.FindShortestPath(nodes[nodes.Length - 1], nodes[goalNode]);
        timer.Stop();
        Debug.Log("Dijkstra = " + timer.ElapsedMilliseconds);
        //Dijkstra.DebugPath(path2);
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= path.Count - 1)
        {
            GameManager.gm.RemoveEnemyFromList(this);
            GameManager.gm.health -= 1;
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = path[wavepointIndex].transform;
    }

    public void UpdatePath()
    {
        Debug.Log(wavepointIndex);
        path = aStar.FindShortestPath(path[wavepointIndex], goal);
        wavepointIndex = 0;
    }
}