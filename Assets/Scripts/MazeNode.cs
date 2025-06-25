using UnityEngine;

enum NodeState
{
    PlayerVisited,
    PlayerCurrent,
    PlayerHidden
}
public class MazeNode : MonoBehaviour
{
    public GameObject[] walls;
    public MeshRenderer floor;

    public Vector2Int coords;
    
    private void Start()
    {
        coords = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }


    public void RemoveWall(int wallIndex)
    {
        walls[wallIndex].SetActive(false); // walls[wallIndex].destroy();
    }
    public void SetState(int state)
    {
        switch (state)
        {
            case (int)NodeState.PlayerHidden:
                floor.material.color = Color.black;
                break;
            case (int)NodeState.PlayerCurrent:
                floor.material.color = new Color(0.4588f, 0.6f, 0.294f);
                break;
            case (int)NodeState.PlayerVisited:
                floor.material.color = Color.blue;
                break;
        }
    }

}
