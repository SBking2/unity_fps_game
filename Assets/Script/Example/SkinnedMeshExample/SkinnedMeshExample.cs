using UnityEngine;
using UnityEngine.InputSystem;

public class SkinnedMeshExample : MonoBehaviour
{
    [Header("��������")]
    public Transform targetBone;          // Ҫ�����Ĺ���
    public HumanBodyBones humanBone;      // ����ʹ�����ι���ö��

    [Header("��������")]
    public float shakeIntensity = 0.5f;   // ����ǿ��
    public float shakeFrequency = 25f;    // ����Ƶ��(Hz)
    public float damping = 2f;            // ��������

    private Animator animator;
    private Quaternion originalRotation;
    private float currentShakeAmount = 0f;
    private bool isShaking = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        // ���ָ�������ι�����û���ֶ�ָ������Transform
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
            Debug.LogError("û���ҵ�Ŀ�������");
            enabled = false;
        }
    }

    void Update()
    {
        // ������������
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartShake();
        }
    }

    void LateUpdate()
    {
        if (isShaking && targetBone != null)
        {
            // ����˥���Ķ���ǿ��
            currentShakeAmount = Mathf.Lerp(currentShakeAmount, 0f, damping * Time.deltaTime);

            // ������ǿ�Ⱥ�Сʱֹͣ
            if (currentShakeAmount < 0.01f)
            {
                StopShake();
                return;
            }

            // ʹ��Perlin��������ƽ���������
            float noiseX = Mathf.PerlinNoise(Time.time * shakeFrequency, 0) * 2 - 1;
            float noiseY = Mathf.PerlinNoise(0, Time.time * shakeFrequency) * 2 - 1;
            float noiseZ = Mathf.PerlinNoise(Time.time * shakeFrequency, Time.time * shakeFrequency) * 2 - 1;

            Vector3 randomOffset = new Vector3(noiseX, noiseY, noiseZ) * currentShakeAmount * shakeIntensity;

            // Ӧ�ö�����ת
            targetBone.localRotation = originalRotation * Quaternion.Euler(randomOffset);
        }
    }

    // ��ʼ����
    public void StartShake(float intensityMultiplier = 1f)
    {
        if (targetBone == null) return;

        isShaking = true;
        currentShakeAmount = 1f * intensityMultiplier;
        originalRotation = targetBone.localRotation; // ����ԭʼ��תΪ��ǰ״̬
    }

    // ����ֹͣ����
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