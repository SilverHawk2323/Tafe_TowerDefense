using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public float countdown = 2f;
    public Dijkstra astar;
    public Dijkstra dijkstra;
    public GridGenerator grid;
    public static GameManager gm;
    public float timeBetweenWaves = 5f;
    public Transform spawnPosition;
    public GameObject enemy;
    public int waveIndex = 0;
    public List<AI_Director>enemies = new List<AI_Director>();
    public Node start;
    public Node goal;

    private void Awake()
    {
        if(gm == null)
        {
            gm = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    private IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemy, spawnPosition.position, spawnPosition.rotation);
        enemies.Add(newEnemy.GetComponentInChildren<AI_Director>());
    }

    public void UpdateEnemyPath()
    {
        foreach(AI_Director director in enemies)
        {
            director.UpdatePath();
        }
    }

    public void RemoveEnemyFromList(AI_Director input)
    {
        enemies.Remove(input);
    }
}
