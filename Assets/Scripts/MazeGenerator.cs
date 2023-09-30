using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public int width = 11;
    public int height = 11;
    private int[,] maze;
    private Stack<Vector2> pathStack = new Stack<Vector2>();

    void Start()
    {
        maze = new int[width, height];
        GenerateMaze();
    }

    void GenerateMaze()
    {
        // 1. Initialize maze with walls
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1; // 1 represents a wall, 0 represents a floor
            }
        }

        // 2. Start from a random point
        int startX = Random.Range(0, width / 2) * 2;
        int startY = Random.Range(0, height / 2) * 2;
        maze[startX, startY] = 0;
        pathStack.Push(new Vector2(startX, startY));

        // 3. Carve the maze
        while (pathStack.Count > 0)
        {
            Vector2 current = pathStack.Peek();
            List<Vector2> unvisitedNeighbours = new List<Vector2>();

            // Check all four directions
            for (int dx = -2; dx <= 2; dx += 4)
            {
                for (int dy = -2; dy <= 2; dy += 4)
                {
                    if (dx != 0 && dy != 0) continue;

                    int x = (int)current.x + dx;
                    int y = (int)current.y + dy;

                    if (x >= 0 && y >= 0 && x < width && y < height && maze[x, y] == 1)
                    {
                        unvisitedNeighbours.Add(new Vector2(x, y));
                    }
                }
            }

            if (unvisitedNeighbours.Count > 0)
            {
                Vector2 chosen = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                maze[(int)chosen.x, (int)chosen.y] = 0;
                maze[(int)current.x + ((int)chosen.x - (int)current.x) / 2, (int)current.y + ((int)chosen.y - (int)current.y) / 2] = 0;
                pathStack.Push(chosen);
            }
            else
            {
                pathStack.Pop();
            }
        }

        // 4. Instantiate maze in Unity
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject toInstantiate = maze[x, y] == 1 ? wallPrefab : floorPrefab;
                Instantiate(toInstantiate, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
