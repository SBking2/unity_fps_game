using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarExample : MonoBehaviour
{
    public Vector2 start_point;
    public Vector2 end_point;
    public float spacing;

    private GameObject[,] m_cubes;

    private AStarManager astar_mgr = new AStarManager();

    private void Start()
    {
        m_cubes = new GameObject[20, 20];
        astar_mgr.Init(20, 20, (node, x, y) =>
        {
            m_cubes[x, y] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            m_cubes[x, y].transform.position = transform.position + new Vector3(
                    x * spacing, 
                    0, 
                    y * spacing);

            if(node.node_type == AStarNodeType.obstacle)
            {
                Renderer renderer = m_cubes[x, y].GetComponent<Renderer>();
                Material mat = renderer.material;  // 这会创建材质的实例
                mat.color = Color.green;
            }
        });
    }

    private void OnEnable()
    {
        InputManager.Instance.onJump += Find;
    }

    private void OnDisable()
    {
        InputManager.Instance.onJump -= Find;
    }

    private void Find()
    { 
        foreach(var cube in m_cubes)
        {
            Renderer renderer = cube.GetComponent<Renderer>();
            Material mat = renderer.material;  // 这会创建材质的实例
            if(mat.color != Color.green)
                mat.color = Color.white;
        }

        foreach(var node in astar_mgr.FindPath(start_point, end_point))
        {
            Renderer renderer = m_cubes[node.x, node.y].GetComponent<Renderer>();
            Material mat = renderer.material;  // 这会创建材质的实例
            mat.color = Color.red;
        }
    }
}
