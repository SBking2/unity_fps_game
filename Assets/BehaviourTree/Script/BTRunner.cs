using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRunner : MonoBehaviour
{
    [SerializeField] private float m_run_gap;
    [SerializeField] private BehaviourTree m_tree;
    public BehaviourTree Tree { get; private set; }
    private State m_state;
    private float m_timer;
    private void Start()
    {
        Tree = m_tree.Clone();
        Tree.black_board.runner = this.gameObject;
    }

    private void Update()
    {
        m_timer += Time.deltaTime;
        
        if(m_timer > m_run_gap)
        {
            m_timer = 0f;
            if (m_state != State.Success || m_state != State.Failure)
                m_state = Tree.Update();
        }
    }
}
