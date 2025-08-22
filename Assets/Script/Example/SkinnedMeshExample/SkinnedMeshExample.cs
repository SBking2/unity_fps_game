using UnityEngine;
using UnityEngine.InputSystem;

public class SkinnedMeshExample : MonoBehaviour
{
    [Header("骨骼设置")]
    public Transform targetBone;          // 要抖动的骨骼
    public HumanBodyBones humanBone;      // 或者使用人形骨骼枚举

    [Header("抖动参数")]
    public float shakeIntensity = 0.5f;   // 抖动强度
    public float shakeFrequency = 25f;    // 抖动频率(Hz)
    public float damping = 2f;            // 减速阻尼

    private Animator animator;
    private Quaternion originalRotation;
    private float currentShakeAmount = 0f;
    private bool isShaking = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        // 如果指定了人形骨骼且没有手动指定骨骼Transform
        if (targetBone == null && animator != null && animator.isHuman)
        {
            targetBone = animator.GetBoneTransform(humanBone);
        }

        if (targetBone != null)
        {
            originalRotation = targetBone.localRotation;
        }
        else
        {
            Debug.LogError("没有找到目标骨骼！");
            enabled = false;
        }
    }

    void Update()
    {
        // 按键触发抖动
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartShake();
        }
    }

    void LateUpdate()
    {
        if (isShaking && targetBone != null)
        {
            // 计算衰减的抖动强度
            currentShakeAmount = Mathf.Lerp(currentShakeAmount, 0f, damping * Time.deltaTime);

            // 当抖动强度很小时停止
            if (currentShakeAmount < 0.01f)
            {
                StopShake();
                return;
            }

            // 使用Perlin噪声生成平滑随机抖动
            float noiseX = Mathf.PerlinNoise(Time.time * shakeFrequency, 0) * 2 - 1;
            float noiseY = Mathf.PerlinNoise(0, Time.time * shakeFrequency) * 2 - 1;
            float noiseZ = Mathf.PerlinNoise(Time.time * shakeFrequency, Time.time * shakeFrequency) * 2 - 1;

            Vector3 randomOffset = new Vector3(noiseX, noiseY, noiseZ) * currentShakeAmount * shakeIntensity;

            // 应用抖动旋转
            targetBone.localRotation = originalRotation * Quaternion.Euler(randomOffset);
        }
    }

    // 开始抖动
    public void StartShake(float intensityMultiplier = 1f)
    {
        if (targetBone == null) return;

        isShaking = true;
        currentShakeAmount = 1f * intensityMultiplier;
        originalRotation = targetBone.localRotation; // 更新原始旋转为当前状态
    }

    // 立即停止抖动
    public void StopShake()
    {
        isShaking = false;
        currentShakeAmount = 0f;
        if (targetBone != null)
        {
            targetBone.localRotation = originalRotation;
        }
    }
}