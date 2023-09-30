using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int rows = 10, cols = 10;
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    private int[,] grid;
    private Stack<Vector2Int> stack;

    // 0 = sein, 1 = põrand
    void Start()
    {
        grid = new int[rows, cols];
        stack = new Stack<Vector2Int>();

        // Alustame kõik seintega
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                grid[x, y] = 0;
            }
        }

        // Lisame seinad ümber labürindi
        for (int x = 0; x < rows; x++)
        {
            grid[x, 0] = 0;
            grid[x, cols - 1] = 0;
        }

        for (int y = 0; y < cols; y++)
        {
            grid[0, y] = 0;
            grid[rows - 1, y] = 0;
        }

        // Loome juhusliku väljapääsu
        int exitX = Random.Range(1, rows - 1);
        int exitY = cols - 1;
        grid[exitX, exitY] = 1;

        // Alustame labürindi loomist koordinaatidest (1, 1)
        Vector2Int start = new Vector2Int(1, 1);
        grid[start.x, start.y] = 1;
        stack.Push(start);

        GenerateMaze(start);

        // Loome labürindi põhjal GameObjectid
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                if (grid[x, y] == 0)
                {
                    Instantiate(wallPrefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(floorPrefab, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }

    void GenerateMaze(Vector2Int current)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();

        // Üleval
        if (current.x > 1)
        {
            neighbours.Add(new Vector2Int(current.x - 2, current.y));
        }

        // Alla
        if (current.x < rows - 2)
        {
            neighbours.Add(new Vector2Int(current.x + 2, current.y));
        }

        // Vasakule
        if (current.y > 1)
        {
            neighbours.Add(new Vector2Int(current.x, current.y - 2));
        }

        // Paremale
        if (current.y < cols - 2)
        {
            neighbours.Add(new Vector2Int(current.x, current.y + 2));
        }

        // Sega naabrid
        neighbours.Shuffle();

        foreach (Vector2Int neighbour in neighbours)
        {
            if (grid[neighbour.x, neighbour.y] == 0)
            {
                // Eemaldame seina
                grid[(current.x + neighbour.x) / 2, (current.y + neighbour.y) / 2] = 1;
                grid[neighbour.x, neighbour.y] = 1;

                stack.Push(neighbour);
                GenerateMaze(neighbour);
            }
        }

        if (stack.Count > 0)
        {
            Vector2Int next = stack.Pop();
            GenerateMaze(next);
        }
    }
}

// Listide segamise laiendus
public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rnd = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
