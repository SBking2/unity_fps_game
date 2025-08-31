using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 用于给ChaState下命令
/// </summary>
public class AIAgent : MonoBehaviour
{
    [SerializeField] private BehaviourTree m_behaviour_tree;
    [SerializeField] private float m_tree_gap;
    private float m_timer;
    private State m_tree_state = State.Running;
    public BehaviourTree BT { get; private set; }
    private NavMeshAgent m_nav_agent;
    private ChaState m_chastate;

    private void Awake()
    {
        m_nav_agent = GetComponent<NavMeshAgent>();
        m_chastate = GetComponent<ChaState>();
    }

    private void Start()
    {
        BT = m_behaviour_tree.Clone();
        BT.black_board.runner = this.gameObject;
        BT.black_board.player = GameObject.Find("Player");

        m_nav_agent.updatePosition = false;
        m_nav_agent.updateRotation = false;
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        m_timer += delta;

        if(m_timer > m_tree_gap && m_tree_state == State.Running)
        {
            m_timer = 0.0f;
            m_tree_state = BT.Update();
        }

    }

    public void MoveTo(Vector3 target)
    {
        m_nav_agent.SetDestination(target);
        if (m_nav_agent.path.corners.Length > 1.0f)
        {
            Vector3 next = m_nav_agent.steeringTarget; // 当前路径上的下一个点
            Vector3 dir = (next - transform.position).normalized;
            m_chastate.MoveDirect(dir);
        }
    }

    public void StopMove()
    {
        m_chastate.MoveDirect(Vector3.zero);
    }
}
