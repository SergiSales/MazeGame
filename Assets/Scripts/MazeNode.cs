using UnityEngine;

enum NodeState
{
    Available,
    Current,
    Completed,
    PlayerVisited,
    PlayerCurrent,
    PlayerHidden
}
public class MazeNode : MonoBehaviour
{
    public GameObject[] walls;
    public MeshRenderer floor;


    public void RemoveWall(int wallIndex)
    {
        walls[wallIndex].SetActive(false); // walls[wallIndex].destroy();
    }
    public void SetState(int state)
    {
        switch (state)
        {
            case (int)NodeState.Available:
                floor.material.color = Color.white;
                break;
            case (int)NodeState.Current:
                floor.material.color = Color.yellow;
                break;
            case (int)NodeState.Completed:
                floor.material.color = Color.blue;
                break;
        }
    }
}
