using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    private PlayerCamera m_player_camera;
    private Movement m_player_movement;
    private PlayerWeaponController m_player_weapon_controller;
    private PlayerRecoil m_player_recoil;

    private Vector3 m_input_direct;

    public void Init()
    {
        m_player_camera = new PlayerCamera(
            GameObject.Find("Player").transform
            , GameObject.Find("CameraVerticalHandler").transform
            , GameObject.Find("CameraBob").transform);

        m_player_movement = GameObject.Find("Player").GetComponent<Movement>();

        m_player_weapon_controller = new PlayerWeaponController(
            GameObject.Find("ShootDir").transform
            , GameObject.Find("WeaponBob").transform
            , GameObject.Find("WeaponSway").transform
            , LayerMask.GetMask("Default", "Ground", "Enemy")
            );

        m_player_recoil = new PlayerRecoil(
            GameObject.Find("WeaponRecoil").transform
            , GameObject.Find("CameraRecoil").transform
            , GameObject.Find("ShootDir").transform
            );

        //将Fire与Recoil绑定
        m_player_weapon_controller.AddFireListener(m_player_recoil.Recoil);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update(float delta)
    {
        float fre_mul = 0.0f;
        if (m_player_movement.Movetype == MoveType.Run)
            fre_mul = m_player_movement.max_run_speed / 5.0f;
        else if (m_player_movement.Movetype == MoveType.Walk)
            fre_mul = m_player_movement.max_walk_speed / 5.0f;
        else if (m_player_movement.Movetype == MoveType.Crouch)
            fre_mul = m_player_movement.max_crouch_speed / 5.0f;
        else
            fre_mul = 0.0f;

        float mutiplier = fre_mul;
        m_player_camera.Update(delta, mutiplier, fre_mul, m_player_movement.IsGround);
        m_player_weapon_controller.Update(
            delta
            , mutiplier
            , fre_mul
            , m_player_movement.IsGround
            , m_mouse_delta
            );
        m_player_recoil.Update(delta);
        HandlerPlayerMove(delta);
    }


    private void HandlerPlayerMove(float delta)
    {
        Vector3 direct = m_player_movement.transform.rotation * m_input_direct;
        m_player_movement.SetMoveDirect(direct);
    }

    public void OnEnable()
    {
        InputManager.Instance.onMouseDelta += m_player_camera.SetInputMouseDelta;
        InputManager.Instance.onMouseDelta += GetMouseDelta;
        InputManager.Instance.onMoveDelta += Input2World;
        InputManager.Instance.onJump += m_player_movement.Jump;
        InputManager.Instance.onSprint += m_player_movement.SetRun;
        InputManager.Instance.onCroch += m_player_movement.SetCrouch;
        InputManager.Instance.onFire += m_player_weapon_controller.SetAttackSignal;
    }

    public void OnDisable()
    {
        InputManager.Instance.onMouseDelta -= m_player_camera.SetInputMouseDelta;
        InputManager.Instance.onMouseDelta -= GetMouseDelta;
        InputManager.Instance.onMoveDelta -= Input2World;
        InputManager.Instance.onJump -= m_player_movement.Jump;
        InputManager.Instance.onSprint -= m_player_movement.SetRun;
        InputManager.Instance.onCroch -= m_player_movement.SetCrouch;
        InputManager.Instance.onFire -= m_player_weapon_controller.SetAttackSignal;
    }

    /// <summary>
    /// 把玩家的输入转换为世界坐标系的方向
    /// </summary>
    /// <param name="input"></param>
    private void Input2World(Vector2 input)
    {
        m_input_direct = new Vector3(input.x, 0.0f, input.y);
    }

    private Vector2 m_mouse_delta;
    private void GetMouseDelta(Vector2 input)
    {
        m_mouse_delta = input;
    }
    private void Debugger(bool flag)
    {
        Debug.Log(flag);
    }
}
