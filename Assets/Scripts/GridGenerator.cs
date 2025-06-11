using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public Node prefab;

    public int rows = 10, columns = 10;
    public float gap = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*void Start()
    {
        GenerateGrid();
    }*/

    [ContextMenu("Generate Grid")]
    public void GenerateGrid()
    {
        Vector3 startPos = transform.position;

        Node prev = null;
        Node[] prevColumn = new Node[columns];

        for (int x = 0; x < rows ; x++)
        {
            for(int y = 0; y < columns ; y++)
            {
                Vector3 newPosition = new Vector3(startPos.x + gap * x, 0f, startPos.z + gap * y);

                Node node = Instantiate(prefab, newPosition, Quaternion.identity);
                node.name = node.name + x + " " + y;
                if(prev != null )
                {
                    node.neighbours.Add(prev);
                    prev.neighbours.Add(node);
                }
                
                if(prevColumn[y] != null)
                {
                    prevColumn[y].neighbours.Add(node);
                    node.neighbours.Add(prevColumn[y]);
                }

                prev = node;

                prevColumn[y] = node;
            }
            prev = null;
        }
    }

    [ContextMenu("MAO")]
    public void KillAllOrphans()
    {
        Node[] nodesInScene = FindObjectsByType<Node>(FindObjectsSortMode.None);

        foreach(var orphan in nodesInScene)
        {
            if (orphan.neighbours.Count == 0)
            {
                DestroyImmediate(orphan.gameObject);
            }
        }
    }
}
