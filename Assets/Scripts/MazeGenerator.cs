using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    //TODO: Change the Maze when the player moves
    public MazeNode nodePrefab;
    public Vector2Int mazeSize;

    public MazeNode scriptMaze;

    public GameObject player;
    private void Start()
    {
        GenerateMaze(mazeSize);
    }

    void GenerateMaze(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f));
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                nodes.Add(newNode);
            }
        }

        HashSet<MazeNode> visited = new HashSet<MazeNode>();
        List<(MazeNode node, MazeNode neighbor, int direction)> walls = new List<(MazeNode, MazeNode, int)>();

        int GetIndex(int x, int y) => x * size.y + y;

        int nodeNumber = Random.Range(0, nodes.Count);
        MazeNode StartNode = nodes[nodeNumber];

        Instantiate(player, StartNode.transform.position,Quaternion.identity, transform);

        visited.Add(StartNode);

        int[,] directions = { { 1, 0, 1, 0 }, { -1, 0, 0, 1 }, { 0, 1, 3, 2 }, { 0, -1, 2, 3 } }; // dx, dy, neighborWall, currentWall

        void AddWalls(MazeNode from, int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {
                int nx = x + directions[i, 0];
                int ny = y + directions[i, 1];

                if (nx >= 0 && nx < size.x && ny >= 0 && ny < size.y)
                {
                    MazeNode neighbor = nodes[GetIndex(nx, ny)];
                    if (!visited.Contains(neighbor))
                    {
                        walls.Add((from, neighbor, i));
                    }
                }
            }
        }

        int startX = nodes.IndexOf(StartNode) / size.y;
        int startY = nodes.IndexOf(StartNode) % size.y;
        AddWalls(StartNode, startX, startY);

        StartNode.SetState((int)NodeState.PlayerCurrent);

        while (walls.Count > 0)
        {
            int randIndex = Random.Range(0, walls.Count);
            var (current, neighbor, dir) = walls[randIndex];
            walls.RemoveAt(randIndex);

            if (visited.Contains(neighbor)) continue;

            visited.Add(neighbor);
            neighbor.SetState((int)NodeState.PlayerHidden);

            // Remove walls between current and neighbor
            neighbor.RemoveWall(directions[dir, 2]);
            current.RemoveWall(directions[dir, 3]);

            int nx = nodes.IndexOf(neighbor) / size.y;
            int ny = nodes.IndexOf(neighbor) % size.y;
            AddWalls(neighbor, nx, ny);
        }
    }
}