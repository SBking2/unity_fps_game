using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BT
{
    public enum State
    {
        Running,
        Success,
        Failure
    }

    public abstract class Node : ScriptableObject
    {
        public State state = State.Running;

        private bool is_started;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        [HideInInspector] public BlackBoard black_board;
        [TextArea] public string description;

        public State Update()
        {
            if(!is_started)
            {
                is_started = true;
                OnStart();
            }

            state = OnUpdate();

            if(state != State.Running)
            {
                is_started = false;
                OnStop();
            }

            return state;
        }

        public virtual Node Clone()
        {
            return Instantiate(this);
        }

        public virtual void Abort()
        {
            OnStop();
            is_started = false;
            state = State.Failure;
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();
    }
}
