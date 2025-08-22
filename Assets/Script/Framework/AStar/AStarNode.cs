public enum AStarNodeType
{
    normal = 0,
    obstacle = 1
}

public class AStarNode
{
    public AStarNode(int x, int y, AStarNodeType type)
    {
        this.x = x;
        this.y = y;
        node_type = type;
    }
    public int x;
    public int y;
    public float cost;
    public float estimate;
    public bool is_traverse;
    public AStarNode last_node;
    public AStarNodeType node_type;
}