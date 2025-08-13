using System;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager
{
    private int map_width;
    private int map_height;
    private AStarNode[,] m_nodes;
    private List<AStarNode> m_open_list = new List<AStarNode>();
    private List<AStarNode> m_close_list = new List<AStarNode>();
    public void Init(int width, int height, Action<AStarNode, int, int> callback = null)
    {
        map_width = width;
        map_height = height;

        m_nodes = new AStarNode[map_width, map_height];
        for (int i = 0; i < map_width; i++)
        {
            for (int j = 0; j < map_height; j++)
            {
                int random = UnityEngine.Random.Range(0, 255);
                m_nodes[i, j] = new AStarNode(i, j, (AStarNodeType)(random > 200 ? 1 : 0));
                if (callback != null) callback(m_nodes[i, j], i, j);
            }
        }
    }

    public List<AStarNode> FindPath(Vector2 start_pos, Vector2 end_pos)
    {
        //判断起点和终点是否合法
        if (start_pos.x < 0 || start_pos.y < 0 || start_pos.x >= map_width || start_pos.y >= map_height
            || end_pos.x < 0 || end_pos.y < 0 || end_pos.x >= map_width || end_pos.y >= map_height)
            return null;

        AStarNode start_node = m_nodes[(int)start_pos.x, (int)start_pos.y];
        AStarNode end_node = m_nodes[(int)end_pos.x, (int)end_pos.y];

        m_open_list.Add(start_node);
        start_node.is_traverse = true;

        while (true)
        {
            AStarNode sn = FindMinInOpen();

            //找到终点
            if (sn == end_node)
            {
                List<AStarNode> path = new List<AStarNode>() { sn };
                while (sn.last_node != null)
                {
                    path.Add(sn.last_node);
                    sn = sn.last_node;
                }
                path.Reverse();
                return path;
            }

            m_open_list.Remove(sn);
            m_close_list.Add(sn);

            FindNearlyNode(sn.x - 1, sn.y - 1, sn.cost + 1.4f, sn, end_node);
            FindNearlyNode(sn.x, sn.y - 1, sn.cost + 1.0f, sn, end_node);
            FindNearlyNode(sn.x + 1, sn.y - 1, sn.cost + 1.4f, sn, end_node);

            FindNearlyNode(sn.x - 1, sn.y, sn.cost + 1.0f, sn, end_node);
            FindNearlyNode(sn.x + 1, sn.y, sn.cost + 1.0f, sn, end_node);

            FindNearlyNode(sn.x - 1, sn.y + 1, sn.cost + 1.4f, sn, end_node);
            FindNearlyNode(sn.x, sn.y + 1, sn.cost + 1.0f, sn, end_node);
            FindNearlyNode(sn.x + 1, sn.y + 1, sn.cost + 1.4f, sn, end_node);
        }
    }

    /// <summary>
    /// 在open_list里找到F值最小的node
    /// </summary>
    /// <returns></returns>
    private AStarNode FindMinInOpen()
    {
        if (m_open_list.Count == 0) return null;
        AStarNode min = m_open_list[0];
        foreach (var node in m_open_list)
        {
            if (node.cost + node.estimate < min.cost + min.estimate)
                min = node;
        }
        return min;
    }

    /// <summary>
    /// 遍历一个点，并将其加入open_list
    /// </summary>
    private void FindNearlyNode(int x, int y, float cost, AStarNode last_node, AStarNode end_node)
    {
        if (x < 0 || x >= map_width || y < 0 || y >= map_height) return;

        AStarNode node = m_nodes[x, y];
        if (node == null || node.node_type == AStarNodeType.obstacle
            || node.is_traverse)
            return;

        node.last_node = last_node;
        node.cost = last_node.cost + cost;
        node.estimate = Mathf.Abs(end_node.x - node.x) + Mathf.Abs(end_node.y - node.y);
        node.is_traverse = true;
        m_open_list.Add(node);
    }
}