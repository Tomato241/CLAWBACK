using UnityEngine;

public class UFOClawChain : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform m_claw;

    [Header("Chain Parts")]
    [SerializeField] private Transform[] m_chainParts;

    [Header("Chain")]
    [SerializeField] private float m_chainLength = 0.5f; // チェーンの長さ

    [Header("Physics")]
    [SerializeField] private float m_gravity = 15.0f; // 重力の強さ
    [SerializeField] private float m_damping = 0.98f; // 減衰の強さ

    [Header("Constraint")]
    [SerializeField] private int m_constraintIterations = 8; // チェーンの距離を固定するための反復回数

    [Header("Swing")]
    [SerializeField] private float m_swingPower = 1.0f; 

    [Header("Weight")]
    [SerializeField] private float m_itemWeight = 0.0f;

    private Vector3[] m_previousPositions;
    private Vector3[] m_velocities;

    private void Start()
    {
        if (m_claw == null)
        {
            Debug.LogError("UFOClawChain : Clawが設定されていません");
            return;
        }

        if (m_chainParts == null || m_chainParts.Length == 0)
        {
            Debug.LogError("UFOClawChain : Chain Partsが設定されていません");
            return;
        }

        m_previousPositions = new Vector3[m_chainParts.Length];
        m_velocities = new Vector3[m_chainParts.Length];

        InitializeChain();
    }

    private void Update()
    {
        if (m_claw == null)
        {
            return;
        }

        if (m_chainParts == null || m_chainParts.Length == 0)
        {
            return;
        }

        float deltaTime = Time.deltaTime;
        for (int i = 0; i < m_chainParts.Length; i++)
        {
            if (m_chainParts[i] == null)
                continue;

            Vector3 currentPosition =
                m_chainParts[i].position;

            // 重力
            m_velocities[i] +=
                Vector3.down *
                m_gravity *
                deltaTime;

            // 減衰
            m_velocities[i] *= m_damping;

            // 移動
            m_chainParts[i].position +=
                m_velocities[i] *
                deltaTime;

            // 前回位置を保存
            m_previousPositions[i] =
                currentPosition;
        }
        for (int iteration = 0;
             iteration < m_constraintIterations;
             iteration++)
        {
            // 一番上はClawに固定
            if (m_chainParts[0] != null)
            {
                Vector3 direction =
                    m_chainParts[0].position -
                    m_claw.position;

                if (direction.sqrMagnitude < 0.0001f)
                {
                    direction = Vector3.down;
                }

                direction.Normalize();

                m_chainParts[0].position =
                    m_claw.position +
                    direction * m_chainLength;
            }
            for (int i = 1; i < m_chainParts.Length; i++)
            {
                if (m_chainParts[i] == null)
                {
                    continue;
                }
                  
                if (m_chainParts[i - 1] == null)
                {
                    continue;
                }
              
                Transform previous =
                    m_chainParts[i - 1];

                Transform current =
                    m_chainParts[i];


                Vector3 direction =
                    current.position -
                    previous.position;

                if (direction.sqrMagnitude < 0.0001f)
                {
                    direction = Vector3.down;
                }

                direction.Normalize();


                current.position =
                    previous.position +
                    direction * m_chainLength;
            }
        }
        for (int i = 0; i < m_chainParts.Length; i++)
        {
            if (m_chainParts[i] == null)
            {
                continue;
            }  
            Vector3 velocity =
                (m_chainParts[i].position -
                m_previousPositions[i]) /
                Mathf.Max(deltaTime, 0.0001f);

            m_velocities[i] = velocity;
        }
    }
    private void InitializeChain()
    {
        for (int i = 0; i < m_chainParts.Length; i++)
        {
            if (m_chainParts[i] == null)
            {
                continue;
            }   
            Vector3 position =
                m_claw.position;

            position.y -=
                m_chainLength * (i + 1);

            m_chainParts[i].position =
                position;

            m_previousPositions[i] =
                position;

            m_velocities[i] =
                Vector3.zero;
        }
    }

    public void SetItemWeight(float weight)
    {
        m_itemWeight =
            Mathf.Max(0.0f, weight);
    }
    public void AddSwing(float power)
    {
        if (m_velocities == null)
        {
            return;
        }
        for (int i = 0; i < m_velocities.Length; i++)
        {
            float ratio =
                (float)(i + 1) /
                m_velocities.Length;

            m_velocities[i].x +=
                power *
                ratio *
                m_swingPower;
        }
    }
}