using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    private PlayerCamera m_camera;
    private Movement m_player_movement;

    private Vector3 m_input_direct;

    public void Init()
    {
        m_camera = new PlayerCamera(GameObject.Find("Player").transform
            , GameObject.Find("CameraVerticalHandler").transform, GameObject.Find("CameraBob").transform);
        m_player_movement = GameObject.Find("Player").GetComponent<Movement>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update(float delta)
    {
        float mutiplier = m_player_movement.Velocity.magnitude / 5.0f;

        float fre_mul = 0.0f;
        if (m_player_movement.Velocity.magnitude > m_player_movement.max_run_speed - 0.5f)
            fre_mul = m_player_movement.max_run_speed / 5.0f;
        else if (m_player_movement.Velocity.magnitude > m_player_movement.max_walk_speed - 0.5f)
            fre_mul = m_player_movement.max_walk_speed / 5.0f;
        else if (m_player_movement.Velocity.magnitude > m_player_movement.max_crouch_speed - 0.5f)
            fre_mul = m_player_movement.max_crouch_speed / 5.0f;

        m_camera.Update(delta, mutiplier, fre_mul);
        HandlerPlayerMove(delta);
    }

    private void HandlerPlayerMove(float delta)
    {
        Vector3 direct = m_player_movement.transform.rotation * m_input_direct;
        m_player_movement.SetMoveDirect(direct);
    }

    public void OnEnable()
    {
        InputManager.Instance.onMouseDelta += m_camera.SetInputMouseDelta;
        InputManager.Instance.onMoveDelta += Input2World;
        InputManager.Instance.onJump += m_player_movement.Jump;
        InputManager.Instance.onSprint += m_player_movement.SetRun;
        InputManager.Instance.onCroch += m_player_movement.SetCrouch;
    }

    public void OnDisable()
    {
        InputManager.Instance.onMouseDelta -= m_camera.SetInputMouseDelta;
        InputManager.Instance.onMoveDelta -= Input2World;
        InputManager.Instance.onJump -= m_player_movement.Jump;
        InputManager.Instance.onSprint -= m_player_movement.SetRun;
        InputManager.Instance.onCroch -= m_player_movement.SetCrouch;
    }

    /// <summary>
    /// 把玩家的输入转换为世界坐标系的方向
    /// </summary>
    /// <param name="input"></param>
    private void Input2World(Vector2 input)
    {
        m_input_direct = new Vector3(input.x, 0.0f, input.y);
    }

    private void Debugger(bool flag)
    {
        Debug.Log(flag);
    }
}
